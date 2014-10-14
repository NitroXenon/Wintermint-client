// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.ChatStatusService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using Complete;
using Complete.Extensions;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using RiotGames.Platform.Game;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("chat.status", Preload = true)]
  public class ChatStatusService : JsApiService
  {
    private static string InactiveXmlStatus = string.Format("<body>  <profileIcon>532</profileIcon>  <level>30</level>  <wins>1</wins>  <leaves>0</leaves>  <odinWins>0</odinWins>  <odinLeaves>0</odinLeaves>  <queueType />  <rankedLosses>0</rankedLosses>  <rankedRating>0</rankedRating>  <tier>DIAMOND</tier>  <statusMsg></statusMsg>  <timeStamp>{0}</timeStamp>  <rankedLeagueName>Wintermint Dreamyland</rankedLeagueName>  <rankedLeagueDivision>I</rankedLeagueDivision>  <rankedLeagueTier>DIAMOND</rankedLeagueTier>  <rankedLeagueQueue>RANKED_SOLO_5x5</rankedLeagueQueue>  <isObservable>ALL</isObservable>  <gameQueueType>{1}</gameQueueType>  <skinname>{2}</skinname>  <gameStatus>{3}</gameStatus></body>", (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) "outOfGame");
    private const string StatusXmlFormat = "<body>  <profileIcon>532</profileIcon>  <level>30</level>  <wins>1</wins>  <leaves>0</leaves>  <odinWins>0</odinWins>  <odinLeaves>0</odinLeaves>  <queueType />  <rankedLosses>0</rankedLosses>  <rankedRating>0</rankedRating>  <tier>DIAMOND</tier>  <statusMsg></statusMsg>  <timeStamp>{0}</timeStamp>  <rankedLeagueName>Wintermint Dreamyland</rankedLeagueName>  <rankedLeagueDivision>I</rankedLeagueDivision>  <rankedLeagueTier>DIAMOND</rankedLeagueTier>  <rankedLeagueQueue>RANKED_SOLO_5x5</rankedLeagueQueue>  <isObservable>ALL</isObservable>  <gameQueueType>{1}</gameQueueType>  <skinname>{2}</skinname>  <gameStatus>{3}</gameStatus></body>";
    private string timestamp;
    private RiotAccount __account;
    private string __type;
    private DateTime __timestamp;

    public ChatStatusService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        ChatClient.__Presence presence = account.Chat.Presence;
        presence.Message = ChatStatusService.InactiveXmlStatus;
        presence.Status = PresenceType.Online;
      });
    }

    [MicroApiMethod("set")]
    public void Chat(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site3 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (ChatStatusService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func1 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite1 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site3;
      // ISSUE: reference to a compiler-generated field
      if (ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accountHandle", typeof (ChatStatusService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site4.Target((CallSite) ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site4, args);
      int handle = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatStatusService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site5;
      // ISSUE: reference to a compiler-generated field
      if (ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "type", typeof (ChatStatusService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site6.Target((CallSite) ChatStatusService.\u003CChat\u003Eo__SiteContainer2.\u003C\u003Ep__Site6, args);
      string type = func2((CallSite) callSite2, obj2);
      RiotAccount activeAccount = JsApiService.AccountBag.Get(handle);
      string timestamp = this.GetTimestamp(activeAccount, type);
      if (this.timestamp == timestamp)
        return;
      this.timestamp = timestamp;
      string xmlStatus = this.GetXmlStatus(activeAccount, timestamp, type);
      try
      {
        ChatClient.__Presence presence = activeAccount.Chat.Presence;
        presence.Message = xmlStatus;
        presence.Status = ChatStatusService.GetStatusForType(type);
        presence.Post();
      }
      catch
      {
      }
      foreach (RiotAccount riotAccount in Enumerable.Where<RiotAccount>((IEnumerable<RiotAccount>) JsApiService.AccountBag.GetAll(), (Func<RiotAccount, bool>) (x => x != activeAccount)))
      {
        try
        {
          ChatClient.__Presence presence = riotAccount.Chat.Presence;
          presence.Message = ChatStatusService.InactiveXmlStatus;
          presence.Status = PresenceType.Online;
          presence.Post();
        }
        catch
        {
        }
      }
    }

    private static PresenceType GetStatusForType(string type)
    {
      switch (type)
      {
        case "in-game":
        case "in-queue":
          return PresenceType.Busy;
        default:
          return PresenceType.Online;
      }
    }

    private string GetXmlStatus(RiotAccount account, string timestamp, string type)
    {
      GameDTO gameDto = account.Game ?? new GameDTO();
      PlayerChampionSelectionDTO championSelectionDto = Enumerable.FirstOrDefault<PlayerChampionSelectionDTO>((IEnumerable<PlayerChampionSelectionDTO>) (gameDto.PlayerChampionSelections ?? new List<PlayerChampionSelectionDTO>()), (Func<PlayerChampionSelectionDTO, bool>) (x => x.SummonerInternalName == account.SummonerInternalName)) ?? new PlayerChampionSelectionDTO();
      return string.Format("<body>  <profileIcon>532</profileIcon>  <level>30</level>  <wins>1</wins>  <leaves>0</leaves>  <odinWins>0</odinWins>  <odinLeaves>0</odinLeaves>  <queueType />  <rankedLosses>0</rankedLosses>  <rankedRating>0</rankedRating>  <tier>DIAMOND</tier>  <statusMsg></statusMsg>  <timeStamp>{0}</timeStamp>  <rankedLeagueName>Wintermint Dreamyland</rankedLeagueName>  <rankedLeagueDivision>I</rankedLeagueDivision>  <rankedLeagueTier>DIAMOND</rankedLeagueTier>  <rankedLeagueQueue>RANKED_SOLO_5x5</rankedLeagueQueue>  <isObservable>ALL</isObservable>  <gameQueueType>{1}</gameQueueType>  <skinname>{2}</skinname>  <gameStatus>{3}</gameStatus></body>", (object) timestamp, string.IsNullOrEmpty(gameDto.QueueTypeName) ? (object) "NONE" : (object) gameDto.QueueTypeName, (object) ChampionNameData.GetLegacyChampionClientNameOrSoraka(championSelectionDto.ChampionId), (object) StringExtensions.Camelize(type));
    }

    private string GetTimestamp(RiotAccount account, string type)
    {
      if (account != this.__account || type != this.__type)
        this.__timestamp = DateTime.UtcNow;
      this.__account = account;
      this.__type = type;
      return ((long) (this.__timestamp - UnixDateTime.Epoch).TotalMilliseconds).ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
