// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.RealmInstallationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Complete.IO;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.Daemons;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Standard.Riot;
using WintermintData.Riot.RealmDownload;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("realm")]
  public class RealmInstallationService : JsApiService
  {
    [MicroApiMethod("installationState")]
    public async Task<object> IsPlayable(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RealmInstallationService.\u003C\u003Ec__DisplayClass5 cDisplayClass5 = new RealmInstallationService.\u003C\u003Ec__DisplayClass5();
      // ISSUE: variable of a compiler-generated type
      RealmInstallationService.\u003C\u003Ec__DisplayClass5 cDisplayClass5_1 = cDisplayClass5;
      // ISSUE: reference to a compiler-generated field
      if (RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (RealmInstallationService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "realmId", typeof (RealmInstallationService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) RealmInstallationService.\u003CIsPlayable\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      string str = func((CallSite) callSite, obj);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass5_1.realmId = str;
      // ISSUE: reference to a compiler-generated method
      RiotUpdateDaemon.UpdaterState update = Enumerable.FirstOrDefault<RiotUpdateDaemon.UpdaterState>((IEnumerable<RiotUpdateDaemon.UpdaterState>) Instances.RiotUpdater.Updates, new Func<RiotUpdateDaemon.UpdaterState, bool>(cDisplayClass5.\u003CIsPlayable\u003Eb__3));
      // ISSUE: reference to a compiler-generated field
      string radsDirectory = this.GetRadsDirectory(cDisplayClass5.realmId);
      string lockPath = Path.Combine(radsDirectory, "lock");
      RealmDownloadsDescription realms = await DownloadsService.Get();
      // ISSUE: reference to a compiler-generated method
      bool hasRealm = Enumerable.Any<RealmDownloadItem>((IEnumerable<RealmDownloadItem>) realms.Downloads, new Func<RealmDownloadItem, bool>(cDisplayClass5.\u003CIsPlayable\u003Eb__4));
      return (object) new
      {
        Exists = (hasRealm && Directory.Exists(radsDirectory)),
        Locked = (hasRealm && FileEx.IsFileLocked(lockPath)),
        Update = (!hasRealm || update == null ? null : new
        {
          Status = update.Status,
          Position = update.Position,
          Length = update.Length
        })
      };
    }

    public string GetRadsDirectory(string realmId)
    {
      return Path.Combine(LaunchData.RiotContainerDirectory, string.Format("league#{0}", (object) realmId), "rads");
    }
  }
}
