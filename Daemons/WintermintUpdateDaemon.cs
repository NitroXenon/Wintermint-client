// Decompiled with JetBrains decompiler
// Type: WintermintClient.Daemons.WintermintUpdateDaemon
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.Data;

namespace WintermintClient.Daemons
{
  internal class WintermintUpdateDaemon
  {
    private static TimeSpan InitialUpdateDelay = TimeSpan.FromMinutes(5.0);
    private static TimeSpan UpdateDelay = TimeSpan.FromHours(1.0);
    private const string ExecutableName = "wintermint-update";

    public void Initialize()
    {
      this.RunLoop();
    }

    public async Task RunLoop()
    {
      await Task.Delay(WintermintUpdateDaemon.InitialUpdateDelay);
      TaskAwaiter awaiter;
      while (true)
      {
        LaunchData.TryLaunch("wintermint-update", (string) null);
        awaiter = Task.Delay(WintermintUpdateDaemon.UpdateDelay).GetAwaiter();
        if (awaiter.IsCompleted)
        {
          awaiter.GetResult();
          awaiter = new TaskAwaiter();
        }
        else
          break;
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003C\u003E1__state = 1;
      TaskAwaiter taskAwaiter = awaiter;
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, WintermintUpdateDaemon.\u003CRunLoop\u003Ed__0>(ref awaiter, this);
    }
  }
}
