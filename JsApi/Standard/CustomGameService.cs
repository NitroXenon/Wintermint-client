// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.CustomGameService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Game;
using RiotGames.Platform.Game.Map;
using RiotGames.Platform.Game.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("customGame")]
  public class CustomGameService : GameJsApiService
  {
    [MicroApiMethod("list")]
    public async Task<object> GetGamesAsync(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realmId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) CustomGameService.\u003CGetGamesAsync\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      string realmId = func((CallSite) callSite, obj);
      RiotAccount account = JsApiService.AccountBag.Get(realmId);
      IEnumerable<\u003C\u003Ef__AnonymousType36<int, string, int, string, string, int, int, bool, bool, int, string>> games = Enumerable.Select((IEnumerable<PracticeGameSearchResult>) await account.InvokeAsync<PracticeGameSearchResult[]>("gameService", "listAllPracticeGames"), game => new
      {
        Id = game.Id,
        Name = game.Name,
        MapId = game.GameMapId,
        MapName = GameJsApiService.GetGameMapFriendlyName(game.GameMapId),
        Mode = game.GameMode,
        Players = game.Team1Count + game.Team2Count,
        MaxPlayers = game.MaxNumPlayers,
        HasPassword = game.PrivateGame,
        IsDefault = PracticeGameData.IsDefaultGame(game.Name) && !game.PrivateGame,
        Spectators = game.SpectatorCount,
        Host = game.Owner.SummonerName
      });
      return (object) Enumerable.ToArray(games);
    }

    [MicroApiMethod("create")]
    public async Task CreateGame(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "spectators", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef, args);
      string spectatorString = GameJsApiService.GetAllowSpectators(func1((CallSite) callSite1, obj1));
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func2 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite2 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site10;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mapId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site11.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site11, args);
      int mapId = func2((CallSite) callSite2, obj2);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site12.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site12;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "name", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site13.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site13, args);
      string name = func3((CallSite) callSite3, obj3);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site14 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func4 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site14.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite4 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site14;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "password", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site15.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site15, args);
      string password = func4((CallSite) callSite4, obj4);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site16 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func5 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site16.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite5 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site16;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site17 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site17 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "gctId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site17.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site17, args);
      int gameTypeConfigId = func5((CallSite) callSite5, obj5);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site18 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func6 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site18.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite6 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site18;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "players", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site19.Target((CallSite) CustomGameService.\u003CCreateGame\u003Eo__SiteContainerd.\u003C\u003Ep__Site19, args);
      int players = func6((CallSite) callSite6, obj6);
      object obj7 = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "createPracticeGame", (object) new PracticeGameConfig()
      {
        AllowSpectators = spectatorString,
        GameMap = new GameMap()
        {
          MapId = mapId,
          TotalPlayers = players
        },
        GameMode = GameJsApiService.GetGameMode(mapId),
        GameName = name,
        GamePassword = password,
        GameTypeConfig = gameTypeConfigId,
        MaxNumPlayers = players
      });
    }

    [MicroApiMethod("ban")]
    public async Task BanPlayer(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CustomGameService.\u003C\u003Ec__DisplayClass2c cDisplayClass2c = new CustomGameService.\u003C\u003Ec__DisplayClass2c();
      // ISSUE: variable of a compiler-generated type
      CustomGameService.\u003C\u003Ec__DisplayClass2c cDisplayClass2c1 = cDisplayClass2c;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site24 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> func1 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site24.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> callSite1 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site24;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site25 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accountId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site25.Target((CallSite) CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site25, args);
      long num = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass2c1.accountId = num;
      GameDTO game = JsApiService.RiotAccount.Game;
      // ISSUE: reference to a compiler-generated method
      if (Enumerable.Any<GameObserver>((IEnumerable<GameObserver>) game.Observers, new Func<GameObserver, bool>(cDisplayClass2c.\u003CBanPlayer\u003Eb__2a)))
      {
        RiotAccount riotAccount = JsApiService.RiotAccount;
        string destination = "gameService";
        string method = "banObserverFromGame";
        object[] objArray1 = new object[2]
        {
          (object) game.Id,
          null
        };
        object[] objArray2 = objArray1;
        int index = 1;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site26 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site26 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (CustomGameService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, long> func2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site26.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, long>> callSite2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site26;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site27 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site27 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accountId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site27.Target((CallSite) CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site27, args);
        // ISSUE: variable of a boxed type
        __Boxed<long> local = (ValueType) func2((CallSite) callSite2, obj2);
        objArray2[index] = (object) local;
        object[] arguments = objArray1;
        object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Enumerable.Any<PlayerParticipant>(game.AllPlayers, new Func<PlayerParticipant, bool>(cDisplayClass2c.\u003CBanPlayer\u003Eb__2b)))
        {
          RiotAccount riotAccount = JsApiService.RiotAccount;
          string destination = "gameService";
          string method = "banUserFromGame";
          object[] objArray1 = new object[2]
          {
            (object) game.Id,
            null
          };
          object[] objArray2 = objArray1;
          int index = 1;
          // ISSUE: reference to a compiler-generated field
          if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site28 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site28 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (long), typeof (CustomGameService)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, long> func2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site28.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, long>> callSite2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site28;
          // ISSUE: reference to a compiler-generated field
          if (CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site29 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site29 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accountId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site29.Target((CallSite) CustomGameService.\u003CBanPlayer\u003Eo__SiteContainer23.\u003C\u003Ep__Site29, args);
          // ISSUE: variable of a boxed type
          __Boxed<long> local = (ValueType) func2((CallSite) callSite2, obj2);
          objArray2[index] = (object) local;
          object[] arguments = objArray1;
          object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
        }
      }
    }

    [MicroApiMethod("quit")]
    public async Task QuitGame()
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "quitGame");
      JsApiService.RiotAccount.Game = (GameDTO) null;
    }

    [MicroApiMethod("makeObserver")]
    public async Task SwitchToObserver(object args)
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "switchPlayerToObserver", (object) JsApiService.RiotAccount.Game.Id);
    }

    public async Task AddBot(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "selectBotChampion";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index = 0;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site39 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site39 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site39.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site39;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site3a == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site3a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "championId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site3a.Target((CallSite) CustomGameService.\u003CAddBot\u003Eo__SiteContainer38.\u003C\u003Ep__Site3a, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func((CallSite) callSite, obj1);
      objArray2[index] = (object) local;
      objArray1[1] = (object) new BotParticipant();
      object[] arguments = objArray1;
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    public async Task RemoveBot(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "removeBotChampion";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index = 0;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site3f == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site3f = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site3f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site3f;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site40 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site40 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "championId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site40.Target((CallSite) CustomGameService.\u003CRemoveBot\u003Eo__SiteContainer3e.\u003C\u003Ep__Site40, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func((CallSite) callSite, obj1);
      objArray2[index] = (object) local;
      objArray1[1] = (object) new BotParticipant();
      object[] arguments = objArray1;
      object obj2 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    [MicroApiMethod("observe")]
    public async Task ObserveGame(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "observeGame";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index1 = 0;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site45 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site45 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site45.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site45;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site46 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site46 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "gameId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site46.Target((CallSite) CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site46, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func1((CallSite) callSite1, obj1);
      objArray2[index1] = (object) local;
      object[] objArray3 = objArray1;
      int index2 = 1;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site47 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site47 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site47.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site47;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site48 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site48 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "password", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site48.Target((CallSite) CustomGameService.\u003CObserveGame\u003Eo__SiteContainer44.\u003C\u003Ep__Site48, args);
      string str = func2((CallSite) callSite2, obj2);
      objArray3[index2] = (object) str;
      object[] arguments = objArray1;
      object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    [MicroApiMethod("makePlayer")]
    public async Task SwitchToPlayer(object args)
    {
      double gameId = JsApiService.RiotAccount.Game.Id;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4d == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4d = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func1 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite1 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4d;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4e == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "blue", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4e.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4e, args);
      // ISSUE: variable of the null type
      __Null local1 = null;
      object obj2 = func1((CallSite) callSite1, obj1, (object) local1);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4f == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4f = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj3;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4f.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site4f, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site50 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site50 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool, object> func2 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site50.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool, object>> callSite2 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site50;
        object obj4 = obj2;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site51 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site51 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (CustomGameService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> func3 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site51.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> callSite3 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site51;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site52 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site52 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "blue", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site52.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site52, args);
        int num = func3((CallSite) callSite3, obj5) ? 1 : 0;
        obj3 = func2((CallSite) callSite2, obj4, num != 0);
      }
      else
        obj3 = obj2;
      object blueRequested = obj3;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site53 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site53 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func4 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site53.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite4 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site53;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site54 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site54 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "purple", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site54.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site54, args);
      // ISSUE: variable of the null type
      __Null local2 = null;
      object obj7 = func4((CallSite) callSite4, obj6, (object) local2);
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site55 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site55 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj8;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site55.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site55, obj7))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site56 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site56 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool, object> func2 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site56.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool, object>> callSite2 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site56;
        object obj4 = obj7;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site57 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site57 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (CustomGameService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> func3 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site57.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> callSite3 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site57;
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site58 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site58 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "purple", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site58.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site58, args);
        int num = func3((CallSite) callSite3, obj5) ? 1 : 0;
        obj8 = func2((CallSite) callSite2, obj4, num != 0);
      }
      else
        obj8 = obj7;
      object purpleRequested = obj8;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site59 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site59 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func5 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site59.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite5 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site59;
      object obj9 = blueRequested;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5a == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5a = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj10;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5a.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5a, obj9))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5b == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5b = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj10 = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5b.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5b, obj9, purpleRequested);
      }
      else
        obj10 = obj9;
      if (func5((CallSite) callSite5, obj10))
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5c == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5c = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        int requestedTeamId = CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5c.Target((CallSite) CustomGameService.\u003CSwitchToPlayer\u003Eo__SiteContainer4c.\u003C\u003Ep__Site5c, blueRequested) ? 100 : 200;
        object obj4 = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "switchObserverToPlayer", new object[2]
        {
          (object) gameId,
          (object) requestedTeamId
        });
      }
    }

    [MicroApiMethod("switch")]
    public async Task SwitchTeams(object args)
    {
      object obj = await JsApiService.RiotAccount.InvokeAsync<object>("gameService", "switchTeams", (object) JsApiService.RiotAccount.Game.Id);
    }

    [MicroApiMethod("join")]
    public async Task JoinGame(object args)
    {
      RiotAccount riotAccount = JsApiService.RiotAccount;
      string destination = "gameService";
      string method = "joinGame";
      object[] objArray1 = new object[2];
      object[] objArray2 = objArray1;
      int index1 = 0;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site68 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site68 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site68.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site68;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site69 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site69 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "gameId", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site69.Target((CallSite) CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site69, args);
      // ISSUE: variable of a boxed type
      __Boxed<int> local = (ValueType) func1((CallSite) callSite1, obj1);
      objArray2[index1] = (object) local;
      object[] objArray3 = objArray1;
      int index2 = 1;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6a == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6a = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CustomGameService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6a.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6a;
      // ISSUE: reference to a compiler-generated field
      if (CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6b == null)
      {
        // ISSUE: reference to a compiler-generated field
        CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6b = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "password", typeof (CustomGameService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6b.Target((CallSite) CustomGameService.\u003CJoinGame\u003Eo__SiteContainer67.\u003C\u003Ep__Site6b, args);
      string str = func2((CallSite) callSite2, obj2);
      objArray3[index2] = (object) str;
      object[] arguments = objArray1;
      object obj3 = await riotAccount.InvokeAsync<object>(destination, method, arguments);
    }

    [MicroApiMethod("start")]
    public async Task<object> StartChampionSelect(object args)
    {
      GameDTO game = JsApiService.RiotAccount.Game;
      StartChampSelectDTO result = await JsApiService.RiotAccount.InvokeAsync<StartChampSelectDTO>("gameService", "startChampionSelection", new object[2]
      {
        (object) game.Id,
        (object) game.OptimisticLock
      });
      return (object) new
      {
        Success = (result.InvalidPlayers.Count == 0),
        JoinFailures = Enumerable.ToArray<object>(Enumerable.Select<FailedJoinPlayer, object>((IEnumerable<FailedJoinPlayer>) result.InvalidPlayers, new Func<FailedJoinPlayer, object>(RiotJsTransformer.TransformFailedJoinPlayer)))
      };
    }
  }
}
