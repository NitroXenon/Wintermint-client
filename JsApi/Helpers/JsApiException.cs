// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Helpers.JsApiException
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;

namespace WintermintClient.JsApi.Helpers
{
  [Serializable]
  public class JsApiException : Exception
  {
    public readonly string Reason;
    public readonly object Info;

    public JsApiException(string reason)
    {
      this.Reason = reason;
    }

    public JsApiException(string className, object info)
    {
      this.Reason = className;
      this.Info = info;
    }
  }
}
