﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Windows
// 
//  Dapplo.Windows is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Windows is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Windows. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

namespace Dapplo.Windows.Enums
{
	/// <summary>
	///     Used for SetWinEventHook
	///     See at <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd318066.aspx">MSDN</a>
	///     and <a href="http://source.winehq.org/source/include/winuser.h">here</a>
	///     and <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd742691.aspx">here</a>
	/// </summary>
	public enum WinEvent : uint
	{
		EVENT_MIN = 0x00000001,

		// System events

		/*
		 *  EVENT_SYSTEM_SOUND
		 *  Sent when a sound is played.  Currently nothing is generating this, we
		 *  this event when a system sound (for menus, etc) is played.  Apps
		 *  generate this, if accessible, when a private sound is played.  For
		 *  example, if Mail plays a "New Mail" sound.
		 *
		 *  System Sounds:
		 *  (Generated by PlaySoundEvent in USER itself)
		 *      hwnd            is NULL
		 *      idObject        is OBJID_SOUND
		 *      idChild         is sound child ID if one
		 *  App Sounds:
		 *  (PlaySoundEvent won't generate notification; up to app)
		 *      hwnd + idObject gets interface pointer to Sound object
		 *      idChild identifies the sound in question
		 *  are going to be cleaning up the SOUNDSENTRY feature in the control panel
		 *  and will use this at that time.  Applications implementing WinEvents
		 *  are perfectly welcome to use it.  Clients of IAccessible* will simply
		 *  turn around and get back a non-visual object that describes the sound.
		 */
		EVENT_SYSTEM_SOUND = 0x01,
		/*
		 * EVENT_SYSTEM_ALERT
		 * System Alerts:
		 * (Generated by MessageBox() calls for example)
		 *      hwnd            is hwndMessageBox
		 *      idObject        is OBJID_ALERT
		 * App Alerts:
		 * (Generated whenever)
		 *      hwnd+idObject gets interface pointer to Alert
		 * System Alerts (indexChild of system ALERT notification)
			#define ALERT_SYSTEM_INFORMATIONAL      1       // MB_INFORMATION
			#define ALERT_SYSTEM_WARNING            2       // MB_WARNING
			#define ALERT_SYSTEM_ERROR              3       // MB_ERROR
			#define ALERT_SYSTEM_QUERY              4       // MB_QUESTION
			#define ALERT_SYSTEM_CRITICAL           5       // HardSysErrBox
			#define CALERT_SYSTEM                   6
		 */
		EVENT_SYSTEM_ALERT = 0x02,
		/*
		 * EVENT_SYSTEM_FOREGROUND
		 * Sent when the foreground (active) window changes, even if it is changing
		 * to another window in the same thread as the previous one.
		 *      hwnd            is hwndNewForeground
		 *      idObject        is OBJID_WINDOW
		 *      idChild    is INDEXID_OBJECT
		 */
		EVENT_SYSTEM_FOREGROUND = 0x03,
		/*
		 * Menu
		 *      hwnd            is window (top level window or popup menu window)
		 *      idObject        is ID of control (OBJID_MENU, OBJID_SYSMENU, OBJID_SELF for popup)
		 *      idChild         is CHILDID_SELF
		 *
		 * EVENT_SYSTEM_MENUSTART
		 * EVENT_SYSTEM_MENUEND
		 * For MENUSTART, hwnd+idObject+idChild refers to the control with the menu bar,
		 *  or the control bringing up the context menu.
		 *
		 * Sent when entering into and leaving from menu mode (system, app bar, and
		 * track popups).
		 */
		EVENT_SYSTEM_MENUSTART = 0x04,
		EVENT_SYSTEM_MENUEND = 0x05,
		/*
		 * EVENT_SYSTEM_MENUPOPUPSTART
		 * EVENT_SYSTEM_MENUPOPUPEND
		 * Sent when a menu popup comes up and just before it is taken down.  Note
		 * that for a call to TrackPopupMenu(), a client will see EVENT_SYSTEM_MENUSTART
		 * followed almost immediately by EVENT_SYSTEM_MENUPOPUPSTART for the popup
		 * being shown.
		 *
		 * For MENUPOPUP, hwnd+idObject+idChild refers to the NEW popup coming up, not the
		 * parent item which is hierarchical.  You can get the parent menu/popup by
		 * asking for the accParent object.
		 */
		EVENT_SYSTEM_MENUPOPUPSTART = 0x06,
		EVENT_SYSTEM_MENUPOPUPEND = 0x07,
		/*
		 * EVENT_SYSTEM_CAPTURESTART
		 * EVENT_SYSTEM_CAPTUREEND
		 * Sent when a window takes the capture and releases the capture.
		 */
		EVENT_SYSTEM_CAPTURESTART = 0x08,
		EVENT_SYSTEM_CAPTUREEND = 0x09,
		/*
		 * Move Size
		 * EVENT_SYSTEM_MOVESIZESTART
		 * EVENT_SYSTEM_MOVESIZEEND
		 * Sent when a window enters and leaves move-size dragging mode.
		 */
		EVENT_SYSTEM_MOVESIZESTART = 0x0A,
		EVENT_SYSTEM_MOVESIZEEND = 0x0B,
		/*
		 * Context Help
		 * EVENT_SYSTEM_CONTEXTHELPSTART
		 * EVENT_SYSTEM_CONTEXTHELPEND
		 * Sent when a window enters and leaves context sensitive help mode.
		 */
		EVENT_SYSTEM_CONTEXTHELPSTART = 0x0C,
		EVENT_SYSTEM_CONTEXTHELPEND = 0x0D,
		/*
		 * Drag & Drop
		 * EVENT_SYSTEM_DRAGDROPSTART
		 * EVENT_SYSTEM_DRAGDROPEND
		 * Send the START notification just before going into drag&drop loop.  Send
		 * the END notification just after canceling out.
		 * Note that it is up to apps and OLE to generate this, since the system
		 * doesn't know.  Like EVENT_SYSTEM_SOUND, it will be a while before this
		 * is prevalent.
		 */
		EVENT_SYSTEM_DRAGDROPSTART = 0x0E,
		EVENT_SYSTEM_DRAGDROPEND = 0x0F,
		/*
		 * Dialog
		 * Send the START notification right after the dialog is completely
		 *  initialized and visible.  Send the END right before the dialog
		 *  is hidden and goes away.
		 * EVENT_SYSTEM_DIALOGSTART
		 * EVENT_SYSTEM_DIALOGEND
		 */
		EVENT_SYSTEM_DIALOGSTART = 0x10,
		EVENT_SYSTEM_DIALOGEND = 0x11,
		/*
		 * EVENT_SYSTEM_SCROLLING
		 * EVENT_SYSTEM_SCROLLINGSTART
		 * EVENT_SYSTEM_SCROLLINGEND
		 * Sent when beginning and ending the tracking of a scrollbar in a window,
		 * and also for scrollbar controls.
		 */
		EVENT_SYSTEM_SCROLLINGSTART = 0x12,
		EVENT_SYSTEM_SCROLLINGEND = 0x13,
		/*
		 * Alt-Tab Window
		 * Send the START notification right after the switch window is initialized
		 * and visible.  Send the END right before it is hidden and goes away.
		 * EVENT_SYSTEM_SWITCHSTART
		 * EVENT_SYSTEM_SWITCHEND
		 */
		EVENT_SYSTEM_SWITCHSTART = 0x14,
		EVENT_SYSTEM_SWITCHEND = 0x15,

