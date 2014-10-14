// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.ChatHost
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Collections.Generic;
using WintermintData.Riot.Account;

namespace WintermintClient.Riot
{
  public class ChatHost
  {
    public static UberChatClient Create(AccountConfig config, RiotAccount account)
    {
      Uri uri = new Uri(config.Endpoints.Chat.Uri);
      UberChatClient uberChatClient1 = new UberChatClient(account);
      uberChatClient1.Host = uri.Host;
      uberChatClient1.Port = uri.Port;
      uberChatClient1.Server = "pvp.net";
      uberChatClient1.Username = config.Username;
      uberChatClient1.Password = "AIR_" + config.Password;
      UberChatClient uberChatClient2 = uberChatClient1;
      uberChatClient2.ConferenceServers.AddRange((IEnumerable<string>) config.Endpoints.Chat.Conference);
      return uberChatClient2;
    }
  }
}
