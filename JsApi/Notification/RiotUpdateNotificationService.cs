// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.RiotUpdateNotificationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.Daemons;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Standard.Riot;
using WintermintData.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class RiotUpdateNotificationService : JsApiService
  {
    public RiotUpdateNotificationService()
    {
      RiotUpdateDaemon riotUpdateDaemon = Instances.RiotUpdater;
      riotUpdateDaemon.Changed += (EventHandler<RiotUpdateDaemon.UpdaterState[]>) ((sender, states) => this.OnUpdateProgressAsync(states));
      riotUpdateDaemon.Completed += (EventHandler<RiotUpdateDaemon.UpdaterState>) ((sender, state) => this.UpdaterOnCompletedAsync(state));
    }

    private async Task UpdaterOnCompletedAsync(RiotUpdateDaemon.UpdaterState update)
    {
      SupportedRealm[] realms = await RealmService.GetRealms();
      JsApiService.Push("update:riot:completed", RiotUpdateNotificationService.GetJsonDto(realms, update));
    }

    private async Task OnUpdateProgressAsync(RiotUpdateDaemon.UpdaterState[] updates)
    {
      SupportedRealm[] realms = await RealmService.GetRealms();
      IEnumerable<object> transformed = Enumerable.Select<RiotUpdateDaemon.UpdaterState, object>(Enumerable.Where<RiotUpdateDaemon.UpdaterState>((IEnumerable<RiotUpdateDaemon.UpdaterState>) updates, (Func<RiotUpdateDaemon.UpdaterState, bool>) (update => !update.Completed)), (Func<RiotUpdateDaemon.UpdaterState, object>) (update => RiotUpdateNotificationService.GetJsonDto(realms, update)));
      JsApiService.Push("update:riot:progress", (object) transformed);
    }

    private static object GetJsonDto(SupportedRealm[] realms, RiotUpdateDaemon.UpdaterState update)
    {
      return (object) new
      {
        realm = ((Func<string, object>) (realmId => (object) Enumerable.FirstOrDefault<SupportedRealm>((IEnumerable<SupportedRealm>) realms, (Func<SupportedRealm, bool>) (x => realmId.Equals(x.Id, StringComparison.OrdinalIgnoreCase)))))(update.RealmId),
        status = update.Status,
        position = update.Position,
        length = update.Length
      };
    }
  }
}
