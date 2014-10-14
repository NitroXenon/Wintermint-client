// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.WintermintJsApiServiceHelper
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Threading.Tasks;

namespace WintermintClient.JsApi
{
  public static class WintermintJsApiServiceHelper
  {
    public static void PropagateExceptions(this Task task)
    {
      if (task == null)
        throw new ArgumentNullException("task");
      if (!task.IsCompleted)
        throw new InvalidOperationException("Task has not completed.");
      if (!task.IsFaulted)
        return;
      task.Wait();
    }
  }
}
