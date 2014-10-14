// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.RiotJsTransformer
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using RiotGames.Platform.Game;
using RiotGames.Platform.Matchmaking;
using RiotGames.Platform.Reroll.Pojo;
using RiotGames.Platform.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using WintermintClient.Riot;

namespace WintermintClient.JsApi
{
  internal static class RiotJsTransformer
  {
    private static string[] allTurns = new string[11]
    {
      "<<none>>",
      "CHAMP_SELECT",
      "FAILED_TO_START",
      "IN_PROGRESS",
      "JOINING_CHAMP_SELECT",
      "POST_CHAMP_SELECT",
      "PRE_CHAMP_SELECT",
      "START_REQUESTED",
      "TEAM_SELECT",
      "TERMINATED",
      "TERMINATED_IN_ERROR"
    };
    private const int kChampionToSkinIdCoefficient = 1000;

    public static RiotJsTransformer.JavascriptyGame TransformGame(GameDTO game, RiotAccount account)
    {
      return RiotJsTransformer.TransformGame(game, account, account.AccountId);
    }

    public static RiotJsTransformer.JavascriptyGame TransformGame(GameDTO game, RiotAccount account, long accountId)
    {
      if (game == null)
      {
        return new RiotJsTransformer.JavascriptyGame()
        {
          State = "none"
        };
      }
      else
      {
        GameTypeConfigDTO gameTypeConfigDto = Enumerable.FirstOrDefault<GameTypeConfigDTO>((IEnumerable<GameTypeConfigDTO>) account.GameTypeConfigs, (Func<GameTypeConfigDTO, bool>) (x => x.Id == (double) game.GameTypeConfigId)) ?? new GameTypeConfigDTO();
        List<BannedChampion> list = game.BannedChampions ?? new List<BannedChampion>(0);
        var fAnonymousType34 = new
        {
          TeamOne = Enumerable.Where<BannedChampion>((IEnumerable<BannedChampion>) list, (Func<BannedChampion, bool>) (x => x.TeamId == 100)),
          TeamTwo = Enumerable.Where<BannedChampion>((IEnumerable<BannedChampion>) list, (Func<BannedChampion, bool>) (x => x.TeamId == 200))
        };
        RiotJsTransformer.JavascriptyGame javascriptyGame = new RiotJsTransformer.JavascriptyGame()
        {
          RealmId = account.RealmId,
          MatchId = (long) game.Id,
          Name = game.Name.Trim(),
          State = GameJsApiService.GetGameState(game.GameState),
          HeroSelectState = GameJsApiService.GetGameHeroSelectState(game.GameState),
          TeamOne = RiotJsTransformer.ToTeam(accountId, fAnonymousType34.TeamOne, 1, (IEnumerable<IParticipant>) game.TeamOne),
          TeamTwo = RiotJsTransformer.ToTeam(accountId, fAnonymousType34.TeamTwo, 2, (IEnumerable<IParticipant>) game.TeamTwo),
          IsOwner = game.OwnerSummary != null && (long) game.OwnerSummary.AccountId == accountId,
          ConferenceJid = game.RoomName + ".pvp.net",
          ConferencePassword = game.RoomPassword,
          TurnHash = RiotJsTransformer.GetTurnHash(game),
          GameTypeConfigName = gameTypeConfigDto.Name,
          QueueName = game.QueueTypeName,
          Created = game.CreationTime,
          ExpiryTime = DateTime.UtcNow + TimeSpan.FromMilliseconds(game.ExpiryTime),
          MapId = game.MapId
        };
        Dictionary<string, RiotJsTransformer.JavascriptyPlayer> dictionary = Enumerable.ToDictionary<RiotJsTransformer.JavascriptyPlayer, string, RiotJsTransformer.JavascriptyPlayer>(Enumerable.Where<RiotJsTransformer.JavascriptyPlayer>(Enumerable.Concat<RiotJsTransformer.JavascriptyPlayer>((IEnumerable<RiotJsTransformer.JavascriptyPlayer>) javascriptyGame.TeamOne.Members, (IEnumerable<RiotJsTransformer.JavascriptyPlayer>) javascriptyGame.TeamTwo.Members), (Func<RiotJsTransformer.JavascriptyPlayer, bool>) (x => x.InternalName != null)), (Func<RiotJsTransformer.JavascriptyPlayer, string>) (x => x.InternalName), (Func<RiotJsTransformer.JavascriptyPlayer, RiotJsTransformer.JavascriptyPlayer>) (x => x));
        foreach (PlayerChampionSelectionDTO championSelectionDto in game.PlayerChampionSelections)
        {
          RiotJsTransformer.JavascriptyPlayer javascriptyPlayer;
          if (dictionary.TryGetValue(championSelectionDto.SummonerInternalName, out javascriptyPlayer))
          {
            javascriptyPlayer.SpellIds = new int[2]
            {
              (int) championSelectionDto.Spell1Id,
              (int) championSelectionDto.Spell2Id
            };
            javascriptyPlayer.ChampionId = championSelectionDto.ChampionId;
            javascriptyPlayer.SkinId = javascriptyPlayer.ChampionId * 1000 + championSelectionDto.SelectedSkinIndex;
          }
        }
        foreach (RiotJsTransformer.JavascriptyPlayer javascriptyPlayer in Enumerable.Select<KeyValuePair<string, RiotJsTransformer.JavascriptyPlayer>, RiotJsTransformer.JavascriptyPlayer>((IEnumerable<KeyValuePair<string, RiotJsTransformer.JavascriptyPlayer>>) dictionary, (Func<KeyValuePair<string, RiotJsTransformer.JavascriptyPlayer>, RiotJsTransformer.JavascriptyPlayer>) (x => x.Value)))
        {
          javascriptyPlayer.RealmId = account.RealmId;
          if (javascriptyGame.HeroSelectState == "post" || javascriptyPlayer.PickState == "pending" && javascriptyPlayer.ChampionId > 0)
            javascriptyPlayer.PickState = "completed";
        }
        return javascriptyGame;
      }
    }

