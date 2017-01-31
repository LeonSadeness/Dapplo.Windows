﻿#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Windows
// 
// Dapplo.Windows is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Windows is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Windows. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Dapplo.Windows.Desktop;
using Dapplo.Windows.Enums;
using Dapplo.Windows.Native;
using Dapplo.Windows.Structs;

#endregion

namespace Dapplo.Windows
{
	/// <summary>
	///     The is a container class to scroll a window
	/// </summary>
	public class WindowScroller
	{
		/// <summary>
		///     The window to scroll, this does not need to be the part of the window that actually scrolls
		/// </summary>
		public WindowInfo WindowToScroll { get; private set; }

		/// <summary>
		///     Area which is scrolling, can be the WindowToScroll
		/// </summary>
		public WindowInfo ScrollingArea { get; private set; }

		/// <summary>
		///     What scrollbar to use
		/// </summary>
		public ScrollBarDirection Direction { get; private set; } = ScrollBarDirection.Vertical;

		/// <summary>
		///     Does the scrollbar need to represent the changes?
		/// </summary>
		public bool ShowChanges { get; set; } = true;

		/// <summary>
		/// Specify which scroll mode needs to be used
		/// </summary>
		public ScrollModes ScrollMode { get; set; } = ScrollModes.WindowsMessage;

		/// <summary>
		/// This is used to be able to reset the location, and also detect if we are at the end.
		/// Some windows might add content when the user is (almost) at the end.
		/// </summary>
		public ScrollInfo InitialScrollInfo { get; }

		/// <summary>
		/// Some windows might add content when the user is (almost) at the end.
		/// If this is true, the scrolling doesn't go beyond the intial bounds.
		/// If this is false, the initial value is only used for reset.
		/// </summary>
		public bool KeepInitialBounds { get; set; } = true;

		/// <summary>
		///     Returns true if the scroller is at the start
		/// </summary>
		/// <returns>bool</returns>
		public bool IsAtStart
		{
			get
			{
				ScrollInfo scrollInfo;
				if (!GetPosition(out scrollInfo))
				{
					return false;
				}
				if (KeepInitialBounds)
				{
					return InitialScrollInfo.nMin >= Math.Max(scrollInfo.nPos, scrollInfo.nTrackPos);
				}
				return scrollInfo.nMin >= Math.Max(scrollInfo.nPos, scrollInfo.nTrackPos);
			}
		}

		/// <summary>
		///     Returns true if the scroller is at the end
		/// </summary>
		/// <returns>bool</returns>
		public bool IsAtEnd
		{
			get
			{
				ScrollInfo scrollInfo;
				if (!GetPosition(out scrollInfo))
				{
					return false;
				}
				if (KeepInitialBounds)
				{
					return InitialScrollInfo.nMax <= Math.Max(scrollInfo.nPos, scrollInfo.nTrackPos) + scrollInfo.nPage - 1;
				}
				return scrollInfo.nMax <= Math.Max(scrollInfo.nPos, scrollInfo.nTrackPos) + scrollInfo.nPage - 1;
			}
		}

		/// <summary>
		///     Factory for the WindowScroller
		/// </summary>
		/// <param name="windowToScroll">WindowInfo is the window to scroll or contains an area which can be scrolled</param>
		/// <param name="direction">ScrollBarDirection vertical is the default</param>
		/// <returns>WindowScroller</returns>
		public static async Task<WindowScroller> CreateAsync(WindowInfo windowToScroll, ScrollBarDirection direction = ScrollBarDirection.Vertical)
		{
			var scrollingArea = windowToScroll;
			var initialScrollInfo = new ScrollInfo(ScrollInfoMask.All);
			if (!User32.GetScrollInfo(windowToScroll.Handle, direction, ref initialScrollInfo))
			{
				scrollingArea =
					await WindowsEnumerator.EnumerateWindowsAsync(windowToScroll.Handle)
						.Where(childWindow => User32.GetScrollInfo(childWindow.Handle, direction, ref initialScrollInfo))
						.FirstOrDefaultAsync();
				if (scrollingArea == null)
				{
					return null;
				}
			}
			var windowScroller = new WindowScroller
			{
				WindowToScroll = windowToScroll,
				ScrollingArea = scrollingArea,
				Direction = direction,

			};
			return windowScroller;
		}

