using System;
using System.Windows.Forms;

namespace MiNa.InventorAddInDebugger.Common
{
    /// <summary>Converts hWnd to IWin32Window</summary>
    /// <remarks>Example:
    /// Form.Show(new WindowWrapper(ThisApplication.MainFrameHWND))</remarks>
    public class WindowWrapper : IWin32Window
    {
        private IntPtr _hwnd;

        /// <summary>Constructor</summary>
        /// <param name="handle">Inventor.Application.MainFrameHWND</param>
        /// <remarks></remarks>
        public WindowWrapper(IntPtr handle) => this._hwnd = handle;

        /// <summary>Main window handle</summary>
        public IntPtr Handle => this._hwnd;
    }
}