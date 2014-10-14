// Decompiled with JetBrains decompiler
// Type: WintermintClient.Program
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Browser;
using System;
using System.IO;
using System.Windows.Forms;
using WintermintClient.Data;

namespace WintermintClient
{
  public class Program
  {
    [STAThread]
    public static void Main(string[] args)
    {
      if (!BrowserEngine.Initialize(args))
        return;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      using (Program.TryAcquireLock())
      {
        AppContainer appContainer = new AppContainer();
        appContainer.Initialize();
        Application.Run(appContainer.window as Form);
      }
      BrowserEngine.Shutdown();
    }

    private static IDisposable TryAcquireLock()
    {
      LaunchData.Initialize();
      string path = Path.Combine(LaunchData.DataDirectory, "run-lock");
      try
      {
        return (IDisposable) new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
      }
      catch
      {
        int num = (int) MessageBox.Show("Wintermint is already running or is shutting down (sorry).\n\nI will do my best to remove this message soon.\n\n-- astralfoxy", "Wintermint", MessageBoxButtons.OK);
        Environment.Exit(0);
        return (IDisposable) null;
      }
    }
  }
}