    private static RiotJsTransformer.JavascriptyTeam ToTeam(long myAccountId, IEnumerable<BannedChampion> bannedChampions, int teamId, IEnumerable<IParticipant> participants)
    {
      return new RiotJsTransformer.JavascriptyTeam()
      {
        Bans = Enumerable.ToArray<int>(Enumerable.Select<BannedChampion, int>(bannedChampions, (Func<BannedChampion, int>) (x => x.ChampionId))),
        Members = Enumerable.ToArray<RiotJsTransformer.JavascriptyPlayer>(Enumerable.Select<IParticipant, RiotJsTransformer.JavascriptyPlayer>(participants, (Func<IParticipant, RiotJsTransformer.JavascriptyPlayer>) (p => RiotJsTransformer.TransformParticipant(p, teamId, myAccountId))))
      };
    }

    private static RiotJsTransformer.JavascriptyPlayer TransformParticipant(IParticipant participant, int teamId, long dudeAccountId)
    {
      GameParticipant gameParticipant = participant as GameParticipant;
      if (gameParticipant != null)
      {
        double num1 = -1.0;
        double num2 = -1.0;
        PlayerParticipant playerParticipant1 = gameParticipant as PlayerParticipant;
        if (playerParticipant1 != null)
        {
          num1 = playerParticipant1.AccountId;
          num2 = playerParticipant1.SummonerId;
        }
        RiotJsTransformer.JsRerollState jsRerollState = (RiotJsTransformer.JsRerollState) null;
        AramPlayerParticipant playerParticipant2 = gameParticipant as AramPlayerParticipant;
        if (playerParticipant2 != null && playerParticipant2.PointSummary != null)
        {
          PointSummary pointSummary = playerParticipant2.PointSummary;
          jsRerollState = new RiotJsTransformer.JsRerollState()
          {
            Points = (int) pointSummary.CurrentPoints,
            MaximumPoints = pointSummary.MaxRolls * (int) pointSummary.PointsCostToRoll,
            RerollCost = (int) pointSummary.PointsCostToRoll
          };
        }
        return new RiotJsTransformer.JavascriptyPlayer()
        {
          Name = gameParticipant.SummonerName,
          InternalName = gameParticipant.SummonerInternalName,
          SummonerId = num2,
          AccountId = num1,
          TeamId = teamId,
          RerollState = jsRerollState,
          SpellIds = new int[0],
          PickState = RiotJsTransformer.TransformPickState(gameParticipant.PickMode),
          IsDude = num1 == (double) dudeAccountId
        };
      }
      else
        return new RiotJsTransformer.JavascriptyPlayer()
        {
          PickState = "completed"
        };
    }

