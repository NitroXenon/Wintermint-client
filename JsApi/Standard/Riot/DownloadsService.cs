// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.Riot.DownloadsService
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
using WintermintClient.JsApi;
using WintermintData.Riot.Account;
using WintermintData.Riot.RealmDownload;

namespace WintermintClient.JsApi.Standard.Riot
{
  [MicroApiService("riot.downloads")]
  public class DownloadsService : JsApiService
  {
    private static readonly AsyncLock AsyncLock = new AsyncLock();

    [MicroApiMethod("get")]
    public static async Task<RealmDownloadsDescription> Get()
    {
      RealmDownloadsDescription downloadsDescription;
      using (await DownloadsService.AsyncLock.LockAsync())
        downloadsDescription = await JsApiService.Client.InvokeCached<RealmDownloadsDescription>("storage.get", (object) "download-realms");
      return downloadsDescription;
    }

    [MicroApiMethod("set")]
    public static async Task Set(object args)
    {
      using (await DownloadsService.AsyncLock.LockAsync())
      {
        // ISSUE: reference to a compiler-generated field
        if (DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, JToken>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (JToken), typeof (DownloadsService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, JToken> func = DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, JToken>> callSite = DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site7;
        // ISSUE: reference to a compiler-generated field
        if (DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "downloads", typeof (DownloadsService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site8.Target((CallSite) DownloadsService.\u003CSet\u003Eo__SiteContainer6.\u003C\u003Ep__Site8, args);
        JToken obj = func((CallSite) callSite, obj1);
        RealmDownloadItem[] downloads = obj.ToObject<RealmDownloadItem[]>();
        RealmDownloadsDescription description = new RealmDownloadsDescription()
        {
          Downloads = downloads
        };
        AccountSettings settings = await JsApiService.Client.Invoke<AccountSettings>("riot.accounts.get", (object) false);
        AccountConfig[] accounts = settings.Accounts;
        description.Downloads = Enumerable.ToArray<RealmDownloadItem>(Enumerable.Where<RealmDownloadItem>((IEnumerable<RealmDownloadItem>) description.Downloads, (Func<RealmDownloadItem, bool>) (x => Enumerable.Any<AccountConfig>((IEnumerable<AccountConfig>) accounts, (Func<AccountConfig, bool>) (account => string.Equals(account.RealmId, x.RealmId, StringComparison.OrdinalIgnoreCase))))));
        object obj2 = await JsApiService.Client.Invoke<object>("storage.set", new object[2]
        {
          (object) "download-realms",
          (object) description
        });
        JsApiService.Client.Purge<RealmDownloadsDescription>("storage.get", (object) "download-realms");
      }
    }
  }
}
