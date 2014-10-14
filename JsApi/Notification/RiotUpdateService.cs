// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.RiotUpdateService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using System;
using WintermintClient;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class RiotUpdateService : JsApiService
  {
    public RiotUpdateService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) => account.StateChanged += new EventHandler<StateChangedEventArgs>(this.AccountOnStateChanged));
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) => account.StateChanged -= new EventHandler<StateChangedEventArgs>(this.AccountOnStateChanged));
    }

    private void AccountOnStateChanged(object sender, StateChangedEventArgs e)
    {
      RiotAccount riotAccount = (RiotAccount) sender;
      if (e.NewState != ConnectionState.Connected)
        return;
      Instances.RiotUpdater.TryUpdate(new string[1]
      {
        riotAccount.RealmId
      });
    }
  }
}
