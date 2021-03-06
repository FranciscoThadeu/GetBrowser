﻿using System.Runtime.InteropServices;

namespace CollectBrowserInstances.Native.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPlacement
    {
        public int length;
        public int flags;
        public int showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public Rect rcNormalPosition;
    }
}
