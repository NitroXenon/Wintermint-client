// Decompiled with JetBrains decompiler
// Type: WintermintClient.Util.AppUserModelId
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using WintermintClient.Data;

namespace WintermintClient.Util
{
  internal static class AppUserModelId
  {
    public static void SetWintermintProperties(IntPtr windowHandle)
    {
      LaunchData.Initialize();
      WindowPropertyValue[] properties = new WindowPropertyValue[3]
      {
        new WindowPropertyValue()
        {
          Key = SystemProperties.System.AppUserModel.ID,
          Value = "astralfoxy.wintermint.client"
        },
        new WindowPropertyValue()
        {
          Key = SystemProperties.System.AppUserModel.RelaunchCommand,
          Value = LaunchData.WintermintRootExecutable
        },
        new WindowPropertyValue()
        {
          Key = SystemProperties.System.AppUserModel.RelaunchDisplayNameResource,
          Value = "Wintermint"
        }
      };
      WindowProperties.SetWindowProperties(windowHandle, properties);
    }
  }
}
