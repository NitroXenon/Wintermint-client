// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.Riot.AccountService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Complete.Async;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintClient.Riot;
using WintermintData.Riot.Account;

namespace WintermintClient.JsApi.Standard.Riot
{
  [MicroApiService("riot.accounts")]
  public class AccountService : JsApiService
  {
    private readonly AsyncLock asyncLock = new AsyncLock();

    private async Task<AccountSettings> GetAccountSettings()
    {
      AccountSettings accountSettings;
      using (await this.asyncLock.LockAsync())
        accountSettings = await JsApiService.Client.Invoke<AccountSettings>("riot.accounts.get", (object) true);
      return accountSettings;
    }

    [MicroApiMethod("activate")]
    public void Activate(object args)
    {
      if (JsApiService.RiotAccount != null && JsApiService.RiotAccount.IsBlocked)
        throw new JsApiException("blocked");
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (AccountService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site7.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site7;
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "handle", typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site8.Target((CallSite) AccountService.\u003CActivate\u003Eo__SiteContainer6.\u003C\u003Ep__Site8, args);
      RiotAccount account = JsApiService.AccountBag.Get(func((CallSite) callSite, obj));
      JsApiService.Client.Invoke("riot.accounts.activate", (object) new AccountReference()
      {
        RealmId = account.RealmId,
        Username = account.Username
      });
      JsApiService.AccountBag.SetActive(account);
    }

    [MicroApiMethod("sync")]
    public async Task SyncAccounts(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, AccountService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "UploadAccounts", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitea.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitea, this, args);
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Siteb == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Siteb = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetAwaiter", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Siteb.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Siteb, obj1);
      object obj3 = obj2;
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitec == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitec = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (AccountService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func1 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitec.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite1 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitec;
      // ISSUE: reference to a compiler-generated field
      if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sited == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsCompleted", typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sited.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sited, obj3);
      object obj;
      if (!func1((CallSite) callSite1, obj4))
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = 0;
        obj = obj2;
        ICriticalNotifyCompletion awaiter1 = obj2 as ICriticalNotifyCompletion;
        if (awaiter1 == null)
        {
          INotifyCompletion awaiter2 = (INotifyCompletion) obj2;
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitOnCompleted<INotifyCompletion, AccountService.\u003CSyncAccounts\u003Ed__14>(ref awaiter2, this);
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<ICriticalNotifyCompletion, AccountService.\u003CSyncAccounts\u003Ed__14>(ref awaiter1, this);
        }
      }
      else
      {
        object obj5 = obj2;
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitee == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitee = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetResult", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitee.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitee, obj5);
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitef == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, AccountService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DownloadAccounts", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitef.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Sitef, this, args);
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetAwaiter", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site10.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site10, obj6);
        object obj8 = obj7;
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (AccountService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> func2 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> callSite2 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site11;
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsCompleted", typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site12.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site12, obj8);
        if (!func2((CallSite) callSite2, obj9))
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003E1__state = 1;
          obj = obj7;
          ICriticalNotifyCompletion awaiter1 = obj7 as ICriticalNotifyCompletion;
          if (awaiter1 == null)
          {
            INotifyCompletion awaiter2 = (INotifyCompletion) obj7;
            // ISSUE: explicit reference operation
            // ISSUE: reference to a compiler-generated field
            (^this).\u003C\u003Et__builder.AwaitOnCompleted<INotifyCompletion, AccountService.\u003CSyncAccounts\u003Ed__14>(ref awaiter2, this);
          }
          else
          {
            // ISSUE: explicit reference operation
            // ISSUE: reference to a compiler-generated field
            (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<ICriticalNotifyCompletion, AccountService.\u003CSyncAccounts\u003Ed__14>(ref awaiter1, this);
          }
        }
        else
        {
          object obj10 = obj7;
          // ISSUE: reference to a compiler-generated field
          if (AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site13 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site13 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GetResult", (IEnumerable<Type>) null, typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site13.Target((CallSite) AccountService.\u003CSyncAccounts\u003Eo__SiteContainer9.\u003C\u003Ep__Site13, obj10);
          Instances.RiotUpdater.TryUpdate();
        }
      }
    }

    [MicroApiMethod("push")]
    public async Task UploadAccounts(object args)
    {
      using (await this.asyncLock.LockAsync())
      {
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site18 = CallSite<Func<CallSite, object, JArray>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (JArray), typeof (AccountService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, JArray> func = AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site18.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, JArray>> callSite = AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site18;
        // ISSUE: reference to a compiler-generated field
        if (AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site19 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accounts", typeof (AccountService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site19.Target((CallSite) AccountService.\u003CUploadAccounts\u003Eo__SiteContainer17.\u003C\u003Ep__Site19, args);
        AccountDto[] accounts = Enumerable.ToArray<AccountDto>(Enumerable.Distinct<AccountDto>(Enumerable.Select<JToken, AccountDto>((IEnumerable<JToken>) func((CallSite) callSite, obj), (Func<JToken, AccountDto>) (x => x.ToObject<AccountDto>())), (IEqualityComparer<AccountDto>) AccountDtoComparer.Instance));
        await JsApiService.Client.Invoke("riot.accounts.set", new object[1]
        {
          (object) accounts
        });
      }
    }

    [MicroApiMethod("pull")]
    public async Task DownloadAccounts(JToken args)
    {
      bool cleanse = Enumerable.Contains<JToken>((IEnumerable<JToken>) args, (JToken) "cleanse") && args[(object) "cleanse"].ToObject<bool>();
      Func<string, string, bool> equals = (Func<string, string, bool>) ((a, b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase));
      Func<RiotAccount, AccountConfig, bool> accountsEquivalent = (Func<RiotAccount, AccountConfig, bool>) ((config, account) =>
      {
        if (equals(config.Username, account.Username) && equals(config.RealmId, account.RealmId))
          return equals(config.Password, account.Password);
        else
          return false;
      });
      AccountSettings settings = await this.GetAccountSettings();
      AccountConfig[] accountConfigs = settings.Accounts;
      RiotAccount[] currentAccounts = JsApiService.AccountBag.GetAll();
      if (cleanse)
      {
        foreach (RiotAccount account in currentAccounts)
          JsApiService.AccountBag.Detach(account);
        bool flag;
        if (!flag)
          ;
      }
      else
      {
        foreach (RiotAccount account in Enumerable.Where<RiotAccount>((IEnumerable<RiotAccount>) currentAccounts, (Func<RiotAccount, bool>) (x =>
        {
          if (!Enumerable.Any<AccountConfig>((IEnumerable<AccountConfig>) accountConfigs, (Func<AccountConfig, bool>) (config => accountsEquivalent(x, config))))
            return true;
          else
            return x.State == ConnectionState.Error;
        })))
          JsApiService.AccountBag.Detach(account);
      }
      foreach (AccountConfig config in accountConfigs)
        JsApiService.AccountBag.Attach(config);
      RiotAccount activeAccount = (RiotAccount) null;
      RiotAccount[] accounts = JsApiService.AccountBag.GetAll();
      AccountReference activeReference = settings.Active;
      if (activeReference != null)
        activeAccount = Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) accounts, (Func<RiotAccount, bool>) (x =>
        {
          if (string.Equals(activeReference.Username, x.Username, StringComparison.OrdinalIgnoreCase))
            return string.Equals(activeReference.RealmId, x.RealmId, StringComparison.OrdinalIgnoreCase);
          else
            return false;
        }));
      if (activeAccount == null)
      {
        activeAccount = Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) accounts);
        if (activeAccount != null)
          JsApiService.Client.Invoke("riot.accounts.activate", (object) new AccountReference()
          {
            Username = activeAccount.Username,
            RealmId = activeAccount.RealmId
          });
      }
      JsApiService.AccountBag.SetActive(activeAccount);
    }

