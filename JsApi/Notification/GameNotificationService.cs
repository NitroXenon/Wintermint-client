// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.GameNotificationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Browser.Rpc;
using MicroApi;
using RiotGames.Platform.Clientfacade.Domain;
using RiotGames.Platform.Game;
using RiotGames.Platform.Game.Message;
using RiotGames.Platform.Statistics;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Standard;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class GameNotificationService : JsApiService
  {
    private string lastGameJson;
    private string lastGameState;
    private int lastPickTurn;
    private DateTime lastTurnEnds;
    private int lastTurnDuration;

    public GameNotificationService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.Blockers["game"] = (Func<string>) (() =>
        {
          if (account.Game == null)
            return (string) null;
          else
            return "in-game";
        });
        account.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult += new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.Connected += new EventHandler(this.OnAccountConnected);
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult -= new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.Connected -= new EventHandler(this.OnAccountConnected);
      });
      JsApiService.AccountBag.ActiveChanged += new EventHandler<RiotAccount>(this.OnActiveAccountChanged);
    }

    private void OnAccountConnected(object sender, EventArgs e)
    {
      RiotAccount riotAccount = sender as RiotAccount;
      if (riotAccount == null)
        return;
      riotAccount.InvokeAsync<object>("gameService", "retrieveInProgressGameInfo");
    }

    private void OnInvocationResult(object sender, InvocationResultEventArgs e)
    {
      RiotAccount account = sender as RiotAccount;
      if ((!e.Success || !(e.Service == "gameService") ? 0 : (e.Method == "quitGame" ? 1 : 0)) != 0)
      {
        if (account.Game != null && !JsApiService.IsGameStateExitable(account.Game.GameState))
          return;
        this.UpdateGame(account, (GameDTO) null);
      }
      else if ((!e.Success || !(e.Service == "gameService") ? 0 : (e.Method == "retrieveInProgressGameInfo" ? 1 : 0)) != 0)
      {
        PlatformGameLifecycleDTO gameLifecycleDto = e.Result as PlatformGameLifecycleDTO;
        this.UpdateGame(account, gameLifecycleDto != null ? gameLifecycleDto.Game : (GameDTO) null);
      }
      else
        this.OnData(account, e.Result);
    }

    private void OnFlexMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Body);
    }

    private void UpdateGame(RiotAccount account, GameDTO game)
    {
      GameDTO game1 = JsApiService.RiotAccount.Game;
      GameDTO game2 = GameNotificationService.IsGameTerminated(game) ? (GameDTO) null : game;
      account.Game = game2;
      if (JsApiService.AccountBag.Active != account)
      {
        if (game2 == null)
          return;
        account.InvokeAsync<object>("gameService", "quitGame");
      }
      else
      {
        if (!GameNotificationService.IsChampSelect(game1) && GameNotificationService.IsChampSelect(game2))
          account.InvokeAsync<object>("gameService", "setClientReceivedGameMessage", new object[2]
          {
            (object) game.Id,
            (object) "CHAMP_SELECT_CLIENT"
          });
        if (!GameNotificationService.IsGameInProgressStrict(game1) && GameNotificationService.IsGameInProgressStrict(game2))
          this.GetFullGameAsync(account);
        this.NotifyGameChanged(account, game);
      }
    }

    private async Task GetFullGameAsync(RiotAccount account)
    {
      if (account.Game != null)
      {
        PlatformGameLifecycleDTO lifecycle = await SpectateService.GetSpectatorGame(account.RealmId, account.SummonerName);
        GameDTO game = account.Game;
        if (game != null && lifecycle.Game != null)
        {
          game.PlayerChampionSelections = lifecycle.Game.PlayerChampionSelections;
          this.UpdateGame(account, game);
        }
      }
    }

    private void OnData(RiotAccount account, object message)
    {
      GameDTO game1 = message as GameDTO;
      if (game1 != null)
      {
        this.UpdateGame(account, game1);
      }
      else
      {
        EndOfGameStats endOfGameStats = message as EndOfGameStats;
        if (endOfGameStats != null)
        {
          this.UpdateGame(account, (GameDTO) null);
          JsApiService.PushIfActive(account, "game:stats", (object) endOfGameStats);
        }
        else
        {
          PlayerCredentialsDto playerCredentialsDto = message as PlayerCredentialsDto;
          if (playerCredentialsDto != null)
          {
            GameDTO game2 = account.Game;
            if (game2 == null)
              return;
            game2.GameState = "IN_PROGRESS";
            game2.GameStateString = "IN_PROGRESS";
            this.UpdateGame(account, game2);
            JsApiService.PushIfActive(account, "game:launch", (object) playerCredentialsDto);
          }
          else
          {
            GameNotification gameNotification = message as GameNotification;
            if (gameNotification != null)
            {
              this.UpdateGame(account, (GameDTO) null);
              if (!(gameNotification.Type == "PLAYER_BANNED_FROM_GAME"))
                return;
              JsApiService.PushIfActive(account, "game:banned", (object) null);
            }
            else
            {
              LoginDataPacket loginDataPacket = message as LoginDataPacket;
              if (loginDataPacket == null || loginDataPacket.ReconnectInfo == null || loginDataPacket.ReconnectInfo.Game == null)
                return;
              this.UpdateGame(account, loginDataPacket.ReconnectInfo.Game);
            }
          }
        }
      }
    }

    private void OnActiveAccountChanged(object sender, RiotAccount account)
    {
      if (account == null)
        return;
      this.NotifyGameChanged(account, account.Game);
    }

    private void NotifyGameChanged(RiotAccount account, GameDTO game)
    {
      RiotJsTransformer.JavascriptyGame jsGame = RiotJsTransformer.TransformGame(game, account);
      this.CompleteJsGame(account, game, jsGame);
      string json = PushNotification.Serialize((object) jsGame);
      if (json == this.lastGameJson)
        return;
      this.lastGameJson = json;
      JsApiService.PushJson("game:current", json);
    }

    private void CompleteJsGame(RiotAccount account, GameDTO game, RiotJsTransformer.JavascriptyGame jsGame)
    {
      if (game == null)
        return;
      GameTypeConfigDTO gameTypeConfigDto = Enumerable.FirstOrDefault<GameTypeConfigDTO>((IEnumerable<GameTypeConfigDTO>) account.GameTypeConfigs, (Func<GameTypeConfigDTO, bool>) (x => x.Id == (double) game.GameTypeConfigId));
      if (gameTypeConfigDto == null)
        return;
      if (this.lastGameState != game.GameState || this.lastPickTurn != game.PickTurn)
      {
        this.lastGameState = game.GameState;
        this.lastPickTurn = game.PickTurn;
        switch (jsGame.HeroSelectState)
        {
          case "pre":
            this.lastTurnDuration = (int) gameTypeConfigDto.BanTimerDuration;
            break;
          case "pick":
            this.lastTurnDuration = (int) gameTypeConfigDto.MainPickTimerDuration;
            break;
          case "post":
            this.lastTurnDuration = (int) gameTypeConfigDto.PostPickTimerDuration;
            break;
          default:
            this.lastTurnDuration = 0;
            break;
        }
        this.lastTurnEnds = DateTime.UtcNow + TimeSpan.FromSeconds((double) this.lastTurnDuration);
      }
      jsGame.TurnDuration = this.lastTurnDuration;
      jsGame.TurnEnds = this.lastTurnEnds;
    }

    private static bool IsChampSelect(GameDTO game)
    {
      if (game == null)
        return false;
      switch (game.GameState)
      {
        case "PRE_CHAMP_SELECT":
        case "CHAMP_SELECT":
        case "POST_CHAMP_SELECT":
          return true;
        default:
          return false;
      }
    }

    private static bool IsGameInProgressStrict(GameDTO game)
    {
      if (game == null)
        return false;
      else
        return game.GameState == "IN_PROGRESS";
    }

    private static bool IsGameTerminated(GameDTO game)
    {
      if (game == null)
        return true;
      switch (game.GameState)
      {
        case "FAILED_TO_START":
        case "TERMINATED":
        case "TERMINATED_IN_ERROR":
          return true;
        default:
          return false;
      }
    }
  }
}
