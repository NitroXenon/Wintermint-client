// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.Riot.RealmService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintData.Riot;

namespace WintermintClient.JsApi.Standard.Riot
{
  [MicroApiService("riot")]
  public class RealmService : JsApiService
  {
    public static Task<SupportedRealm[]> GetRealms()
    {
      return JsApiService.Client.InvokeCached<SupportedRealm[]>("riot.realms.get");
    }

    [MicroApiMethod("realms")]
    public static async Task<object> GetRealmsClient()
    {
      SupportedRealm[] realms = await JsApiService.Client.InvokeCached<SupportedRealm[]>("riot.realms.get");
      return (object) Enumerable.OrderBy<SupportedRealm, string>((IEnumerable<SupportedRealm>) realms, (Func<SupportedRealm, string>) (x => x.Name));
    }
  }
}