		/*
		 * EVENT_SYSTEM_MINIMIZESTART
		 * EVENT_SYSTEM_MINIMIZEEND
		 * Sent when a window minimizes and just before it restores.
		 */
		EVENT_SYSTEM_MINIMIZESTART = 0x16,
		EVENT_SYSTEM_MINIMIZEEND = 0x17,

		EVENT_SYSTEM_DESKTOPSWITCH = 0x20,

		// AppGrabbed: HWND = hwnd of app thumbnail, objectID = 0, childID = 0
		EVENT_SYSTEM_SWITCHER_APPGRABBED = 0x0024,

		// OverTarget: HWND = hwnd of app thumbnail, objectID =
		//            1 for center
		//            2 for near snapped
		//            3 for far snapped
		//            4 for prune
		//            childID = 0
		EVENT_SYSTEM_SWITCHER_APPOVERTARGET = 0x0025,
		// Dropped: HWND = hwnd of app thumbnail, objectID = <same as above>, childID = 0
		EVENT_SYSTEM_SWITCHER_APPDROPPED = 0x0026,
		// Cancelled: HWND = hwnd of app thumbnail, objectID = 0, childID = 0
		EVENT_SYSTEM_SWITCHER_CANCELLED = 0x0027,
		/*
		 * Sent when an IME's soft key is pressed and should be echoed,
		 * but is not passed through the keyboard hook.
		 * Must not be sent when a key is sent through the keyboard hook.
		 *     HWND             is the hwnd of the UI containing the soft key
		 *     idChild          is the Unicode value of the character entered
		 *     idObject         is a bitfield
		 *         0x00000001: set if a 32-bit Unicode surrogate pair is used
		 */
		EVENT_SYSTEM_IME_KEY_NOTIFICATION = 0x0029,

