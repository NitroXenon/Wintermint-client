// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.GameJsApiService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using WintermintClient.Data;

namespace WintermintClient.JsApi
{
  public abstract class GameJsApiService : JsApiService
  {
    internal static string GetAllowSpectators(string jsValue)
    {
      switch (jsValue.ToLowerInvariant())
      {
        case "any":
          return "ALL";
        case "dropin":
          return "DROPINONLY";
        case "lobby":
          return "LOBBYONLY";
        default:
          return "NONE";
      }
    }

    internal static string GetGameMode(string jsValue)
    {
      switch (jsValue.ToLowerInvariant())
      {
        case "aram":
          return "ARAM";
        case "classic":
          return "CLASSIC";
        case "dominion":
          return "ODIN";
        case "tutorial":
          return "TUTORIAL";
        default:
          throw new ArgumentException("jsValue");
      }
    }

    internal static string GetGameMode(int mapId)
    {
      switch (mapId)
      {
        case 3:
        case 7:
          return "ARAM";
        case 8:
          return "ODIN";
        default:
          return "CLASSIC";
      }
    }

    internal static string GetGameMapFriendlyName(int value)
    {
      switch (GameData.GetMapClassification(value))
      {
        case "crystal-scar":
          return "Crystal Scar";
        case "howling-abyss":
          return "Howling Abyss";
        case "proving-grounds":
          return "Proving Grounds";
        case "summoners-rift":
          return "Summoner's Rift";
        case "twisted-treeline":
          return "Twisted Treeline";
        default:
          return "Unknown";
      }
    }

    internal static string GetGameState(string gameState)
    {
      switch (gameState)
      {
        case "TEAM_SELECT":
          return "team-select";
        case "PRE_CHAMP_SELECT":
        case "CHAMP_SELECT":
        case "POST_CHAMP_SELECT":
          return "hero-select";
        case "IN_PROGRESS":
          return "in-progress";
        case "START_REQUESTED":
          return "game-init";
        case "JOINING_CHAMP_SELECT":
          return "match-found";
        case "TERMINATED":
        case "TERMINATED_IN_ERROR":
        case "FAILED_TO_START":
          return "error";
        default:
          return "unknown";
      }
    }

    internal static string GetGameHeroSelectState(string gameState)
    {
      switch (gameState)
      {
        case "TEAM_SELECT":
          return "team";
        case "PRE_CHAMP_SELECT":
          return "pre";
        case "CHAMP_SELECT":
          return "pick";
        case "POST_CHAMP_SELECT":
          return "post";
        case "JOINING_CHAMP_SELECT":
          return "found";
        default:
          return "none";
      }
    }
  }
}
