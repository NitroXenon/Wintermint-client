// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.LaunchData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WintermintClient.Data
{
  internal static class LaunchData
  {
    private static int loadAttempts;

    public static string ApplicationDirectory { get; private set; }

    private static string ApplicationReleasesDirectory { get; set; }

    public static string RootDirectory { get; private set; }

    public static string DataDirectory { get; private set; }

    public static string RiotContainerDirectory { get; private set; }

    public static string LauncherExecutable { get; private set; }

    public static string WintermintRootExecutable { get; private set; }

    public static string GetTemporaryFolder(string purpose)
    {
      return Path.Combine(Path.GetTempPath(), string.Format("{0:N}{1:N}", (object) Guid.NewGuid(), (object) Guid.NewGuid()));
    }

    public static void Launch(string name, string arguments = null)
    {
      Process.Start(new ProcessStartInfo()
      {
        FileName = LaunchData.LauncherExecutable,
        Arguments = string.Format("{0} {1}", (object) name, (object) arguments),
        UseShellExecute = false
      });
    }

    public static bool TryLaunch(string name, string arguments = null)
    {
      try
      {
        LaunchData.Launch(name, arguments);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static void LaunchLocal(string name, string arguments = null)
    {
      Process.Start(new ProcessStartInfo()
      {
        FileName = Path.Combine(LaunchData.ApplicationDirectory, name),
        Arguments = arguments,
        UseShellExecute = false
      });
    }

    public static bool TryLaunchLocal(string name, string arguments = null)
    {
      try
      {
        LaunchData.LaunchLocal(name, arguments);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static void Initialize()
    {
      if (Interlocked.Increment(ref LaunchData.loadAttempts) > 1)
        return;
      DirectoryInfo directory = new FileInfo(Assembly.GetEntryAssembly().Location).Directory;
      LaunchData.ApplicationDirectory = directory.FullName;
      DirectoryInfo parent = directory.Parent;
      LaunchData.ApplicationReleasesDirectory = parent.FullName;
      LaunchData.RootDirectory = parent.Parent.FullName;
      LaunchData.DataDirectory = Path.Combine(LaunchData.RootDirectory, "data");
      LaunchData.RiotContainerDirectory = Path.Combine(LaunchData.RootDirectory, "game");
      LaunchData.LauncherExecutable = Path.Combine(LaunchData.RootDirectory, "launcher");
      LaunchData.WintermintRootExecutable = Path.Combine(LaunchData.RootDirectory, "wintermint.exe");
      Directory.CreateDirectory(LaunchData.ApplicationDirectory);
      Directory.CreateDirectory(LaunchData.ApplicationReleasesDirectory);
      Directory.CreateDirectory(LaunchData.RootDirectory);
      Directory.CreateDirectory(LaunchData.DataDirectory);
      Directory.CreateDirectory(LaunchData.RiotContainerDirectory);
      if ((!"application".Equals(new DirectoryInfo(LaunchData.ApplicationReleasesDirectory).Name, StringComparison.OrdinalIgnoreCase) || !"data".Equals(new DirectoryInfo(LaunchData.DataDirectory).Name, StringComparison.OrdinalIgnoreCase) || (!File.Exists(LaunchData.LauncherExecutable) || !File.Exists(LaunchData.WintermintRootExecutable)) ? 0 : (File.Exists(Path.Combine(LaunchData.ApplicationDirectory, "clean")) ? 1 : 0)) != 0)
        return;
      int num = (int) MessageBox.Show("Please re-install Wintermint. Some important files and folders are missing.", "Wintermint", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Environment.Exit(0);
    }
  }
}
