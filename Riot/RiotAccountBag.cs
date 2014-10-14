// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.RiotAccountBag
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using WintermintData.Riot.Account;

namespace WintermintClient.Riot
{
  internal class RiotAccountBag
  {
    private readonly HashSet<RiotAccount> accounts;

    public RiotAccount Active { get; private set; }

    public event EventHandler<RiotAccount> ActiveChanged;

    public event EventHandler<RiotAccount> AccountAdded;

    public event EventHandler<RiotAccount> AccountRemoved;

    public RiotAccountBag()
    {
      this.accounts = new HashSet<RiotAccount>();
    }

    public RiotAccount Attach(AccountConfig config)
    {
      RiotAccount riotAccount = Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) this.accounts, (Func<RiotAccount, bool>) (x =>
      {
        if (x.Username == config.Username)
          return x.RealmId == config.RealmId;
        else
          return false;
      }));
      if (riotAccount != null)
        return riotAccount;
      RiotAccount account = new RiotAccount(config)
      {
        CanConnect = true
      };
      account.ConnectAsync();
      this.accounts.Add(account);
      this.OnAccountAdded(account);
      return account;
    }

    public void Detach(RiotAccount account)
    {
      if (!this.accounts.Contains(account))
        return;
      if (account == this.Active)
        this.SetActive(Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) this.accounts, (Func<RiotAccount, bool>) (x =>
        {
          if (x != account)
            return x.RealmId == account.RealmId;
          else
            return false;
        })) ?? Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) this.accounts));
      account.CanConnect = false;
      this.accounts.Remove(account);
      account.Close();
      this.OnAccountRemoved(account);
    }

    public RiotAccount[] GetAll()
    {
      return Enumerable.ToArray<RiotAccount>((IEnumerable<RiotAccount>) this.accounts);
    }

    public void SetActive(RiotAccount account)
    {
      if (account != null && !this.accounts.Contains(account))
        throw new AccountNotFoundException("The specified account is not registered.");
      this.Active = account;
      this.FireActiveChanged(account);
    }

    public RiotAccount Get(int handle)
    {
      return Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) this.accounts, (Func<RiotAccount, bool>) (x => x.Handle == handle));
    }

    public RiotAccount Get(string realm)
    {
      return this.Get(realm, RiotAccountPreference.LeastContention);
    }

    public RiotAccount Get(string realm, RiotAccountPreference preference)
    {
      switch (preference)
      {
        case RiotAccountPreference.Active:
          if (this.Active == null)
            return (RiotAccount) null;
          if (!(this.Active.RealmId == realm))
            return (RiotAccount) null;
          else
            return this.Active;
        case RiotAccountPreference.Inactive:
        case RiotAccountPreference.LeastContention:
          IEnumerable<RiotAccount> source = Enumerable.Where<RiotAccount>((IEnumerable<RiotAccount>) this.accounts, (Func<RiotAccount, bool>) (x =>
          {
            if (x.RealmId == realm)
              return x.State == ConnectionState.Connected;
            else
              return false;
          }));
          if (preference == RiotAccountPreference.Inactive)
            source = Enumerable.Where<RiotAccount>(source, (Func<RiotAccount, bool>) (x => x != this.Active));
          RiotAccount[] riotAccountArray = Enumerable.ToArray<RiotAccount>(source);
          if (riotAccountArray.Length <= 0)
            return (RiotAccount) null;
          else
            return MoreEnumerable.MinBy<RiotAccount, int>((IEnumerable<RiotAccount>) riotAccountArray, (Func<RiotAccount, int>) (x => x.PendingInvocations));
        case RiotAccountPreference.InactivePreferred:
          return this.Get(realm, RiotAccountPreference.Inactive) ?? this.Get(realm, RiotAccountPreference.Active);
        default:
          throw new ArgumentOutOfRangeException("preference");
      }
    }

    private void AccountOnStateChanged(object sender, StateChangedEventArgs e)
    {
      RiotAccount riotAccount = sender as RiotAccount;
      if (riotAccount == null || e.NewState != ConnectionState.Disconnected || e.OldState == e.NewState)
        return;
      riotAccount.ReconnectThrottledAsync();
    }

    public void OnAccountAdded(RiotAccount account)
    {
      if (this.AccountAdded == null)
        return;
      account.StateChanged += new EventHandler<StateChangedEventArgs>(this.AccountOnStateChanged);
      this.AccountAdded((object) this, account);
    }

    public void OnAccountRemoved(RiotAccount account)
    {
      if (this.AccountRemoved == null)
        return;
      account.StateChanged -= new EventHandler<StateChangedEventArgs>(this.AccountOnStateChanged);
      this.AccountRemoved((object) this, account);
    }

    public void FireActiveChanged(RiotAccount account)
    {
      if (this.ActiveChanged == null)
        return;
      this.ActiveChanged((object) this, account);
    }
  }
}
