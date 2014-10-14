// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.UberChatClient
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;

namespace WintermintClient.Riot
{
  public class UberChatClient : ChatClient
  {
    public RiotAccount Account;

    public string RealmId
    {
      get
      {
        return this.Account.RealmId;
      }
    }

    public int AccountHandle
    {
      get
      {
        return this.Account.Handle;
      }
    }

    public UberChatClient(RiotAccount account)
    {
      this.Account = account;
    }
  }
}
