// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.MessyService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using System.Diagnostics;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.JsApi;
using WintermintData.Mess;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("mess")]
  public class MessyService : JsApiService
  {
    [MicroApiMethod("account")]
    public async Task<AccountMess> GetAccountMess()
    {
      AccountMess value = await JsApiService.Client.Invoke<AccountMess>("mess.account");
      return value;
    }

    [MicroApiMethod("forceRiotUpdate")]
    public void ForceRiotUpdate()
    {
      Instances.RiotUpdater.TryUpdate();
    }

    [MicroApiMethod("trello")]
    public void OpenTrelloBoard()
    {
      Process.Start("https://trello.com/b/eWEl2gJK");
    }
  }
}