		EVENT_SYSTEM_END = 0x00FF,
		EVENT_OEM_DEFINED_START = 0x0101,
		EVENT_OEM_DEFINED_END = 0x01FF,

		// Console events
		EVENT_CONSOLE_CARET = 0x4001,
		EVENT_CONSOLE_UPDATE_REGION = 0x4002,
		EVENT_CONSOLE_UPDATE_SIMPLE = 0x4003,
		EVENT_CONSOLE_UPDATE_SCROLL = 0x4004,
		EVENT_CONSOLE_LAYOUT = 0x4005,
		EVENT_CONSOLE_START_APPLICATION = 0x4006,
		EVENT_CONSOLE_END_APPLICATION = 0x4007,

		EVENT_UIA_EVENTID_START = 0x4E00,
		EVENT_UIA_EVENTID_END = 0x4EFF,

		EVENT_UIA_PROPID_START = 0x7500,
		EVENT_UIA_PROPID_END = 0x75FF,


		/*
		 * Object events
		 *
		 * The system AND apps generate these.  The system generates these for
		 * real windows.  Apps generate these for objects within their window which
		 * act like a separate control, e.g. an item in a list view.
		 *
		 * When the system generate them, dwParam2 is always WMOBJID_SELF.  When
		 * apps generate them, apps put the has-meaning-to-the-app-only ID value
		 * in dwParam2.
		 * For all events, if you want detailed accessibility information, callers
		 * should
		 *      * Call AccessibleObjectFromWindow() with the hwnd, idObject parameters
		 *          of the event, and IID_IAccessible as the REFIID, to get back an
		 *          IAccessible* to talk to
		 *      * Initialize and fill in a VARIANT as VT_I4 with lVal the idChild
		 *          parameter of the event.
		 *      * If idChild isn't zero, call get_accChild() in the container to see
		 *          if the child is an object in its own right.  If so, you will get
		 *          back an IDispatch* object for the child.  You should release the
		 *          parent, and call QueryInterface() on the child object to get its
		 *          IAccessible*.  Then you talk directly to the child.  Otherwise,
		 *          if get_accChild() returns you nothing, you should continue to
		 *          use the child VARIANT.  You will ask the container for the properties
		 *          of the child identified by the VARIANT.  In other words, the
		 *          child in this case is accessible but not a full-blown object.
		 *          Like a button on a titlebar which is 'small' and has no children.
		 * For all EVENT_OBJECT events,
		 *      hwnd is the dude to Send the WM_GETOBJECT message to (unless NULL,
		 *          see above for system things)
		 *      idObject is the ID of the object that can resolve any queries a
		 *          client might have.  It's a way to deal with windowless controls,
		 *          controls that are just drawn on the screen in some larger parent
		 *          window (like SDM), or standard frame elements of a window.
		 *      idChild is the piece inside of the object that is affected.  This
		 *          allows clients to access things that are too small to have full
		 *          blown objects in their own right.  Like the thumb of a scrollbar.
		 *          The hwnd/idObject pair gets you to the container, the dude you
		 *          probably want to talk to most of the time anyway.  The idChild
		 *          can then be passed into the acc properties to get the name/value
		 *          of it as needed.
		 *
		 * Example #1:
		 *      System propagating a listbox selection change
		 *      EVENT_OBJECT_SELECTION
		 *          hwnd == listbox hwnd
		 *          idObject == OBJID_WINDOW
		 *          idChild == new selected item, or CHILDID_SELF if
		 *              nothing now selected within container.
		 *      Word '97 propagating a listbox selection change
		 *          hwnd == SDM window
		 *          idObject == SDM ID to get at listbox 'control'
		 *          idChild == new selected item, or CHILDID_SELF if
		 *              nothing
		 *
		 * Example #2:
		 *      System propagating a menu item selection on the menu bar
		 *      EVENT_OBJECT_SELECTION
		 *          hwnd == top level window
		 *          idObject == OBJID_MENU
		 *          idChild == ID of child menu bar item selected
		 *
		 * Example #3:
		 *      System propagating a dropdown coming off of said menu bar item
		 *      EVENT_OBJECT_CREATE
		 *          hwnd == popup item
		 *          idObject == OBJID_WINDOW
		 *          idChild == CHILDID_SELF
		 *
		 * Example #4:
		 *
		 * For EVENT_OBJECT_REORDER, the object referred to by hwnd/idObject is the
		 * PARENT container in which the zorder is occurring.  This is because if
		 * one child is zordering, all of them are changing their relative zorder.
		 */
		EVENT_OBJECT_CREATE = 0x8000, // hwnd + ID + idChild is created item
		EVENT_OBJECT_DESTROY = 0x8001, // hwnd + ID + idChild is destroyed item
		EVENT_OBJECT_SHOW = 0x8002, // hwnd + ID + idChild is shown item
		EVENT_OBJECT_HIDE = 0x8003, // hwnd + ID + idChild is hidden item
		EVENT_OBJECT_REORDER = 0x8004, // hwnd + ID + idChild is parent of zordering children
		EVENT_OBJECT_FOCUS = 0x8005, // hwnd + ID + idChild is focused item
		/*
		 * NOTES:
		 * There is only one "focused" child item in a parent.  This is the place
		 * keystrokes are going at a given moment.  Hence only send a notification
		 * about where the NEW focus is going.  A NEW item getting the focus already
		 * implies that the OLD item is losing it.
		 *
		 * SELECTION however can be multiple.  Hence the different SELECTION
		 * notifications.  Here's when to use each:
		 *
		 * (1) Send a SELECTION notification in the simple single selection
		 *     case (like the focus) when the item with the selection is
		 *     merely moving to a different item within a container.  hwnd + ID
		 *     is the container control, idChildItem is the new child with the
		 *     selection.
		 *
		 * (2) Send a SELECTIONADD notification when a new item has simply been added
		 *     to the selection within a container.  This is appropriate when the
		 *     number of newly selected items is very small.  hwnd + ID is the
		 *     container control, idChildItem is the new child added to the selection.
		 *
		 * (3) Send a SELECTIONREMOVE notification when a new item has simply been
		 *     removed from the selection within a container.  This is appropriate
		 *     when the number of newly selected items is very small, just like
		 *     SELECTIONADD.  hwnd + ID is the container control, idChildItem is the
		 *     new child removed from the selection.
		 *
		 * (4) Send a SELECTIONWITHIN notification when the selected items within a
		 *     control have changed substantially.  Rather than propagate a large
		 *     number of changes to reflect removal for some items, addition of
		 *     others, just tell somebody who cares that a lot happened.  It will
		 *     be faster an easier for somebody watching to just turn around and
		 *     query the container control what the new bunch of selected items
		 *     are.
		 */
		EVENT_OBJECT_SELECTION = 0x8006, // hwnd + ID + idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex
		EVENT_OBJECT_SELECTIONADD = 0x8007, // hwnd + ID + idChild is item added
		EVENT_OBJECT_SELECTIONREMOVE = 0x8008, // hwnd + ID + idChild is item removed
		EVENT_OBJECT_SELECTIONWITHIN = 0x8009, // hwnd + ID + idChild is parent of changed selected items

