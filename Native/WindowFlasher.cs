// Decompiled with JetBrains decompiler
// Type: WintermintClient.Native.WindowFlasher
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Runtime.InteropServices;

namespace WintermintClient.Native
{
  public static class WindowFlasher
  {
    private static uint FLASHWINFO_SIZE = (uint) Marshal.SizeOf(typeof (WindowFlasher.FLASHWINFO));
    private const uint FLASHW_ALL = 3U;
    private const uint FLASHW_CAPTION = 1U;
    private const uint FLASHW_STOP = 0U;
    private const uint FLASHW_TIMER = 4U;
    private const uint FLASHW_TIMERNOFG = 12U;
    private const uint FLASHW_TRAY = 2U;

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FlashWindowEx(ref WindowFlasher.FLASHWINFO pwfi);

    private static void DoFlash(IntPtr handle, uint flags, uint count)
    {
      if (WindowFlasher.GetForegroundWindow() == handle)
        return;
      WindowFlasher.FLASHWINFO pwfi = new WindowFlasher.FLASHWINFO()
      {
        cbSize = WindowFlasher.FLASHWINFO_SIZE,
        dwTimeout = 0U,
        hwnd = handle,
        dwFlags = flags,
        uCount = count
      };
      WindowFlasher.FlashWindowEx(ref pwfi);
    }

    public static void Pulse(IntPtr handle)
    {
      WindowFlasher.DoFlash(handle, 15U, uint.MaxValue);
    }

    public static void Stop(IntPtr handle)
    {
      WindowFlasher.DoFlash(handle, 0U, 0U);
    }

    public static void Flash(IntPtr handle)
    {
      WindowFlasher.DoFlash(handle, 3U, 3U);
    }

    public static void Flash(IntPtr handle, int count)
    {
      WindowFlasher.DoFlash(handle, 3U, (uint) count);
    }

    private struct FLASHWINFO
    {
      public uint cbSize;
      public IntPtr hwnd;
      public uint dwFlags;
      public uint uCount;
      public uint dwTimeout;
    }
  }
}
