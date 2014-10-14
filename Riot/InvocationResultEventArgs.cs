// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.InvocationResultEventArgs
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;

namespace WintermintClient.Riot
{
  public class InvocationResultEventArgs : EventArgs
  {
    public string Service;
    public string Method;
    public bool Success;
    public object Result;

    public InvocationResultEventArgs(string service, string method, bool success, object result)
    {
      this.Service = service;
      this.Method = method;
      this.Success = success;
      this.Result = result;
    }
  }
}
