using System;
using System.Runtime.InteropServices;

namespace Sens_Amethyst_Recoil {
  class DLLImports {
    [DllImport("user32.dll")] public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    [DllImport("user32.dll")] public static extern short GetAsyncKeyState(int vKey);
    [DllImport("Kernel32.dll")] public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
    [DllImport("Kernel32.dll")] public static extern bool QueryPerformanceFrequency(out long lpFrequency);
    [DllImport("user32.dll")] public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
  }
}