		/// <summary>
		/// Get position
		/// </summary>
		/// <returns>SCROLLINFO</returns>
		private bool GetPosition(out ScrollInfo scrollInfo)
		{
			scrollInfo = new ScrollInfo(ScrollInfoMask.All);

			return User32.GetScrollInfo(ScrollingArea.Handle, Direction, ref scrollInfo);
		}

		/// <summary>
		/// Apply position
		/// </summary>
		/// <param name="scrollInfo">SCROLLINFO ref</param>
		/// <returns>bool</returns>
		private bool ApplyPosition(ref ScrollInfo scrollInfo)
		{
			if (ShowChanges)
			{
				if (!User32.SetScrollInfo(ScrollingArea.Handle, Direction, ref scrollInfo, true))
				{
					return false;
				}
			}
			User32.SendMessage(ScrollingArea.Handle, WindowsMessages.WM_VSCROLL, new IntPtr(4 + 0x10000 * scrollInfo.nPos), IntPtr.Zero);
			return true;
		}

		/// <summary>
		///     Move to the start
		/// </summary>
		/// <returns>bool if this worked</returns>
		public bool Start()
		{
			// Only in this mode we can move to the start
			if (ScrollMode != ScrollModes.WindowsMessage)
			{
				return false;
			}
			ScrollInfo scrollInfo;
			if (!GetPosition(out scrollInfo))
			{
				return false;
			}
			scrollInfo.nPos = scrollInfo.nMin;
			return ApplyPosition(ref scrollInfo);
		}

		/// <summary>
		///     Move to the end
		/// </summary>
		/// <returns>bool if this worked</returns>
		public bool End()
		{
			// Only in this mode we can move to the end
			if (ScrollMode != ScrollModes.WindowsMessage)
			{
				return false;
			}
			ScrollInfo scrollInfo;
			if (!GetPosition(out scrollInfo))
			{
				return false;
			}

			scrollInfo.nPos = scrollInfo.nMax;
			return ApplyPosition(ref scrollInfo);
		}

		/// <summary>
		///     Go to the previous "page"
		/// </summary>
		/// <returns>bool if this worked</returns>
		public bool Previous()
		{

			switch (ScrollMode)
			{
				case ScrollModes.KeyboardPageUpDown:
					// TODO: Send page up / down
					break;
				case ScrollModes.WindowsMessage:
					// Calculate previous position
					ScrollInfo scrollInfo;
					if (!GetPosition(out scrollInfo))
					{
						return false;
					}
					scrollInfo.nPos = Math.Max(scrollInfo.nMin, scrollInfo.nPos - (int)scrollInfo.nPage);
					return ApplyPosition(ref scrollInfo);
				case ScrollModes.MouseWheel:
					// TODO: Send mousewheel
					break;
			}

			return true;
		}

		/// <summary>
		///     Go to the next "page"
		/// </summary>
		/// <returns>bool if this worked</returns>
		public bool Next()
		{
			switch (ScrollMode)
			{
				case ScrollModes.KeyboardPageUpDown:
					// TODO: Send page up / down
					break;
				case ScrollModes.WindowsMessage:
					ScrollInfo scrollInfo;
					if (!GetPosition(out scrollInfo))
					{
						return false;
					}

					// Calculate next position
					scrollInfo.nPos = Math.Min(scrollInfo.nMax, scrollInfo.nPos + (int)scrollInfo.nPage);
					return ApplyPosition(ref scrollInfo);
				case ScrollModes.MouseWheel:
					// TODO: Send mousewheel
					break;
			}
			return true;
		}

		/// <summary>
		/// Set the position back to the original
		/// </summary>
		/// <returns>true if this worked</returns>
		public bool Reset()
		{
			ScrollInfo initialScrollInfo = InitialScrollInfo;
			return ApplyPosition(ref initialScrollInfo);
		}
	}
}