		/*
		 * Examples of when to send an EVENT_OBJECT_STATECHANGE include
		 *      * It is being enabled/disabled (USER does for windows)
		 *      * It is being pressed/released (USER does for buttons)
		 *      * It is being checked/unchecked (USER does for radio/check buttons)
		 */
		EVENT_OBJECT_STATECHANGE = 0x800A,
		/*
		 * Note:
		 * A LOCATIONCHANGE is not sent for every child object when the parent
		 * changes shape/moves.  Send one notification for the topmost object
		 * that is changing.  For example, if the user resizes a top level window,
		 * USER will generate a LOCATIONCHANGE for it, but not for the menu bar,
		 * title bar, scrollbars, etc.  that are also changing shape/moving.
		 *
		 * In other words, it only generates LOCATIONCHANGE notifications for
		 * real windows that are moving/sizing.  It will not generate a LOCATIONCHANGE
		 * for every non-floating child window when the parent moves (the children are
		 * logically moving also on screen, but not relative to the parent).
		 *
		 * Now, if the app itself resizes child windows as a result of being
		 * sized, USER will generate LOCATIONCHANGEs for those dudes also because
		 * it doesn't know better.
		 *
		 * Note also that USER will generate LOCATIONCHANGE notifications for two
		 * non-window sys objects:
		 *      (1) System caret
		 *      (2) Cursor
		 */
		EVENT_OBJECT_LOCATIONCHANGE = 0x800B,
		EVENT_OBJECT_NAMECHANGE = 0x800C, // hwnd + ID + idChild is item w/ name change
		EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D, // hwnd + ID + idChild is item w/ desc change
		EVENT_OBJECT_VALUECHANGE = 0x800E, // hwnd + ID + idChild is item w/ value change
		EVENT_OBJECT_PARENTCHANGE = 0x800F, // hwnd + ID + idChild is item w/ new parent
		EVENT_OBJECT_HELPCHANGE = 0x8010, // hwnd + ID + idChild is item w/ help change
		EVENT_OBJECT_DEFACTIONCHANGE = 0x8011, // hwnd + ID + idChild is item w/ def action change
		EVENT_OBJECT_ACCELERATORCHANGE = 0x8012, // hwnd + ID + idChild is item w/ keybd accel change

