// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.SettingsService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("settings.local")]
  public class SettingsService : JsApiService
  {
    private static string StorageLocation = Path.Combine(LaunchData.DataDirectory, "settings");

    [MicroApiMethod("store")]
    public async Task StoreAsync(JObject obj)
    {
      using (StreamWriter streamWriter = new StreamWriter(SettingsService.StorageLocation, false, Encoding.UTF8))
      {
        string json = obj.ToString(Formatting.None);
        await streamWriter.WriteAsync(json);
      }
    }

    [MicroApiMethod("read")]
    public async Task<object> ReadAsync()
    {
      object obj;
      using (StreamReader streamReader = new StreamReader(SettingsService.StorageLocation, Encoding.UTF8))
      {
        string json = await streamReader.ReadToEndAsync();
        obj = (object) JObject.Parse(json);
      }
      return obj;
    }
  }
}
