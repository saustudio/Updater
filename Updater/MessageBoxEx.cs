using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

//[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode = true)]

namespace MessageEx
{
    public class MessageBoxEx
    {
        public static DialogResult Show(string text, uint uTimeout)
        {
            Setup("", uTimeout);
            return MessageBox.Show(text);
        }

        public static DialogResult Show(string text, string caption, uint uTimeout)
        {
            Setup(caption, uTimeout);
            return MessageBox.Show(text, caption);
        }



        public static DialogResult ShowInfo(string text, string caption, uint uTimeout)
        {
            Setup(caption, uTimeout);
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string text, string caption, uint uTimeout)
        {
            Setup(caption, uTimeout);
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(string text, string caption, uint uTimeout)
        {
            Setup(caption, uTimeout);
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        //      public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        //public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        //public const int WH_CALLWNDPROCRET = 12;
        //public const int WM_DESTROY = 0x0002;
        //public const int WM_INITDIALOG = 0x0110;
        //public const int WM_TIMER = 0x0113;
        //public const int WM_USER = 0x400;
        //public const int DM_GETDEFID = WM_USER + 0;

        //[DllImport("User32.dll")]
        //public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        //[DllImport("User32.dll")]
        //public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //[DllImport("user32.dll")]
        //public static extern int UnhookWindowsHookEx(IntPtr idHook);

        //[DllImport("user32.dll")]
        //public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern int GetWindowTextLength(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        //[DllImport("user32.dll")]
        //public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        //      [DllImport("kernel32.dll")]
        //      public static extern int GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };

        private const int TimerID = 42;
        //private static HookProc hookProc;
        //private static TimerProc hookTimer;
        private static uint hookTimeout;
        private static string hookCaption;
        private static IntPtr hHook;

        static MessageBoxEx()
        {
            //hookProc = new HookProc(MessageBoxHookProc);
            //hookTimer = new TimerProc(MessageBoxTimerProc);
            hookTimeout = 0;
            hookCaption = null;
            hHook = IntPtr.Zero;
        }

        private static void Setup(string caption, uint uTimeout)
        {
#warning no implemented
            //if (hHook != IntPtr.Zero)
            //	throw new NotSupportedException("multiple calls are not supported");

            //hookTimeout = uTimeout;
            //hookCaption = caption != null ? caption : "";
            //hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, GetCurrentThreadId());
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
#warning no implemented
            return IntPtr.Zero;
            //if (nCode < 0)
            //	return CallNextHookEx(hHook, nCode, wParam, lParam);

            //CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            //IntPtr hook = hHook;

            //if (hookCaption != null && msg.message == WM_INITDIALOG)
            //{
            //	int nLength = GetWindowTextLength(msg.hwnd);
            //	StringBuilder text = new StringBuilder(nLength + 1);

            //	GetWindowText(msg.hwnd, text, text.Capacity);

            //	if (hookCaption == text.ToString())
            //	{
            //		hookCaption = null;
            //		SetTimer(msg.hwnd, (UIntPtr)TimerID, hookTimeout, hookTimer);
            //		UnhookWindowsHookEx(hHook);
            //		hHook = IntPtr.Zero;
            //	}
            //}

            //return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void MessageBoxTimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime)
        {
#warning no implemented
            //if (nIDEvent == (UIntPtr)TimerID)
            //{
            //	short dw = (short)SendMessage(hWnd, DM_GETDEFID, IntPtr.Zero, IntPtr.Zero);

            //	EndDialog(hWnd, (IntPtr)dw);
            //}
        }
    }
}
