// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.QueueNotificationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using RiotGames.Platform.Game;
using RiotGames.Platform.Game.Message;
using RiotGames.Platform.Matchmaking;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class QueueNotificationService : JsApiService
  {
    private const int kNoQueueId = -1;
    private const int kStatusAfk = 0;
    private const int kStatusAccept = 1;
    private const int kStatusDecline = 2;

    public QueueNotificationService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.Storage["queueId"] = (object) -1;
        account.Blockers["queue"] = (Func<string>) (() =>
        {
          if ((int) account.Storage["queueId"] == -1)
            return (string) null;
          else
            return "in-queue";
        });
        account.InvocationResult += new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.InvocationResult -= new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
      });
    }

    private void OnFlexMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Body);
    }

    private void OnInvocationResult(object sender, InvocationResultEventArgs e)
    {
      RiotAccount account = sender as RiotAccount;
      this.OnData(account, e.Result);
      if (e.Service == "matchmakerService" && e.Method == "purgeFromQueues" && e.Success)
      {
        this.SetLeftQueue(account);
      }
      else
      {
        if (!(e.Service == "gameService") || !(e.Method == "quitGame") || (account.Game == null || !JsApiService.IsGameStateExitable(account.Game.GameState)))
          return;
        this.SetLeftQueue(account);
      }
    }

    private void SetLeftQueue(RiotAccount account)
    {
      account.Storage["queueId"] = (object) -1;
      JsApiService.PushIfActive(account, "game:queue", (object) -1);
      JsApiService.PushIfActive(account, "game:queue:done", (object) null);
    }

    private void SetEnteredQueue(RiotAccount account, int queueId)
    {
      account.Storage["queueId"] = (object) queueId;
      JsApiService.PushIfActive(account, "game:queue", (object) queueId);
    }

    private async void OnData(RiotAccount account, object obj)
    {
      try
      {
        await this.ProcessData(account, obj);
      }
      catch
      {
      }
    }

    private async Task ProcessData(RiotAccount account, object obj)
    {
      SearchingForMatchNotification queue = obj as SearchingForMatchNotification;
      if (queue != null)
      {
        if (account != JsApiService.RiotAccount)
        {
          account.InvokeAsync<object>("matchmakerService", "purgeFromQueues");
        }
        else
        {
          if ((queue.JoinedQueues == null ? 0 : (queue.JoinedQueues.Count > 0 ? 1 : 0)) != 0)
            this.SetEnteredQueue(account, Enumerable.First<QueueInfo>((IEnumerable<QueueInfo>) queue.JoinedQueues).QueueId);
          var fAnonymousType13 = new
          {
            JoinedQueues = Enumerable.Select<QueueInfo, int>((IEnumerable<QueueInfo>) queue.JoinedQueues, (Func<QueueInfo, int>) (x => x.QueueId)),
            JoinFailures = queue.PlayerJoinFailures != null ? Enumerable.ToArray<object>(Enumerable.Select<FailedJoinPlayer, object>((IEnumerable<FailedJoinPlayer>) queue.PlayerJoinFailures, new Func<FailedJoinPlayer, object>(RiotJsTransformer.TransformFailedJoinPlayer))) : new object[0]
          };
          JsApiService.PushIfActive(account, "game:queue:joinStatus", (object) fAnonymousType13);
        }
      }
      else
      {
        GameNotification gameNotification = obj as GameNotification;
        if (gameNotification != null)
        {
          switch (gameNotification.Type)
          {
            case "PLAYER_QUIT":
            case "TEAM_REMOVED":
            case "PLAYER_REMOVED":
              string summonerName = (string) null;
              long summonerId;
              if (long.TryParse(gameNotification.MessageArgument, out summonerId))
                summonerName = await JsApiService.GetSummonerNameBySummonerId(account.RealmId, summonerId);
              this.SetLeftQueue(account);
              if (!string.IsNullOrEmpty(summonerName))
              {
                JsApiService.PushIfActive(account, "game:queue:leave", (object) summonerName);
                break;
              }
              else
                break;
          }
        }
        else
        {
          GameDTO game = obj as GameDTO;
          if (game != null && game.StatusOfParticipants != null)
          {
            int[] statuses = Enumerable.ToArray<int>(Enumerable.Select<char, int>((IEnumerable<char>) game.StatusOfParticipants.ToCharArray(), (Func<char, int>) (x => int.Parse(x.ToString((IFormatProvider) CultureInfo.InvariantCulture)))));
            if (game.GameState == "START_REQUESTED" || game.GameState == "IN_PROGRESS")
              JsApiService.PushIfActive(account, "game:queue:done", (object) null);
            if (game.GameState != "JOINING_CHAMP_SELECT")
              JsApiService.PushIfActive(account, "game:queue:dropped", (object) null);
            switch (game.GameState)
            {
              case "JOINING_CHAMP_SELECT":
                JsApiService.PushIfActive(account, "game:queue:found", (object) QueueNotificationService.GetAcceptDeclineStatus(game.StatusOfParticipants));
                break;
              case "FAILED_TO_START":
              case "START_REQUESTED":
              case "IN_PROGRESS":
                this.SetLeftQueue(account);
                break;
              case "TERMINATED":
                int idx = 0;
                \u003C\u003Ef__AnonymousType14<PlayerParticipant, int>[] source1 = Enumerable.ToArray(Enumerable.Where(Enumerable.Select(Enumerable.Concat<IParticipant>((IEnumerable<IParticipant>) game.TeamOne, (IEnumerable<IParticipant>) game.TeamTwo), x => new
                {
                  Participant = x as PlayerParticipant,
                  Status = statuses[idx++]
                }), x => x.Participant != null));
                var fAnonymousType14 = Enumerable.FirstOrDefault(source1, x => x.Participant.SummonerId == (double) account.SummonerId);
                if (fAnonymousType14 != null)
                {
                  List<\u003C\u003Ef__AnonymousType14<PlayerParticipant, int>> source2 = Enumerable.FirstOrDefault<List<\u003C\u003Ef__AnonymousType14<PlayerParticipant, int>>>(Enumerable.Select<IGrouping<double?, \u003C\u003Ef__AnonymousType14<PlayerParticipant, int>>, List<\u003C\u003Ef__AnonymousType14<PlayerParticipant, int>>>(Enumerable.GroupBy(Enumerable.Where(source1, x => x.Participant.TeamParticipantId.HasValue), x => x.Participant.TeamParticipantId), x => Enumerable.ToList(x)), g => Enumerable.Any(g, x => x.Participant.SummonerId == (double) account.SummonerId));
                  if (source2 != null && Enumerable.Any(source2, x => x.Status != 1))
                  {
                    this.SetLeftQueue(account);
                    JsApiService.PushIfActive(account, "game:queue:acceptFail", (object) Enumerable.Select(Enumerable.Where(source2, x => x.Status != 1), x => x.Participant.SummonerName));
                    break;
                  }
                  else if (fAnonymousType14.Status != 1)
                  {
                    this.SetLeftQueue(account);
                    JsApiService.PushIfActive(account, "game:queue:acceptFail", (object) new string[1]
                    {
                      account.SummonerName
                    });
                    break;
                  }
                  else
                    break;
                }
                else
                  break;
            }
          }
        }
      }
    }

    private static IEnumerable<string> GetAcceptDeclineStatus(string statusOfParticipants)
    {
      return Enumerable.Select<char, string>((IEnumerable<char>) statusOfParticipants.ToCharArray(), new Func<char, string>(QueueNotificationService.GetSingleStatus));
    }

    private static string GetSingleStatus(char status)
    {
      switch (status)
      {
        case '0':
          return "none";
        case '1':
          return "accepted";
        case '2':
          return "declined";
        default:
          return "unknown";
      }
    }
  }
}
