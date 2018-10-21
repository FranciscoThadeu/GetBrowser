using System;
using System.Collections.Generic;

namespace CollectBrowserInstances.Native.Windows
{
    public class WindowsEnumerator
    {
        public delegate bool WindowEnumConstraint(Window window);

        /// <summary> 
        /// Get all top-level window information 
        /// </summary> 
        /// <returns>List of window information objects</returns> 
        public IList<Window> GetTopLevelWindows()
        {
            return GetTopLevelWindows(null);
        }

        public IList<Window> GetTopLevelWindows(string className)
        {
            return GetWindows(window => !window.HasParentWindow && NativeMethods.CompareClassNames(window.Hwnd, className));
        }

        public IList<Window> GetWindows(WindowEnumConstraint constraint)
        {
            var windows = new List<Window>();

            NativeMethods.EnumWindows((hwnd, lParam) =>
            {
                var window = new Window(hwnd);
                if (constraint == null || constraint(window))
                    windows.Add(window);

                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> 
        /// Get all child windows for the specific windows handle (hwnd). 
        /// </summary> 
        /// <returns>List of child windows for parent window</returns> 
        public IList<Window> GetChildWindows(IntPtr hwnd)
        {
            return GetChildWindows(hwnd, (string)null);
        }

        public IList<Window> GetChildWindows(IntPtr hwnd, string childClass)
        {
            return GetChildWindows(hwnd, window => NativeMethods.CompareClassNames(window.Hwnd, childClass));
        }

        public IList<Window> GetChildWindows(IntPtr hwnd, WindowEnumConstraint constraint)
        {
            var childWindows = new List<Window>();

            NativeMethods.EnumChildWindows(hwnd, (childHwnd, lParam) =>
            {
                var childWindow = new Window(childHwnd);
                if (constraint == null || constraint(childWindow))
                    childWindows.Add(childWindow);

                return true;
            }, IntPtr.Zero);

            return childWindows;
        }
    }
}