    [MicroApiMethod("list")]
    public async Task<object> GetAccountList()
    {
      RiotAccount[] accounts = JsApiService.AccountBag.GetAll();
      Func<string, string, string> findSummonerNameForAccount = (Func<string, string, string>) ((realmId, username) =>
      {
        RiotAccount riotAccount = Enumerable.FirstOrDefault<RiotAccount>((IEnumerable<RiotAccount>) accounts, (Func<RiotAccount, bool>) (x =>
        {
          if (string.Equals(realmId, x.RealmId, StringComparison.OrdinalIgnoreCase))
            return string.Equals(username, x.Username, StringComparison.OrdinalIgnoreCase);
          else
            return false;
        }));
        if (riotAccount == null)
          return string.Empty;
        else
          return riotAccount.SummonerName;
      });
      AccountSettings settings = await this.GetAccountSettings();
      return (object) Enumerable.Select((IEnumerable<AccountConfig>) Enumerable.OrderBy<AccountConfig, string>((IEnumerable<AccountConfig>) settings.Accounts, (Func<AccountConfig, string>) (account => account.Username)), account => new
      {
        SummonerName = findSummonerNameForAccount(account.RealmId, account.Username),
        Username = account.Username,
        Password = account.Password,
        RealmId = account.RealmId
      });
    }
  }
}
