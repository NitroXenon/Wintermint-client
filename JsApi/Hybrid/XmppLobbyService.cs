// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Hybrid.XmppLobbyService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using Complete.Hex;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using RiotGames.Platform.Game;
using RiotGames.Platform.Matchmaking;
using RiotGames.Platform.Summoner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.JsApi.Notification;
using WintermintClient.JsApi.Standard;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Hybrid
{
  [MicroApiService("lobby", Preload = true)]
  public class XmppLobbyService : JsApiService
  {
    private readonly object lobbySyncObject;
    private readonly HashSet<XmppLobbyService.LobbyMember> members;
    private readonly HashSet<XmppLobbyService.LobbyMember> invitations;
    private RiotAccount account;
    private XmppLobbyService.Role role;
    private string inviteId;
    private int queueId;
    private int maxMembers;
    private bool autoAcceptInvitation;
    private XmppLobbyService.InvitationPassbackData respondStateObject;
    private long hostSummonerId;
    private string hostSummonerName;

    public XmppLobbyService()
    {
      this.invitations = new HashSet<XmppLobbyService.LobbyMember>(XmppLobbyService.LobbyMemberComparer.Instance);
      this.members = new HashSet<XmppLobbyService.LobbyMember>(XmppLobbyService.LobbyMemberComparer.Instance);
      this.lobbySyncObject = new object();
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.Chat.MailReceived += new EventHandler<Chat.MessageReceivedEventArgs>(this.OnMailReceived);
        account.Chat.MessageReceived += new EventHandler<Chat.MessageReceivedEventArgs>(this.OnMessageReceived);
        account.MessageReceived += new EventHandler<RtmpSharp.Messaging.MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult += new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.StateChanged += new EventHandler<StateChangedEventArgs>(this.OnAccountStateChanged);
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.Chat.MailReceived -= new EventHandler<Chat.MessageReceivedEventArgs>(this.OnMailReceived);
        account.Chat.MessageReceived -= new EventHandler<Chat.MessageReceivedEventArgs>(this.OnMessageReceived);
        account.MessageReceived -= new EventHandler<RtmpSharp.Messaging.MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult -= new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
        account.StateChanged -= new EventHandler<StateChangedEventArgs>(this.OnAccountStateChanged);
      });
      JsApiService.AccountBag.ActiveChanged += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        if (account.State != ConnectionState.Connected)
          return;
        this.CreateLobby(this.queueId);
      });
    }

    [MicroApiMethod("create")]
    public async Task Create(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      XmppLobbyService.\u003C\u003Ec__DisplayClass15 cDisplayClass15 = new XmppLobbyService.\u003C\u003Ec__DisplayClass15();
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site9;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "inviteId", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea.Target((CallSite) XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea, args);
      string inviteId = func1((CallSite) callSite1, obj1) ?? string.Format("wm-{0}", (object) Guid.NewGuid().ToString("N"));
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func2 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite2 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func3 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite3 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sited == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "role", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sited.Target((CallSite) XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sited, args);
      // ISSUE: variable of the null type
      __Null local = null;
      object obj3 = func3((CallSite) callSite3, obj2, (object) local);
      int num1;
      if (!func2((CallSite) callSite2, obj3))
      {
        num1 = 1;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (XmppLobbyService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> func4 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> callSite4 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "role", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef.Target((CallSite) XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Sitef, args);
        num1 = func4((CallSite) callSite4, obj4);
      }
      XmppLobbyService.Role role = (XmppLobbyService.Role) num1;
      // ISSUE: variable of a compiler-generated type
      XmppLobbyService.\u003C\u003Ec__DisplayClass15 cDisplayClass15_1 = cDisplayClass15;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func5 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite5 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site10;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "queueId", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site11.Target((CallSite) XmppLobbyService.\u003CCreate\u003Eo__SiteContainer8.\u003C\u003Ep__Site11, args);
      int num2 = func5((CallSite) callSite5, obj5);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass15_1.queueId = num2;
      GameQueueConfig[] queues = await XmppLobbyService.GetQueues(JsApiService.RiotAccount);
      // ISSUE: reference to a compiler-generated method
      GameQueueConfig queue = Enumerable.FirstOrDefault<GameQueueConfig>((IEnumerable<GameQueueConfig>) queues, new Func<GameQueueConfig, bool>(cDisplayClass15.\u003CCreate\u003Eb__12)) ?? Enumerable.FirstOrDefault<GameQueueConfig>((IEnumerable<GameQueueConfig>) Enumerable.OrderBy<GameQueueConfig, double>((IEnumerable<GameQueueConfig>) queues, (Func<GameQueueConfig, double>) (x => x.Id)));
      if (this.inviteId != inviteId)
      {
        if (this.role == XmppLobbyService.Role.Host)
        {
          lock (this.lobbySyncObject)
          {
            foreach (XmppLobbyService.LobbyMember item_0 in this.members)
              this.XmppMessage(JsApiService.GetSummonerJidFromId(item_0.SummonerId), "GAME_INVITE_OWNER_CANCEL", string.Empty);
          }
        }
        else if (this.respondStateObject != null)
          this.XmppMessage(JsApiService.GetSummonerJidFromId(this.respondStateObject.HostSummonerId), "GAME_INVITE_REJECT", this.respondStateObject.Xml);
        lock (this.lobbySyncObject)
        {
          this.members.Clear();
          this.invitations.Clear();
        }
      }
      this.inviteId = inviteId;
      this.queueId = (int) queue.Id;
      this.role = role;
      this.maxMembers = queue.MaximumParticipantListSize;
      this.autoAcceptInvitation = true;
      this.hostSummonerId = JsApiService.RiotAccount.SummonerId;
      this.hostSummonerName = JsApiService.RiotAccount.SummonerName;
      this.account = JsApiService.RiotAccount;
      this.NotifyAll();
    }

    private void CreateLobby(int queueId)
    {
      this.Create((object) new
      {
        queueId = queueId,
        role = (object) null,
        inviteId = (object) null
      });
    }

    [MicroApiMethod("invite")]
    public async Task Invite(object args)
    {
      if (this.account == null)
        throw new JsApiException("no-account");
      if (this.account.Game != null)
        throw new JsApiException("in-game");
      if (!this.role.HasFlag((Enum) XmppLobbyService.Role.Host))
        throw new JsApiException("not-host");
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site1f;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "invitorName", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20.Target((CallSite) XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site20, args);
      string invitorName = func1((CallSite) callSite1, obj1) ?? this.account.SummonerName;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site21;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22.Target((CallSite) XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site22, args);
      string jid = func2((CallSite) callSite2, obj2);
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site23 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site23 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site23.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site23;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "summonerName", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site24.Target((CallSite) XmppLobbyService.\u003CInvite\u003Eo__SiteContainer1e.\u003C\u003Ep__Site24, args);
      string summonerName = func3((CallSite) callSite3, obj3);
      if (jid != null)
      {
        long summonerId = JsApiService.GetSummonerIdFromJid(jid);
        await this.Invite(summonerId, invitorName);
      }
      else
      {
        PublicSummoner summoner = await JsApiService.GetSummoner(this.account.RealmId, summonerName);
        await this.Invite(summoner.SummonerId, invitorName);
      }
    }

    private async Task Invite(long summonerId, string invitorName)
    {
      string summonerName = await JsApiService.GetSummonerNameBySummonerId(this.account.RealmId, summonerId);
      GameQueueConfig queue = await XmppLobbyService.GetQueueById(this.account, this.queueId);
      string gameType = queue.Ranked ? "RANKED_GAME_PREMADE" : "NORMAL_GAME";
      int mapId = Enumerable.FirstOrDefault<int>((IEnumerable<int>) queue.SupportedMapIds);
      XDocument document = new XDocument(new object[1]
      {
        (object) new XElement((XName) "body", new object[7]
        {
          (object) new XElement((XName) "inviteId", (object) this.inviteId),
          (object) new XElement((XName) "userName", (object) invitorName),
          (object) new XElement((XName) "profileIcon", (object) 0),
          (object) new XElement((XName) "gameType", (object) gameType),
          (object) new XElement((XName) "mapId", (object) mapId),
          (object) new XElement((XName) "queueId", (object) this.queueId),
          (object) new XElement((XName) "gameMode", (object) "League of Legends")
        })
      });
      lock (this.lobbySyncObject)
        this.invitations.Add(new XmppLobbyService.LobbyMember()
        {
          SummonerId = summonerId,
          Name = summonerName
        });
      this.NotifyInvitations();
      string jid = JsApiService.GetSummonerJidFromId(summonerId);
      this.XmppMessage(jid, "GAME_INVITE", document.ToString(SaveOptions.DisableFormatting));
    }

    [MicroApiMethod("respond")]
    public void Respond(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3c == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3c = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (XmppLobbyService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func1 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3c.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite1 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3c;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3d == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3d = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accept", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3d.Target((CallSite) XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3d, args);
      bool flag = func1((CallSite) callSite1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3e == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3e = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func2 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3e.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite2 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3e;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3f == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3f = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func3 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3f.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite3 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site3f;
      // ISSUE: reference to a compiler-generated field
      if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site40 == null)
      {
        // ISSUE: reference to a compiler-generated field
        XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site40 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "passbackObject", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site40.Target((CallSite) XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site40, args);
      // ISSUE: variable of the null type
      __Null local1 = null;
      object obj3 = func3((CallSite) callSite3, obj2, (object) local1);
      XmppLobbyService.InvitationPassbackData invitationPassbackData;
      if (func2((CallSite) callSite2, obj3))
      {
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site41 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site41 = CallSite<Func<CallSite, object, XmppLobbyService.InvitationPassbackData>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (XmppLobbyService.InvitationPassbackData), typeof (XmppLobbyService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, XmppLobbyService.InvitationPassbackData> func4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site41.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, XmppLobbyService.InvitationPassbackData>> callSite4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site41;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site42 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site42 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "passbackObject", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site42.Target((CallSite) XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site42, args);
        invitationPassbackData = func4((CallSite) callSite4, obj4);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site43 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site43 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> func4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site43.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> callSite4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site43;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site44 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site44 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> func5 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site44.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> callSite5 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site44;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site45 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site45 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "passbackString", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site45.Target((CallSite) XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site45, args);
        // ISSUE: variable of the null type
        __Null local2 = null;
        object obj5 = func5((CallSite) callSite5, obj4, (object) local2);
        if (!func4((CallSite) callSite4, obj5))
          return;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site46 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site46 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (XmppLobbyService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> func6 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site46.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> callSite6 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site46;
        // ISSUE: reference to a compiler-generated field
        if (XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site47 == null)
        {
          // ISSUE: reference to a compiler-generated field
          XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site47 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "passbackString", typeof (XmppLobbyService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site47.Target((CallSite) XmppLobbyService.\u003CRespond\u003Eo__SiteContainer3b.\u003C\u003Ep__Site47, args);
        invitationPassbackData = XmppLobbyService.DecodePassbackString(func6((CallSite) callSite6, obj6));
      }
      int num1 = invitationPassbackData.Handle;
      long summonerId = invitationPassbackData.HostSummonerId;
      string str1 = invitationPassbackData.HostName;
      string message = invitationPassbackData.Xml;
      string str2 = invitationPassbackData.InviteId;
      int num2 = invitationPassbackData.QueueId;
      int num3 = invitationPassbackData.MapId;
      RiotAccount riotAccount = JsApiService.RiotAccount;
      if (flag && riotAccount.Handle != num1)
        throw new JsApiException("inactive-account");
      if (flag)
      {
        this.Create((object) new
        {
          role = XmppLobbyService.Role.Invitee,
          inviteId = str2,
          mapId = num3,
          queueId = num2
        });
        this.hostSummonerId = summonerId;
        this.hostSummonerName = str1;
        this.respondStateObject = invitationPassbackData;
      }
      string subject = flag ? "GAME_INVITE_ACCEPT" : "GAME_INVITE_REJECT";
      this.XmppMessage(JsApiService.GetSummonerJidFromId(summonerId), subject, message);
    }

    [MicroApiMethod("leave")]
    public void Leave(object args)
    {
      this.XmppMessage(JsApiService.GetSummonerJidFromId(this.hostSummonerId), "GAME_INVITE_REJECT", (string) null);
      this.role = XmppLobbyService.Role.None;
      this.NotifyRole();
    }

    [MicroApiMethod("queue")]
    public async Task Queue()
    {
      long[] team;
      lock (this.lobbySyncObject)
        team = Enumerable.ToArray<long>(Enumerable.Concat<long>((IEnumerable<long>) new long[1]
        {
          JsApiService.RiotAccount.SummonerId
        }, Enumerable.Select<XmppLobbyService.LobbyMember, long>((IEnumerable<XmppLobbyService.LobbyMember>) this.members, (Func<XmppLobbyService.LobbyMember, long>) (x => x.SummonerId))));
      SearchingForMatchNotification matchNotification = await this.account.InvokeAsync<SearchingForMatchNotification>("matchmakerService", "attachTeamToQueue", (object) new MatchMakerParams()
      {
        BotDifficulty = "MEDIUM",
        InvitationId = this.inviteId,
        QueueIds = new List<int>()
        {
          this.queueId
        },
        Team = Enumerable.ToList<long>((IEnumerable<long>) team)
      });
    }

    [MicroApiMethod("reinvite")]
    public void Reinvite()
    {
      if (!this.role.HasFlag((Enum) XmppLobbyService.Role.Host))
        return;
      lock (this.lobbySyncObject)
      {
        XmppLobbyService.LobbyMember[] local_0 = Enumerable.ToArray<XmppLobbyService.LobbyMember>((IEnumerable<XmppLobbyService.LobbyMember>) this.members);
        this.members.Clear();
        this.invitations.Clear();
        foreach (XmppLobbyService.LobbyMember item_0 in local_0)
          this.Invite(item_0.SummonerId, this.account.SummonerName);
      }
    }

    private void OnInvocationResult(object sender, InvocationResultEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Result);
    }

    private void OnFlexMessageReceived(object sender, RtmpSharp.Messaging.MessageReceivedEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Body);
    }

    private void OnData(RiotAccount account, object message)
    {
      GameDTO gameDto;
      if (account != this.account || (gameDto = message as GameDTO) == null || !(gameDto.GameState == "START_REQUESTED") && !(gameDto.GameState == "IN_PROGRESS"))
        return;
      this.autoAcceptInvitation = false;
      if (this.members != null)
        this.members.Clear();
      this.CreateLobby(this.queueId);
    }

    private void OnMessageReceived(object sender, Chat.MessageReceivedEventArgs e)
    {
      try
      {
        this.ProcessMessage(sender, e);
      }
      catch (Exception ex)
      {
      }
    }

    private void ProcessMessage(object sender, Chat.MessageReceivedEventArgs e)
    {
      if (e.MessageId == null || !e.MessageId.StartsWith("hm_") || (!this.role.HasFlag((Enum) XmppLobbyService.Role.Invitee) || !string.Equals(this.hostSummonerName, e.Sender.Name, StringComparison.OrdinalIgnoreCase)))
        return;
      this.UpdateRoomFromHost(XDocument.Parse(e.Message).Element((XName) "body"));
    }

    private void OnMailReceived(object sender, Chat.MessageReceivedEventArgs e)
    {
      this.ProcessMail(sender, e);
    }

    private async Task ProcessMail(object sender, Chat.MessageReceivedEventArgs args)
    {
      if (XmppLobbyService.IsSupportedSubject(args.Subject))
      {
        UberChatClient chat = (UberChatClient) sender;
        RiotAccount account = chat.Account;
        string senderJid = args.Sender.BareJid;
        long senderSummonerId = JsApiService.GetSummonerIdFromJid(senderJid);
        if (args.Subject == "GAME_INVITE_OWNER_CANCEL")
        {
          if (senderSummonerId == this.hostSummonerId)
          {
            JsApiService.Push("lobby:disband", (object) new
            {
              hostSummonerName = this.hostSummonerName
            });
            this.CreateLobby(this.queueId);
          }
        }
        else if (args.Subject == "GAME_INVITE_REJECT")
        {
          lock (this.lobbySyncObject)
          {
            if (this.members.RemoveWhere((Predicate<XmppLobbyService.LobbyMember>) (x => x.SummonerId == senderSummonerId)) > 0)
              this.NotifyMembers();
          }
        }
        else
        {
          XElement data = XDocument.Parse(args.Message).Element((XName) "body");
          if (this.role.HasFlag((Enum) XmppLobbyService.Role.Invitee) && senderSummonerId == this.hostSummonerId && args.Subject == "GAME_INVITE_LIST_STATUS")
          {
            this.UpdateRoomFromHost(data);
          }
          else
          {
            string inviteId = data.Element((XName) "inviteId").Value;
            if (args.Subject == "GAME_INVITE")
            {
              string name = data.Element((XName) "userName").Value;
              int mapId = int.Parse(data.Element((XName) "mapId").Value);
              int queueId = int.Parse(data.Element((XName) "queueId").Value);
              GameQueueConfig queue = await XmppLobbyService.GetQueueById(account, queueId);
              GameService.JsGameMap[] maps = await GameService.GetMaps(account);
              GameService.JsGameMap map = Enumerable.First<GameService.JsGameMap>((IEnumerable<GameService.JsGameMap>) maps, (Func<GameService.JsGameMap, bool>) (x => x.Id == mapId));
              GameTypeConfigDTO gameTypeConfig = Enumerable.First<GameTypeConfigDTO>((IEnumerable<GameTypeConfigDTO>) account.GameTypeConfigs, (Func<GameTypeConfigDTO, bool>) (x => x.Id == (double) queue.GameTypeConfigId));
              string senderName = await JsApiService.GetSummonerNameByJid(account.RealmId, args.Sender.BareJid);
              XmppLobbyService.InvitationPassbackData passbackObject = new XmppLobbyService.InvitationPassbackData()
              {
                InviteId = inviteId,
                MapId = mapId,
                QueueId = queueId,
                Handle = account.Handle,
                HostSummonerId = senderSummonerId,
                HostName = senderName,
                Xml = args.Message
              };
              if (this.autoAcceptInvitation && this.role.HasFlag((Enum) XmppLobbyService.Role.Invitee) && this.hostSummonerId == senderSummonerId)
                this.Respond((object) new
                {
                  accept = true,
                  handle = account.Handle,
                  passbackObject = passbackObject
                });
              else
                JsApiService.Push("lobby:invite", (object) new
                {
                  InviteId = inviteId,
                  Contact = ContactsNotificationService.JsContact.Create(chat, args.Sender),
                  Timestamp = args.Timestamp,
                  Sender = senderName,
                  Invitor = name,
                  MapName = map.Name,
                  QueueName = queue.Type,
                  GameTypeConfigName = gameTypeConfig.Name,
                  Handle = account.Handle,
                  PassbackString = XmppLobbyService.EncodePassbackString(passbackObject)
                });
            }
            else
            {
              if (this.role.HasFlag((Enum) XmppLobbyService.Role.Host) && this.inviteId == inviteId)
              {
                XmppLobbyService.LobbyMember invitedDude;
                lock (this.lobbySyncObject)
                  invitedDude = Enumerable.FirstOrDefault<XmppLobbyService.LobbyMember>((IEnumerable<XmppLobbyService.LobbyMember>) this.invitations, (Func<XmppLobbyService.LobbyMember, bool>) (x => x.SummonerId == senderSummonerId));
                switch (args.Subject)
                {
                  case "GAME_INVITE_ACCEPT":
                    lock (this.lobbySyncObject)
                    {
                      string local_1;
                      if (invitedDude != null)
                      {
                        if (this.members.Count < this.maxMembers)
                        {
                          local_1 = "GAME_INVITE_ACCEPT_ACK";
                          this.invitations.Remove(invitedDude);
                          this.members.Add(invitedDude);
                          this.NotifyMembers();
                          this.NotifyInvitations();
                        }
                        else
                        {
                          local_1 = "GAME_INVITE_REJECT_GAME_FULL";
                          this.invitations.Remove(invitedDude);
                          this.NotifyInvitations();
                        }
                      }
                      else
                        local_1 = "GAME_MSG_OUT_OF_SYNC";
                      this.XmppMessage(senderJid, local_1, args.Message);
                      goto label_53;
                    }
                  case "GAME_INVITE_SUGGEST":
                    string jid = args.Sender.BareJid;
                    lock (this.lobbySyncObject)
                    {
                      if (Enumerable.All<XmppLobbyService.LobbyMember>((IEnumerable<XmppLobbyService.LobbyMember>) this.members, (Func<XmppLobbyService.LobbyMember, bool>) (x => x.SummonerId != senderSummonerId)))
                        goto label_53;
                    }
                    string internalName = new JabberId(jid).User;
                    string str = data.Element((XName) "suggestedInviteJid").Value;
                    XmppLobbyService xmppLobbyService = this;
                    string summonerNameByJid = await JsApiService.GetSummonerNameByJid(account.RealmId, internalName);
                    var fAnonymousTypec = new
                    {
                      jid = str,
                      invitorName = summonerNameByJid
                    };
                    xmppLobbyService.Invite((object) fAnonymousTypec);
                    break;
                  case "VERIFY_INVITEE_ACK":
                  case "VERIFY_INVITEE_NAK":
                    goto label_53;
                }
              }
              if (this.role.HasFlag((Enum) XmppLobbyService.Role.Invitee) && senderSummonerId == this.hostSummonerId && this.inviteId == inviteId)
              {
                switch (args.Subject)
                {
                  case "GAME_INVITE_ACCEPT_ACK":
                    this.NotifyInviteId();
                    break;
                  case "GAME_INVITE_ALLOW_SUGGESTIONS":
                    this.role |= XmppLobbyService.Role.Invitor;
                    this.NotifyRole();
                    break;
                  case "GAME_INVITE_DISALLOW_SUGGESTIONS":
                    this.role &= ~XmppLobbyService.Role.Invitor;
                    this.NotifyRole();
                    break;
                  case "GAME_INVITE_CANCEL":
                    this.role = XmppLobbyService.Role.None;
                    this.NotifyRole();
                    break;
                  case "GAME_INVITE_REJECT_GAME_FULL":
                    JsApiService.Push("lobby:full", (object) inviteId);
                    this.CreateLobby(this.queueId);
                    break;
                  case "GAME_MSG_OUT_OF_SYNC":
                    JsApiService.Push("lobby:lostSync", (object) inviteId);
                    this.CreateLobby(this.queueId);
                    break;
                  case "VERIFY_INVITEE":
                    object obj = await JsApiService.RiotAccount.InvokeAsync<object>("matchmakerService", "acceptInviteForMatchmakingGame", (object) inviteId);
                    XElement document = new XElement((XName) "body", (object) new XElement((XName) "inviteId", (object) inviteId));
                    this.XmppMessage(senderJid, "VERIFY_INVITEE_ACK", document.ToString(SaveOptions.DisableFormatting));
                    break;
                }
              }
            }
          }
        }
      }
label_53:;
    }

    private void OnAccountStateChanged(object sender, StateChangedEventArgs e)
    {
      if (e.NewState != ConnectionState.Connected || this.account == sender)
        return;
      this.CreateLobby(this.queueId);
    }

    private void NotifyInvitations()
    {
      JsApiService.Push("lobby:invitations", (object) this.invitations);
    }

    private void NotifyRole()
    {
      JsApiService.Push("lobby:role", (object) this.role);
    }

    private void NotifyQueueId()
    {
      JsApiService.Push("lobby:queueId", (object) this.queueId);
    }

    private void NotifyInviteId()
    {
      JsApiService.Push("lobby:join", (object) new
      {
        InviteId = this.inviteId,
        HostName = this.hostSummonerName
      });
    }

    private void NotifyAll()
    {
      this.NotifyRole();
      this.NotifyQueueId();
      this.NotifyInviteId();
      this.NotifyInvitations();
      this.NotifyMembers();
    }

    private void NotifyMembers()
    {
      XmppLobbyService.LobbyMember[] lobbyMemberArray;
      lock (this.lobbySyncObject)
      {
        RiotAccount local_1 = JsApiService.RiotAccount;
        XmppLobbyService.LobbyMember[] temp_20;
        if (this.role != XmppLobbyService.Role.Host)
          temp_20 = new XmppLobbyService.LobbyMember[1]
          {
            new XmppLobbyService.LobbyMember()
            {
              Name = this.hostSummonerName,
              SummonerId = this.hostSummonerId
            }
          };
        else
          temp_20 = new XmppLobbyService.LobbyMember[1]
          {
            new XmppLobbyService.LobbyMember()
            {
              Name = local_1.SummonerName,
              SummonerId = local_1.SummonerId
            }
          };
        lobbyMemberArray = Enumerable.ToArray<XmppLobbyService.LobbyMember>(Enumerable.Concat<XmppLobbyService.LobbyMember>((IEnumerable<XmppLobbyService.LobbyMember>) temp_20, (IEnumerable<XmppLobbyService.LobbyMember>) this.members));
      }
      JsApiService.Push("lobby:members", (object) lobbyMemberArray);
    }

    private void UpdateRoomFromHost(XElement data)
    {
      XElement xelement = data.Element((XName) "invitelist");
      if (xelement == null)
        return;
      \u003C\u003Ef__AnonymousType10<string, XmppLobbyService.LobbyMember>[] source = Enumerable.ToArray(Enumerable.Select(Enumerable.Select(Enumerable.Select((IEnumerable<XElement>) Enumerable.ToArray<XElement>(xelement.Elements((XName) "invitee")), invite => new
      {
        invite = invite,
        name = invite.Attribute((XName) "name").Value
      }), param0 => new
      {
        \u003C\u003Eh__TransparentIdentifier80 = param0,
        status = param0.invite.Attribute((XName) "status").Value
      }), param0 =>
      {
        string status = param0.status;
        XmppLobbyService.LobbyMember lobbyMember = new XmppLobbyService.LobbyMember()
        {
          Name = param0.\u003C\u003Eh__TransparentIdentifier80.name
        };
        return new
        {
          Status = status,
          Member = lobbyMember
        };
      }));
      lock (this.lobbySyncObject)
      {
        this.invitations.Clear();
        foreach (\u003C\u003Ef__AnonymousType10<string, XmppLobbyService.LobbyMember> item_0 in Enumerable.Where(source, x => x.Status == "PENDING"))
          this.invitations.Add(item_0.Member);
        this.members.Clear();
        foreach (\u003C\u003Ef__AnonymousType10<string, XmppLobbyService.LobbyMember> item_1 in Enumerable.Where(source, x => x.Status == "ACCEPTED"))
          this.members.Add(item_1.Member);
      }
      this.NotifyInvitations();
      this.NotifyMembers();
    }

    private void XmppMessage(string jid, string subject, string message)
    {
      if (this.account == null || this.account.Chat == null)
        return;
      this.account.Chat.Chat.Message(jid, subject, message);
    }

    private static bool IsSupportedSubject(string subject)
    {
      switch (subject)
      {
        case "GAME_INVITE":
        case "GAME_INVITE_ACCEPT":
        case "GAME_INVITE_ACCEPT_ACK":
        case "GAME_INVITE_ALLOW_SUGGESTIONS":
        case "GAME_INVITE_CANCEL":
        case "GAME_INVITE_DISALLOW_SUGGESTIONS":
        case "GAME_INVITE_LIST_STATUS":
        case "GAME_INVITE_OWNER_CANCEL":
        case "GAME_INVITE_REJECT":
        case "GAME_INVITE_REJECT_GAME_FULL":
        case "GAME_INVITE_SUGGEST":
        case "GAME_MSG_OUT_OF_SYNC":
        case "PRACTICE_GAME_INVITE":
        case "PRACTICE_GAME_INVITE_ACCEPT":
        case "PRACTICE_GAME_INVITE_ACCEPT_ACK":
        case "PRACTICE_GAME_JOIN":
        case "PRACTICE_GAME_JOIN_ACK":
        case "PRACTICE_GAME_OWNER_CHANGE":
        case "VERIFY_INVITEE":
        case "VERIFY_INVITEE_ACK":
        case "VERIFY_INVITEE_NAK":
        case "VERIFY_INVITEE_RESET":
          return true;
        default:
          return false;
      }
    }

    private static Task<GameQueueConfig[]> GetQueues(RiotAccount account)
    {
      return account.InvokeCachedAsync<GameQueueConfig[]>("matchmakerService", "getAvailableQueues");
    }

    private static async Task<GameQueueConfig> GetQueueById(RiotAccount account, int queueId)
    {
      GameQueueConfig[] queues = await XmppLobbyService.GetQueues(account);
      return Enumerable.FirstOrDefault<GameQueueConfig>((IEnumerable<GameQueueConfig>) queues, (Func<GameQueueConfig, bool>) (x => x.Id == (double) queueId));
    }

    private static string EncodePassbackString(XmppLobbyService.InvitationPassbackData data)
    {
      return HexadecimalExtensions.ToHex(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) data)));
    }

    private static XmppLobbyService.InvitationPassbackData DecodePassbackString(string str)
    {
      return JsonConvert.DeserializeObject<XmppLobbyService.InvitationPassbackData>(Encoding.UTF8.GetString(HexadecimalExtensions.ToBytes(str)));
    }

    [Flags]
    public enum Role
    {
      None = 0,
      Host = 1,
      Invitee = 2,
      Invitor = 4,
      PowerfulInvitee = Invitor | Invitee,
    }

    private class LobbyMember
    {
      public long SummonerId;
      public string Name;
    }

    private sealed class LobbyMemberComparer : IEqualityComparer<XmppLobbyService.LobbyMember>
    {
      public static readonly IEqualityComparer<XmppLobbyService.LobbyMember> Instance = (IEqualityComparer<XmppLobbyService.LobbyMember>) new XmppLobbyService.LobbyMemberComparer();

      public bool Equals(XmppLobbyService.LobbyMember x, XmppLobbyService.LobbyMember y)
      {
        if (object.ReferenceEquals((object) x, (object) y))
          return true;
        if (object.ReferenceEquals((object) x, (object) null) || object.ReferenceEquals((object) y, (object) null) || x.GetType() != y.GetType())
          return false;
        else
          return string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
      }

      public int GetHashCode(XmppLobbyService.LobbyMember obj)
      {
        if (obj.Name == null)
          return 0;
        else
          return obj.Name.ToLowerInvariant().GetHashCode();
      }
    }

    private class InvitationPassbackData
    {
      public string InviteId;
      public int MapId;
      public int QueueId;
      public int Handle;
      public long HostSummonerId;
      public string HostName;
      public string Xml;
    }
  }
}
