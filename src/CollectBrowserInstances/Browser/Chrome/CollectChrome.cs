using CollectBrowserInstances.Models;
using CollectBrowserInstances.Native.Windows;
using System.Collections.Generic;
using System.Linq;

namespace CollectBrowserInstances.Browser.InternetExplorer
{
    public class CollectChrome
    {

        public static List<BrowserIds> ListCollectChromeInstances()
        {
            var enumerator = new WindowsEnumerator();
            List<BrowserIds> _browser = new List<BrowserIds>();

            var topLevelWindows = enumerator.GetTopLevelWindows("Chrome_WidgetWin_1");
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


        public static bool ActivateChrome(string title)
        {
            //AutoIt.AutoItX.AutoItSetOption("WinTitleMatchMode", 2);
            var searchString = title;
            var enumerator = new WindowsEnumerator();
            List<string> tabs = new List<string>();
            var topLevelWindows = enumerator.GetTopLevelWindows("Chrome_WidgetWin_1");
            foreach (var mainBrowserWindow in topLevelWindows)
            {
                tabs = new List<string>();
                AutoIt.AutoItX.WinActivate(mainBrowserWindow.Hwnd);
                for (int i = 0; i < 50; i++)
                {
                    
                   
                     if (AutoIt.AutoItX.WinGetTitle("[ACTIVE]").Contains(searchString))
                        {
                        AutoIt.AutoItX.WinSetState(searchString, "", AutoIt.AutoItX.SW_MAXIMIZE);
                       // AutoIt.AutoItX.WinSetState(searchString, "", AutoIt.AutoItX.SW_SHOW);
                        return true;
                        //
                    }

                      AutoIt.AutoItX.Send("^{TAB}");
                    //Thread.Sleep(250);
                    if (tabs.Any() && tabs.Contains(AutoIt.AutoItX.WinGetTitle("[ACTIVE]")))
                        break;

                    tabs.Add(AutoIt.AutoItX.WinGetTitle("[ACTIVE]"));

                    

                }

            }


            return false;
        }

        //public static bool ActivateChromeTab(string title)
        //{
        //    int contInstance = 0;
        //    Process[] procsChrome = Process.GetProcessesByName("chrome");
        //    foreach (Process proc in procsChrome)
        //    {
        //        try
        //        {
        //            contInstance++;
        //            if (proc.MainWindowHandle == IntPtr.Zero)
        //            {
        //                continue;
        //            }

        //            AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
        //            Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, title);
        //            AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);

        //            TreeWalker treewalker = TreeWalker.ControlViewWalker;
        //            AutomationElement elmTabStrip = treewalker.GetParent(elmNewTab);
        //            Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
        //            var index = 0;

        //            var tabItems = elmTabStrip.FindAll(TreeScope.Children, condTabItem);

        //            var coll = new List<AutomationElement>();
        //            foreach (AutomationElement element in tabItems)
        //            {
        //                coll.Add(element);
        //            }

        //            bool NameMatch(string name)
        //            {
        //                return name == title || name.StartsWith(title + " ");
        //            }

        //            // short-circuit the search when no searched string cannot be found
        //            if (!coll.Any(e => NameMatch(e.Current.Name)))
        //            {
        //                continue;
        //            }

        //            var t = new Stopwatch();
        //            t.Start();
        //            var withPoints = coll.AsParallel().Select(e =>
        //            {
        //                var point = new System.Windows.Point(int.MaxValue, int.MaxValue);
        //                if (e.TryGetClickablePoint(out point))
        //                {

        //                }

        //                return new
        //                {
        //                    Name = e.Current.Name,
        //                    Element = e,
        //                    Point = point
        //                };
        //            }).OrderBy(e => e.Point.X);

        //            foreach (var tabItem in withPoints)
        //            {
        //                index++;
        //                var name = tabItem.Name;
        //                if (NameMatch(name))
        //                {
        //                    SetForegroundWindow(proc.MainWindowHandle); // activate window
        //                    Select(index); // select tab                
        //                    return true;
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }

        //    return false;
        //}



        //private static void Select(int tabIndex)
        //{
        //    const int maxShortcutNumber = 8;

        //    if (tabIndex <= 0) { return; }

        //    KeyDown(LCtrl);

        //    if (tabIndex <= maxShortcutNumber)
        //    {
        //        KeyPress(GetKeyNumber(tabIndex));
        //    }
        //    else
        //    {
        //        KeyPress(GetKeyNumber(maxShortcutNumber));

        //        for (var i = 0; i < tabIndex - maxShortcutNumber; i++)
        //        {
        //            Dump(i);
        //            const int timeToDigestPreviousKeyPress = 75;
        //            Thread.Sleep(timeToDigestPreviousKeyPress);
        //            KeyPress(Tab);
        //        }
        //    }
        //    KeyUp(LCtrl);
        //}
        //private static byte GetKeyNumber(int number)
        //{
        //    if (number < 0 || number > 9)
        //        throw new ApplicationException("Invalid number for key press.");

        //    return (byte)(0x30 + number);
        //}

        //private static void KeyDown(byte vKey)
        //{
        //    keybd_event(vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
        //}

        //private static void KeyUp(byte vKey)
        //{
        //    keybd_event(vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        //}

        //private static void KeyPress(byte vKey)
        //{
        //    KeyDown(vKey);
        //    KeyUp(vKey);
        //}

        //private static byte LCtrl = 0xA2; //VK_LCONTROL
        //private static byte LWin = 0x5B; //VK_LWIN
        //private static byte LAlt = 0xA4; //VK_LMENU
        //private static byte Tab = 0x09; //VK_TAB
        //private const int KEYEVENTF_EXTENDEDKEY = 1;
        //private const int KEYEVENTF_KEYUP = 2;

        //private static byte GetNumber(int number)
        //{
        //    if (number < 0 || number > 9)
        //        throw new ApplicationException("Invalid numbre for key press. Must be between 0 and 9:" + number);

        //    return (byte)(0x30 + number);
        //}

        //[DllImport("user32.dll")]
        //private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        //[DllImport("user32.dll")]
        //static extern bool SetForegroundWindow(IntPtr hWnd);

        //private static void Dump(object o)
        //{
        //    string json = JsonConvert.SerializeObject(o, Formatting.Indented);
        //    Console.WriteLine(json);
        //}
    }
}
