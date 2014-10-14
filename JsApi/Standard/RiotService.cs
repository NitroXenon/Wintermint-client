// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.RiotService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("riot")]
  public class RiotService : JsApiService
  {
    [MicroApiMethod("storeUri")]
    public async Task<string> GetStoreUri(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (RiotService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "handle", typeof (RiotService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) RiotService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      int handle = func((CallSite) callSite, obj);
      RiotAccount account = JsApiService.AccountBag.Get(handle);
      return await account.InvokeAsync<string>("loginService", "getStoreUrl");
    }

    [MicroApiMethod("isOnline")]
    public async Task<bool> IsOnline(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "loginService";
      string method = "isLoggedIn";
      // ISSUE: reference to a compiler-generated field
      if (RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (RiotService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Site9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Site9;
      // ISSUE: reference to a compiler-generated field
      if (RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "username", typeof (RiotService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea.Target((CallSite) RiotService.\u003CIsOnline\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea, args);
      string str = func((CallSite) callSite, obj);
      return await riotAccount.InvokeAsync<bool>(destination, method, (object) str);
    }
  }
}
