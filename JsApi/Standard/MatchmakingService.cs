// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.MatchmakingService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Matchmaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("matchmaking")]
  public class MatchmakingService : JsApiService
  {
    [MicroApiMethod("isEnabled")]
    public Task<bool> IsEnabled()
    {
      return JsApiService.AccountBag.Get(JsApiService.RiotAccount.RealmId, RiotAccountPreference.InactivePreferred).InvokeAsync<bool>("matchmakerService", "isMatchmakingEnabled");
    }

    [MicroApiMethod("getQueueInfo")]
    public Task<QueueInfo> GetQueueInfo(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (MatchmakingService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "queueId", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) MatchmakingService.\u003CGetQueueInfo\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      return JsApiService.AccountBag.Get(JsApiService.RiotAccount.RealmId, RiotAccountPreference.InactivePreferred).InvokeAsync<QueueInfo>("matchmakerService", "getQueueInfo", (object) func((CallSite) callSite, obj));
    }

    [MicroApiMethod("queue")]
    public async Task AttachToQueue(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func1 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite1 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site5;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func2 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite2 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site6;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "queueIds", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site7.Target((CallSite) MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site7, args);
      // ISSUE: variable of the null type
      __Null local1 = null;
      object obj2 = func2((CallSite) callSite2, obj1, (object) local1);
      int[] numArray1;
      if (!func1((CallSite) callSite1, obj2))
      {
        numArray1 = new int[0];
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, int[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int[]), typeof (MatchmakingService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int[]> func3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site8.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int[]>> callSite3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site8;
        // ISSUE: reference to a compiler-generated field
        if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "queueIds", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site9.Target((CallSite) MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site9, args);
        numArray1 = func3((CallSite) callSite3, obj3);
      }
      int[] queueIds = numArray1;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func4 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitea.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite4 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitea;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Siteb == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Siteb = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func5 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Siteb.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite5 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Siteb;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitec == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitec = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerIds", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitec.Target((CallSite) MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitec, args);
      // ISSUE: variable of the null type
      __Null local2 = null;
      object obj5 = func5((CallSite) callSite5, obj4, (object) local2);
      long[] numArray2;
      if (!func4((CallSite) callSite4, obj5))
      {
        numArray2 = new long[0];
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sited == null)
        {
          // ISSUE: reference to a compiler-generated field
          MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, long[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long[]), typeof (MatchmakingService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, long[]> func3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sited.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, long[]>> callSite3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sited;
        // ISSUE: reference to a compiler-generated field
        if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitee == null)
        {
          // ISSUE: reference to a compiler-generated field
          MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerIds", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitee.Target((CallSite) MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitee, args);
        numArray2 = func3((CallSite) callSite3, obj3);
      }
      long[] teamSummonerIds = numArray2;
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "matchmakerService";
      string method = "attachTeamToQueues";
      MatchMakerParams matchMakerParams = new MatchMakerParams();
      matchMakerParams.BotDifficulty = "MEDIUM";
      MatchMakerParams matchMakerParams1 = matchMakerParams;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MatchmakingService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func6 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitef.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite6 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Sitef;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "inviteId", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site10.Target((CallSite) MatchmakingService.\u003CAttachToQueue\u003Eo__SiteContainer4.\u003C\u003Ep__Site10, args);
      string str = func6((CallSite) callSite6, obj6);
      matchMakerParams1.InvitationId = str;
      matchMakerParams.QueueIds = Enumerable.ToList<int>((IEnumerable<int>) queueIds);
      matchMakerParams.Team = Enumerable.ToList<long>((IEnumerable<long>) teamSummonerIds);
      MatchMakerParams matchMakerParams2 = matchMakerParams;
      SearchingForMatchNotification matchNotification = await riotAccount.InvokeAsync<SearchingForMatchNotification>(destination, method, (object) matchMakerParams2);
    }

    [MicroApiMethod("unqueue")]
    public async Task Unqueue()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("matchmakerService", "purgeFromQueues");
    }

    [MicroApiMethod("notifyAcceptedInviteId")]
    public async Task AcceptInviteForMatchmakingGame(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "matchmakerService";
      string method = "acceptInviteForMatchmakingGame";
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1a == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1a = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MatchmakingService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1a.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1a;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1b == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1b = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "inviteId", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1b.Target((CallSite) MatchmakingService.\u003CAcceptInviteForMatchmakingGame\u003Eo__SiteContainer19.\u003C\u003Ep__Site1b, args);
      string str = func((CallSite) callSite, obj1);
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, (object) str);
    }

    [MicroApiMethod("getQueues")]
    public async Task<object> GetAvailableQueues()
    {
      RiotAccount account = JsApiService.AccountBag.Get(JsApiService.RiotAccount.RealmId, RiotAccountPreference.InactivePreferred);
      return (object) await account.InvokeCachedAsync<GameQueueConfig[]>("matchmakerService", "getAvailableQueues");
    }

    [MicroApiMethod("acceptGame")]
    public async Task HandleFoundGame(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "acceptPoppedGame";
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site24 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (MatchmakingService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func = MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site24.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite = MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site24;
      // ISSUE: reference to a compiler-generated field
      if (MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site25 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accept", typeof (MatchmakingService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site25.Target((CallSite) MatchmakingService.\u003CHandleFoundGame\u003Eo__SiteContainer23.\u003C\u003Ep__Site25, args);
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) (bool) (func((CallSite) callSite, obj1) ? 1 : 0);
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, (object) local);
    }
  }
}
