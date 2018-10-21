using System.Runtime.InteropServices;

namespace CollectBrowserInstances.Native.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowInfo
    {
        public uint cbSize;
        public Rect rcWindow;
        public Rect rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;
    }
}
