using System.Runtime.InteropServices;

namespace CollectBrowserInstances.Native.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int X;
        public int Y;
    }
}
