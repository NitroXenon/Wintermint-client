// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.HeroSelectService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using RiotGames.Platform.Game;
using RiotGames.Platform.Gameclient.Domain.Game.Trade;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("heroSelect", Preload = true)]
  public class HeroSelectService : JsApiService
  {
    private int oldChampionId;
    private string oldChampionSelectionString;

    public HeroSelectService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) => account.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived));
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) => account.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived));
    }

    [MicroApiMethod("quit")]
    public async Task QuitGame()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "quitGame");
      JsApiService.RiotAccount.Game = (GameDTO) null;
    }

    [MicroApiMethod("banChampion")]
    public async Task BanChampion(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "banChampion";
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site8;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "championId", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site9.Target((CallSite) HeroSelectService.\u003CBanChampion\u003Eo__SiteContainer7.\u003C\u003Ep__Site9, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func((CallSite) callSite, obj1);
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, (object) local);
    }

    [MicroApiMethod("getBannableChampions")]
    public async Task<object> GetChampionsForBan()
    {
      ChampionBanInfoDTO[] result = await JsApiService.RiotAccount.InvokeAsync<ChampionBanInfoDTO[]>("gameService", "getChampionsForBan");
      return (object) Enumerable.ToArray(Enumerable.Select((IEnumerable<ChampionBanInfoDTO>) result, x => new
      {
        Id = x.ChampionId
      }));
    }

    [MicroApiMethod("cancelSelectChampion")]
    public async Task CancelSelectChampion()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "cancelSelectChampion");
    }

    [MicroApiMethod("selectSkin")]
    public async Task SelectChampionSkin(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "selectChampionSkin";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index1 = 0;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site17 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site17 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site17.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site17;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "championId", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site18.Target((CallSite) HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site18, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local1 = (ValueType) func1((CallSite) callSite1, obj1);
      objArray2[index1] = (object) local1;
      object[] objArray3 = objArray1;
      int index2 = 1;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site19 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func2 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site19.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite2 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site19;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site1a == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site1a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "skinId", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site1a.Target((CallSite) HeroSelectService.\u003CSelectChampionSkin\u003Eo__SiteContainer16.\u003C\u003Ep__Site1a, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local2 = (ValueType) func2((CallSite) callSite2, obj2);
      objArray3[index2] = (object) local2;
      object[] arguments = objArray1;
      object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    [MicroApiMethod("spells.select")]
    public async Task SelectSummoners(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "selectSpells";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index1 = 0;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "spell1", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20.Target((CallSite) HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local1 = (ValueType) func1((CallSite) callSite1, obj1);
      objArray2[index1] = (object) local1;
      object[] objArray3 = objArray1;
      int index2 = 1;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func2 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite2 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "spell2", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22.Target((CallSite) HeroSelectService.\u003CSelectSummoners\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local2 = (ValueType) func2((CallSite) callSite2, obj2);
      objArray3[index2] = (object) local2;
      object[] arguments = objArray1;
      object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    [MicroApiMethod("spells.available")]
    public int[] GetAvailableSpells(object args)
    {
      return GameData.GetAvailableSummonerSpells(JsApiService.RiotAccount.Game.GameMode);
    }

    [MicroApiMethod("selectChampion")]
    public async Task SelectChampion(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "selectChampion";
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site27 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site27 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site27.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site27;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site28 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "championId", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site28.Target((CallSite) HeroSelectService.\u003CSelectChampion\u003Eo__SiteContainer26.\u003C\u003Ep__Site28, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func((CallSite) callSite, obj1);
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, (object) local);
    }

    [MicroApiMethod("lockIn")]
    public async Task LockIn()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "championSelectCompleted");
    }

    [MicroApiMethod("reroll")]
    public async Task Reroll()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("lcdsRerollService", "roll");
    }

    [MicroApiMethod("setRunePage")]
    public Task SetRunePage(JObject setup)
    {
      return InventoryHelper.SetActiveRuneSetup(JsApiService.RiotAccount.RealmId, JsApiService.RiotAccount.SummonerName, setup);
    }

    [MicroApiMethod("setMasteryPage")]
    public Task SetMasteryPage(JObject setup)
    {
      return InventoryHelper.SetActiveMasterySetup(setup);
    }

    [MicroApiMethod("trade.request")]
    public async Task RequestChampionTrade(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HeroSelectService.\u003C\u003Ec__DisplayClass36 cDisplayClass36 = new HeroSelectService.\u003C\u003Ec__DisplayClass36();
      // ISSUE: variable of a compiler-generated type
      HeroSelectService.\u003C\u003Ec__DisplayClass36 cDisplayClass36_1 = cDisplayClass36;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site33 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site33 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site33.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site33;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site34 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site34 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "internalName", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site34.Target((CallSite) HeroSelectService.\u003CRequestChampionTrade\u003Eo__SiteContainer32.\u003C\u003Ep__Site34, args);
      string str = func((CallSite) callSite, obj);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass36_1.internalName = str;
      // ISSUE: reference to a compiler-generated method
      PlayerParticipant summoner = Enumerable.First<PlayerParticipant>(JsApiService.RiotAccount.Game.AllPlayers, new Func<PlayerParticipant, bool>(cDisplayClass36.\u003CRequestChampionTrade\u003Eb__35));
      JsApiService.Push("game:current:trade:request", (object) true);
      JsApiService.Push("game:current:trade:data", (object) new
      {
        Request = new
        {
          IsSelf = true
        },
        OtherSummonerName = summoner.SummonerName,
        OtherSummonerInternalName = summoner.SummonerInternalName
      });
      try
      {
        // ISSUE: reference to a compiler-generated field
        await this.CallAttemptTradeAsync(cDisplayClass36.internalName, false);
      }
      catch
      {
        this.DismissTrade();
      }
    }

    [MicroApiMethod("trade.accept")]
    public Task AcceptChampionTrade(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3d == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3d = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (HeroSelectService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3d;
      // ISSUE: reference to a compiler-generated field
      if (HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3e == null)
      {
        // ISSUE: reference to a compiler-generated field
        HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "internalName", typeof (HeroSelectService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3e.Target((CallSite) HeroSelectService.\u003CAcceptChampionTrade\u003Eo__SiteContainer3c.\u003C\u003Ep__Site3e, args);
      string summonerInternalName = func((CallSite) callSite, obj);
      this.DismissTrade();
      return this.CallAttemptTradeAsync(summonerInternalName, true);
    }

    [MicroApiMethod("trade.decline")]
    public Task DeclineChampionTrade()
    {
      this.DismissTrade();
      return (Task) JsApiService.RiotAccount.InvokeAsync<object>("lcdsChampionTradeService", "dismissTrade");
    }

    [MicroApiMethod("trade.cancel")]
    public Task CancelChampionTrade()
    {
      this.DismissTrade();
      return this.DeclineChampionTrade();
    }

    private Task CallAttemptTradeAsync(string summonerInternalName, bool isResposne)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      PlayerChampionSelectionDTO championSelectionDto = Enumerable.First<PlayerChampionSelectionDTO>((IEnumerable<PlayerChampionSelectionDTO>) riotAccount.Game.PlayerChampionSelections, (Func<PlayerChampionSelectionDTO, bool>) (x => x.SummonerInternalName == summonerInternalName));
      return (Task) riotAccount.InvokeAsync<object>("lcdsChampionTradeService", "attemptTrade", new object[3]
      {
        (object) summonerInternalName,
        (object) championSelectionDto.ChampionId,
        (object) (bool) (isResposne ? 1 : 0)
      });
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      try
      {
        RiotAccount account = sender as RiotAccount;
        if (account == null || account != JsApiService.RiotAccount || account.Game == null)
          return;
        GameDTO game = e.Body as GameDTO;
        if (game != null)
        {
          PlayerChampionSelectionDTO championSelectionDto = Enumerable.FirstOrDefault<PlayerChampionSelectionDTO>((IEnumerable<PlayerChampionSelectionDTO>) game.PlayerChampionSelections, (Func<PlayerChampionSelectionDTO, bool>) (x => x.SummonerInternalName == account.SummonerInternalName));
          int num = championSelectionDto != null ? championSelectionDto.ChampionId : 0;
          if (num != this.oldChampionId)
            this.DismissTrade();
          this.oldChampionId = num;
          GameTypeConfigDTO gameTypeConfigDto = Enumerable.First<GameTypeConfigDTO>((IEnumerable<GameTypeConfigDTO>) account.GameTypeConfigs, (Func<GameTypeConfigDTO, bool>) (x => x.Id == (double) game.GameTypeConfigId));
          if (game.GameState != "POST_CHAMP_SELECT" || !gameTypeConfigDto.AllowTrades)
            JsApiService.Push("game:current:trade:targets", (object) new object[0]);
          if (!gameTypeConfigDto.AllowTrades)
            return;
          string selectionsString = HeroSelectService.GetChampionSelectionsString(game);
          if (selectionsString != this.oldChampionSelectionString)
            this.UpdateTradersAsync(account);
          this.oldChampionSelectionString = selectionsString;
        }
        else
        {
          TradeContractDTO trade = e.Body as TradeContractDTO;
          if (trade == null)
            return;
          PlayerParticipant[] playerParticipantArray = Enumerable.ToArray<PlayerParticipant>(account.Game.AllPlayers);
          string str1 = Enumerable.First<PlayerParticipant>((IEnumerable<PlayerParticipant>) playerParticipantArray, (Func<PlayerParticipant, bool>) (x => x.SummonerInternalName == trade.RequesterInternalSummonerName)).SummonerName;
          string str2 = Enumerable.First<PlayerParticipant>((IEnumerable<PlayerParticipant>) playerParticipantArray, (Func<PlayerParticipant, bool>) (x => x.SummonerInternalName == trade.RequesterInternalSummonerName)).SummonerName;
          switch (trade.State)
          {
            case "PENDING":
              JsApiService.Push("game:current:trade:request", (object) true);
              JsApiService.Push("game:current:trade:data", (object) new
              {
                Request = new
                {
                  IsSelf = (trade.RequesterInternalSummonerName == account.SummonerInternalName),
                  SummonerName = str1,
                  ChampionId = trade.RequesterChampionId
                },
                Response = new
                {
                  IsSelf = (trade.ResponderInternalSummonerName == account.SummonerInternalName),
                  SummonerName = str2,
                  ChampionId = trade.ResponderChampionId
                },
                OtherSummonerName = (trade.RequesterInternalSummonerName == account.SummonerInternalName ? str2 : str1),
                OtherSummonerInternalName = trade.RequesterInternalSummonerName
              });
              break;
            case "BUSY":
              this.DismissTrade();
              JsApiService.Push("game:current:trade:status", (object) "busy");
              break;
            case "DECLINED":
              this.DismissTrade();
              JsApiService.Push("game:current:trade:status", (object) "declined");
              break;
            case "INVALID":
            case "CANCELED":
              this.DismissTrade();
              JsApiService.Push("game:current:trade:request", (object) false);
              break;
          }
        }
      }
      catch
      {
      }
    }

    private async void UpdateTradersAsync(RiotAccount account)
    {
      try
      {
        PotentialTradersDTO traders = await account.InvokeAsync<PotentialTradersDTO>("lcdsChampionTradeService", "getPotentialTraders");
        if (account == JsApiService.RiotAccount)
        {
          IEnumerable<PlayerParticipant> players = account.Game.AllPlayers;
          JsApiService.Push("game:current:trade:targets", (object) Enumerable.Select(Enumerable.Select((IEnumerable<string>) traders.PotentialTraders, traderInternalName => new
          {
            traderInternalName = traderInternalName,
            player = Enumerable.First<PlayerParticipant>(players, (Func<PlayerParticipant, bool>) (player => player.SummonerInternalName == traderInternalName))
          }), param0 => param0.player.SummonerId));
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void DismissTrade()
    {
      JsApiService.Push("game:current:trade:request", (object) false);
    }

    private static string GetChampionSelectionsString(GameDTO game)
    {
      if (game.PlayerChampionSelections == null)
        return "";
      else
        return string.Join<int>("/", Enumerable.Select<PlayerChampionSelectionDTO, int>((IEnumerable<PlayerChampionSelectionDTO>) game.PlayerChampionSelections, (Func<PlayerChampionSelectionDTO, int>) (x => x.ChampionId)));
    }
  }
}
