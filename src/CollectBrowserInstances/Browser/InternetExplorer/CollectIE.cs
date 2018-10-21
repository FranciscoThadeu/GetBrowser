using CollectBrowserInstances.Models;
using CollectBrowserInstances.Native.Windows;
using System.Collections.Generic;

namespace CollectBrowserInstances.Browser.InternetExplorer
{
    public class CollectIE
    {

        public static List<BrowserIds> ListCollectInternetExplorerInstances()
        {

            var enumerator = new WindowsEnumerator();
            List<BrowserIds> _browser = new List<BrowserIds>();

            var topLevelWindows = enumerator.GetTopLevelWindows("IEFrame");
            foreach (var mainBrowserWindow in topLevelWindows)
            {
                var windows = enumerator.GetChildWindows(mainBrowserWindow.Hwnd, "TabWindowClass");

                // IE6 has no TabWindowClass so use the IEFrame as starting point
                if (windows.Count == 0)
                {
                    windows.Add(mainBrowserWindow);
                }

                foreach (var window in windows)
                {


                    _browser.Add(new BrowserIds
                    {
                        ProcessID = window.ProcessID,
                        HWND = window.Hwnd.ToString(),
                        Title = window.Title
                    });
                }
            }

            return _browser;
        }

    }
}
