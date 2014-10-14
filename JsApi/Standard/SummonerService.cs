// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.SummonerService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Summoner;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("summoner")]
  public class SummonerService : GameJsApiService
  {
    [MicroApiMethod("getSummoner")]
    public async Task<object> JsGetSummoner(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      string summonerName = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site3;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site4.Target((CallSite) SummonerService.\u003CJsGetSummoner\u003Eo__SiteContainer0.\u003C\u003Ep__Site4, args);
      string realm = func2((CallSite) callSite2, obj2);
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      PublicSummoner response = await account.InvokeCachedAsync<PublicSummoner>("summonerService", "getSummonerByName", (object) summonerName);
      object obj3;
      if (response == null)
      {
        account.RemoveCached<PublicSummoner>("summonerService", "getSummonerByName", (object) summonerName);
        obj3 = (object) null;
      }
      else
        obj3 = (object) new
        {
          AccountId = response.AccountId,
          SummonerId = response.SummonerId,
          SummonerName = response.Name,
          InternalName = response.InternalName,
          Level = response.SummonerLevel,
          IconId = response.ProfileIconId
        };
      return obj3;
    }

    [MicroApiMethod("getNames")]
    public async Task<object> GetSummonerNames(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sited == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object realm = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sited.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sited, args);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, long[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long[]), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long[]> func1 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long[]>> callSite1 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitee;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerIds", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Sitef, args);
      long[] summonerIds = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, RiotAccountBag, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Get", (IEnumerable<Type>) null, typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object account = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site10.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site10, JsApiService.AccountBag, realm);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, string, string, long[], TimeSpan, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "InvokeCached", (IEnumerable<Type>) new Type[1]
        {
          typeof (string[])
        }, typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site11.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site11, account, "summonerService", "getSummonerNames", summonerIds, TimeSpan.FromMinutes(2.0));
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetAwaiter", (IEnumerable<Type>) null, typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site12.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site12, obj2);
      object obj4 = obj3;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func2 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite2 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site13;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsCompleted", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site14.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site14, obj4);
      if (!func2((CallSite) callSite2, obj5))
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = 0;
        object obj = obj3;
        ICriticalNotifyCompletion awaiter1 = obj3 as ICriticalNotifyCompletion;
        if (awaiter1 == null)
        {
          INotifyCompletion awaiter2 = (INotifyCompletion) obj3;
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitOnCompleted<INotifyCompletion, SummonerService.\u003CGetSummonerNames\u003Ed__16>(ref awaiter2, this);
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<ICriticalNotifyCompletion, SummonerService.\u003CGetSummonerNames\u003Ed__16>(ref awaiter1, this);
        }
      }
      else
      {
        object obj6 = obj3;
        // ISSUE: reference to a compiler-generated field
        if (SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site15 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetResult", (IEnumerable<Type>) null, typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        return SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site15.Target((CallSite) SummonerService.\u003CGetSummonerNames\u003Eo__SiteContainerc.\u003C\u003Ep__Site15, obj6);
      }
    }

    [MicroApiMethod("getName")]
    public async Task<object> GetSummonerName(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1d == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1d = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1d;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1e == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1e.Target((CallSite) SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1e, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1f == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1f = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> func2 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> callSite2 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site1f;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerId", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site20.Target((CallSite) SummonerService.\u003CGetSummonerName\u003Eo__SiteContainer1c.\u003C\u003Ep__Site20, args);
      long summonerId = func2((CallSite) callSite2, obj2);
      RiotAccount account = JsApiService.AccountBag.Get(realm);
      return (object) await account.InvokeCachedAsync<string[]>("summonerService", "getSummonerNames", (object) new long[1]
      {
        summonerId
      });
    }

    [MicroApiMethod("getNameByJid")]
    public Task<string> GetSummonerNameByJid(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site28 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site28 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site28.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site28;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site29 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site29 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realm", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site29.Target((CallSite) SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site29, args);
      string realm = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2a == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2a = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2a.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2a;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2b == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2b = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2b.Target((CallSite) SummonerService.\u003CGetSummonerNameByJid\u003Eo__SiteContainer27.\u003C\u003Ep__Site2b, args);
      string jid = func2((CallSite) callSite2, obj2);
      return JsApiService.GetSummonerNameByJid(realm, jid);
    }

    [MicroApiMethod("create")]
    public async Task<object> Create(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2d == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2d = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2d;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2e == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "handle", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2e.Target((CallSite) SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2e, args);
      int accountHandle = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2f == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2f = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (SummonerService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site2f;
      // ISSUE: reference to a compiler-generated field
      if (SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site30 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site30 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (SummonerService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site30.Target((CallSite) SummonerService.\u003CCreate\u003Eo__SiteContainer2c.\u003C\u003Ep__Site30, args);
      string summonerName = func2((CallSite) callSite2, obj2);
      RiotAccount account = JsApiService.AccountBag.Get(accountHandle);
      return await account.InvokeAsync<object>("summonerService", "createDefaultSummoner", (object) summonerName);
    }
  }
}