		EVENT_OBJECT_INVOKED = 0x8013, // hwnd + ID + idChild is item invoked
		EVENT_OBJECT_TEXTSELECTIONCHANGED = 0x8014, // hwnd + ID + idChild is item w? test selection change
		/*
		 * EVENT_OBJECT_CONTENTSCROLLED
		 * Sent when ending the scrolling of a window object.
		 *
		 * Unlike the similar event (EVENT_SYSTEM_SCROLLEND), this event will be
		 * associated with the scrolling window itself. There is no difference
		 * between horizontal or vertical scrolling.
		 *
		 * This event should be posted whenever scroll action is completed, including
		 * when it is scrolled by scroll bars, mouse wheel, or keyboard navigations.
		 *
		 *   example:
		 *          hwnd == window that is scrolling
		 *          idObject == OBJID_CLIENT
		 *          idChild == CHILDID_SELF
		 */
		EVENT_OBJECT_CONTENTSCROLLED = 0x8015,
		EVENT_SYSTEM_ARRANGMENTPREVIEW = 0x8016,
		/*
		 * EVENT_OBJECT_CLOAKED / UNCLOAKED
		 * Sent when a window is cloaked or uncloaked.
		 * A cloaked window still exists, but is invisible to
		 * the user.
		 */
		EVENT_OBJECT_CLOAKED = 0x8017,
		EVENT_OBJECT_UNCLOAKED = 0x8018,

		/*
		 * EVENT_OBJECT_LIVEREGIONCHANGED
		 * Sent when an object that is part of a live region
		 * changes.  A live region is an area of an application
		 * that changes frequently and/or asynchronously, so
		 * that an assistive technology tool might want to pay
		 * special attention to it.
		 */
		EVENT_OBJECT_LIVEREGIONCHANGED = 0x8019,

		/*
		 * EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED
		 * Sent when a window that is hosting other Accessible
		 * objects changes the hosted objects.  A client may
		 * wish to requery to see what the new hosted objects are,
		 * especially if it has been monitoring events from this
		 * window.  A hosted object is one with a different Accessibility
		 * framework (MSAA or UI Automation) from its host.
		 *
		 * Changes in hosted objects with the *same* framework
		 * as the parent should be handed with the usual structural
		 * change events, such as EVENT_OBJECT_CREATED for MSAA.
		 * see above.
		 */
		EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED = 0x8020,

		/*
		 * Drag / Drop Events
		 * These events are used in conjunction with the
		 * UI Automation Drag/Drop patterns.
		 *
		 * For DRAGSTART, DRAGCANCEL, and DRAGCOMPLETE,
		 * HWND+objectID+childID refers to the object being dragged.
		 *
		 * For DRAGENTER, DRAGLEAVE, and DRAGDROPPED,
		 * HWND+objectID+childID refers to the target of the drop
		 * that is being hovered over.
		 */

		EVENT_OBJECT_DRAGSTART = 0x8021,
		EVENT_OBJECT_DRAGCANCEL = 0x8022,
		EVENT_OBJECT_DRAGCOMPLETE = 0x8023,

		EVENT_OBJECT_DRAGENTER = 0x8024,
		EVENT_OBJECT_DRAGLEAVE = 0x8025,
		EVENT_OBJECT_DRAGDROPPED = 0x8026,

		/*
		 * EVENT_OBJECT_IME_SHOW/HIDE
		 * Sent by an IME window when it has become visible or invisible.
		 */
		EVENT_OBJECT_IME_SHOW = 0x8027,
		EVENT_OBJECT_IME_HIDE = 0x8028,

		/*
		 * EVENT_OBJECT_IME_CHANGE
		 * Sent by an IME window whenever it changes size or position.
		 */
		EVENT_OBJECT_IME_CHANGE = 0x8029,

		EVENT_OBJECT_END = 0x80FF,

		EVENT_AIA_START = 0xA000,
		EVENT_AIA_END = 0xAFFF,
		EVENT_MAX = 0x7FFFFFFF
	}
}