    private static int GetTurnHash(GameDTO game)
    {
      int num = Math.Max(Array.IndexOf<string>(RiotJsTransformer.allTurns, game.GameState), 0);
      return game.PickTurn << 4 | num & 15;
    }

    private static string TransformPickState(int pickState)
    {
      switch (pickState)
      {
        case 0:
          return "pending";
        case 1:
          return "picking";
        case 2:
          return "completed";
        default:
          return "unknown";
      }
    }

    public static object TransformPersonalGame(PlayerGameStats playerStats)
    {
      return (object) new RiotJsTransformer.TransformedPersonalGame()
      {
        Id = (int) playerStats.GameId,
        Length = -1,
        MapId = (int) playerStats.GameMapId,
        Mode = playerStats.GameMode,
        Queue = playerStats.QueueType,
        Started = playerStats.CreateDate,
        Type = playerStats.QueueType,
        Name = string.Empty,
        AccountId = (long) playerStats.UserId,
        ChampionId = (int) playerStats.ChampionId,
        Spell1 = (int) playerStats.Spell1,
        Spell2 = (int) playerStats.Spell2,
        Left = playerStats.Leaver,
        Statistics = Enumerable.ToDictionary<RawStat, string, int>((IEnumerable<RawStat>) playerStats.Statistics, (Func<RawStat, string>) (x => x.StatType), (Func<RawStat, int>) (x => (int) x.Value)),
        IpEarned = (int) playerStats.IpEarned,
        Map = GameJsApiService.GetGameMapFriendlyName((int) playerStats.GameMapId)
      };
    }

    public static object TransformFailedJoinPlayer(FailedJoinPlayer player)
    {
      QueueDodger queueDodger = player as QueueDodger;
      return (object) new
      {
        Summoner = player.Summoner.Name,
        Reason = player.ReasonFailed.ToLowerInvariant(),
        Length = (queueDodger != null ? queueDodger.DodgePenaltyRemainingTime : -1.0)
      };
    }

    [Serializable]
    internal class JavascriptyGame
    {
      public string RealmId;
      public long MatchId;
      public string Name;
      public string State;
      public string HeroSelectState;
      public RiotJsTransformer.JavascriptyTeam TeamOne;
      public RiotJsTransformer.JavascriptyTeam TeamTwo;
      public bool IsOwner;
      public string ConferenceJid;
      public string ConferencePassword;
      public string GameTypeConfigName;
      public string QueueName;
      public DateTime Created;
      public DateTime ExpiryTime;
      public int TurnHash;
      public int MapId;
      public int TurnDuration;
      public DateTime TurnEnds;

      public string Id
      {
        get
        {
          return string.Format("{0}#{1}", (object) this.RealmId, (object) this.MatchId);
        }
      }
    }

    [Serializable]
    internal class TransformedPersonalGame
    {
      public int Id;
      public int Length;
      public int MapId;
      public string Mode;
      public string Queue;
      public DateTime Started;
      public string Type;
      public string Name;
      public long AccountId;
      public int ChampionId;
      public int Spell1;
      public int Spell2;
      public bool Left;
      public Dictionary<string, int> Statistics;
      public string Map;
      public int IpEarned;
    }

    [Serializable]
    internal class JavascriptyTeam
    {
      public int[] Bans;
      public RiotJsTransformer.JavascriptyPlayer[] Members;
    }

    [Serializable]
    internal class JsRerollState
    {
      public int Points;
      public int MaximumPoints;
      public int RerollCost;
    }

    [Serializable]
    internal class JavascriptyPlayer
    {
      public string Name;
      public string InternalName;
      public string RealmId;
      public double AccountId;
      public double SummonerId;
      public int TeamId;
      public bool IsDude;
      public string PickState;
      public RiotJsTransformer.JsRerollState RerollState;
      public int[] SpellIds;
      public int ChampionId;
      public int SkinId;
    }
  }
}
