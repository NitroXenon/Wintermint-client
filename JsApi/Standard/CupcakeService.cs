// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.CupcakeService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.JsApi.ApiHost;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("cupcake")]
  public class CupcakeService : JsApiService
  {
    [MicroApiMethod("follow")]
    public async Task RequestGameStateTracking(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CupcakeService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (CupcakeService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CupcakeService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site3;
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "matchId", typeof (CupcakeService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site4.Target((CallSite) CupcakeService.\u003CRequestGameStateTracking\u003Eo__SiteContainer0.\u003C\u003Ep__Site4, args);
      string matchId = func2((CallSite) callSite2, obj2);
      string uri = string.Format("https://cupcake.fresh.wintermint.net/{0}/match/{1}/track", (object) realm, (object) matchId);
      HttpClient http = new HttpClient();
      HttpResponseMessage httpResponseMessage = await http.PostAsync(uri, (HttpContent) new StringContent("{ \"notifications\": false }"));
    }

    [MicroApiMethod("state.get")]
    public async Task<JsonResult> GetLiveGameState(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sited == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CupcakeService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sited.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sited;
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (CupcakeService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee.Target((CallSite) CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CupcakeService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef;
      // ISSUE: reference to a compiler-generated field
      if (CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "matchId", typeof (CupcakeService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Site10.Target((CallSite) CupcakeService.\u003CGetLiveGameState\u003Eo__SiteContainerc.\u003C\u003Ep__Site10, args);
      string matchId = func2((CallSite) callSite2, obj2);
      string uri = string.Format("https://cupcake.fresh.wintermint.net/{0}/match/{1}/state", (object) realm, (object) matchId);
      WebClient http = new WebClient();
      string json = await http.DownloadStringTaskAsync(uri);
      return new JsonResult(json);
    }
  }
}
