// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.AccountBagNotificationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class AccountBagNotificationService : JsApiService
  {
    public AccountBagNotificationService()
    {
      JsApiService.AccountBag.AccountAdded += new EventHandler<RiotAccount>(this.OnBagStateChanged);
      JsApiService.AccountBag.AccountRemoved += new EventHandler<RiotAccount>(this.OnBagStateChanged);
      JsApiService.AccountBag.ActiveChanged += new EventHandler<RiotAccount>(this.OnActiveAccountChanged);
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.StateChanged += new EventHandler<StateChangedEventArgs>(this.OnAccountStateChanged);
        account.QueuePositionChanged += new EventHandler<int>(this.OnQueuePositionChanged);
        account.WaitDelayChanged += new EventHandler<DateTime>(this.OnWaitDelayChanged);
        JsApiService.Push("wm:account:added", AccountBagNotificationService.TransformAccount(account));
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.StateChanged -= new EventHandler<StateChangedEventArgs>(this.OnAccountStateChanged);
        account.QueuePositionChanged -= new EventHandler<int>(this.OnQueuePositionChanged);
        account.WaitDelayChanged -= new EventHandler<DateTime>(this.OnWaitDelayChanged);
        JsApiService.Push("wm:account:removed", AccountBagNotificationService.TransformAccount(account));
      });
    }

    private void OnWaitDelayChanged(object sender, DateTime waitingUntil)
    {
      this.NotifyAccounts();
    }

    private void OnQueuePositionChanged(object sender, int i)
    {
      this.NotifyAccounts();
    }

    private void OnAccountStateChanged(object sender, StateChangedEventArgs e)
    {
      this.NotifyAccounts();
    }

    private void OnBagStateChanged(object sender, RiotAccount account)
    {
      this.NotifyAccounts();
    }

    private void OnActiveAccountChanged(object sender, RiotAccount riotAccount)
    {
      this.NotifyAccounts();
    }

    private void NotifyAccounts()
    {
      object[] objArray = Enumerable.ToArray<object>((IEnumerable<object>) Enumerable.OrderBy<object, object>(Enumerable.Select<RiotAccount, object>((IEnumerable<RiotAccount>) JsApiService.AccountBag.GetAll(), new Func<RiotAccount, object>(AccountBagNotificationService.TransformAccount)), (Func<object, object>) (x =>
      {
        // ISSUE: reference to a compiler-generated field
        if (AccountBagNotificationService.\u003CNotifyAccounts\u003Eo__SiteContainer4.\u003C\u003Ep__Site5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountBagNotificationService.\u003CNotifyAccounts\u003Eo__SiteContainer4.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Username", typeof (AccountBagNotificationService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        return AccountBagNotificationService.\u003CNotifyAccounts\u003Eo__SiteContainer4.\u003C\u003Ep__Site5.Target((CallSite) AccountBagNotificationService.\u003CNotifyAccounts\u003Eo__SiteContainer4.\u003C\u003Ep__Site5, x);
      })));
      JsApiService.Push("wm:accounts", (object) objArray);
      JsApiService.Push("wm:accounts:active", AccountBagNotificationService.TransformAccount(JsApiService.AccountBag.Active));
    }

    private static object TransformAccount(RiotAccount account)
    {
      if (account == null)
        return (object) null;
      else
        return (object) new
        {
          Username = account.Username,
          Name = account.SummonerName,
          AccountId = account.AccountId,
          SummonerId = account.SummonerId,
          RealmId = account.RealmId,
          RealmName = account.RealmName,
          RealmFullName = account.RealmFullName,
          Handle = account.Handle,
          Active = (account == JsApiService.AccountBag.Active),
          State = account.State,
          QueuePosition = account.QueuePosition,
          WaitingUntil = account.WaitingUntil,
          ErrorReason = account.ErrorReason
        };
    }
  }
}
