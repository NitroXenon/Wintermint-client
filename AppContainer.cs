// Decompiled with JetBrains decompiler
// Type: WintermintClient.AppContainer
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Browser;
using Browser.BrowserWindows;
using Browser.Rpc;
using FileDatabase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.JsApi.ApiHost;
using WintermintClient.Util;

namespace WintermintClient
{
  internal class AppContainer
  {
    private static readonly AppContainer.SchemeFileDbLink[] WintermintSchemes = new AppContainer.SchemeFileDbLink[3]
    {
      new AppContainer.SchemeFileDbLink("astral", "support", "http/ui/"),
      new AppContainer.SchemeFileDbLink("astral-data", "support", ""),
      new AppContainer.SchemeFileDbLink("astral-media", "media", "")
    };
    public ManualResetEvent CloseHandle = new ManualResetEvent(false);
    public IBrowserWindow window;
    private WintermintApiHost api;
    private Dictionary<string, string> mimeTypes;
    private Dictionary<string, AppContainer.SchemeFileDbLink> schemeMap;

    public void Initialize()
    {
      AppContainer.InitializeDebugEnvironment();
      Process.GetCurrentProcess().PriorityBoostEnabled = true;
      Instances.InitializeAsync(Enumerable.ToArray<string>(Enumerable.Select<AppContainer.SchemeFileDbLink, string>((IEnumerable<AppContainer.SchemeFileDbLink>) AppContainer.WintermintSchemes, (Func<AppContainer.SchemeFileDbLink, string>) (x => x.DatabaseName)))).Wait();
      this.schemeMap = Enumerable.ToDictionary<AppContainer.SchemeFileDbLink, string>((IEnumerable<AppContainer.SchemeFileDbLink>) AppContainer.WintermintSchemes, (Func<AppContainer.SchemeFileDbLink, string>) (x => x.Scheme));
      this.mimeTypes = JsonConvert.DeserializeObject<Dictionary<string, string>>(Instances.SupportFiles.GetString("http/mimetypes.json"));
      BrowserEngine.DataRequest += new EventHandler<DataRequest>(this.OnDataRequest);
      JsApiService.Push = (JsApiService.JsPush) ((key, obj) => PushNotification.Send(this.window.CefBrowser, key, obj));
      JsApiService.PushJson = (JsApiService.JsPushJson) ((key, json) => PushNotification.SendJson(this.window.CefBrowser, key, json));
      this.window = BrowserWindowFactory.Create();
      this.window.RequestReceived += (EventHandler<RequestContext>) ((sender, context) => this.api.ProcessRequest(context));
      this.window.BrowserClosed += (EventHandler) ((sender, e) => this.CloseHandle.Set());
      this.window.BrowserCreated += (EventHandler) ((sender, e) =>
      {
        Instances.WindowHandle = this.window.Handle;
        AppUserModelId.SetWintermintProperties(this.window.Handle);
        this.api = new WintermintApiHost(this.window);
      });
    }

    private static void InitializeDebugEnvironment()
    {
      Console.Out.WriteLineAsync("::host.require = ~> 0.0.1");
      Console.Out.WriteLineAsync("::host.generate_reports = all");
      Console.Out.WriteLineAsync("::host.plugin_additional_paths = /home/kevin/env/wintermint/debug-plugins");
      Console.Out.WriteLineAsync("::host.plugin_autoload = true");
      Console.Out.WriteLineAsync("::host.source_map = /home/kevin/code/wintermint-ui/debugging/symbols.bin");
      Console.Out.WriteLineAsync("::host.league_path = /home/kevin/env/wintermint/league-of-legends/na/");
    }

    private async void OnDataRequest(object sender, DataRequest request)
    {
      try
      {
        await this.ProcessDataRequest(request);
      }
      catch (Exception ex)
      {
        request.SetNoData();
      }
    }

    private async Task ProcessDataRequest(DataRequest request)
    {
      request.Headers.Add("Access-Control-Allow-Origin", "astral://prototype");
      Uri uri = new Uri(request.Url);
      AppContainer.SchemeFileDbLink schemeData = this.schemeMap[uri.Scheme];
      string path = schemeData.PathPrefix + uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
      string extension = this.GetExtension(path);
      string mimeType = this.GetMimeType(extension);
      IFileDb database = Instances.FileDatabases[schemeData.DatabaseName];
      using (Stream stream = database.GetStream(path.ToLowerInvariant()))
      {
        MemoryStream memoryStream = new MemoryStream();
        await stream.CopyToAsync((Stream) memoryStream);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        request.SetData((Stream) memoryStream, mimeType);
      }
    }

    private string GetMimeType(string extension)
    {
      string str;
      if (!this.mimeTypes.TryGetValue(extension, out str))
        return this.mimeTypes["default"];
      else
        return str;
    }

    private string GetExtension(string path)
    {
      int num1 = path.LastIndexOf('/');
      int num2 = path.LastIndexOf('.');
      if (num2 <= num1)
        return "";
      else
        return path.Substring(num2 + 1);
    }

    private struct SchemeFileDbLink
    {
      public readonly string Scheme;
      public readonly string DatabaseName;
      public readonly string PathPrefix;

      public SchemeFileDbLink(string scheme, string databaseName, string pathPrefix)
      {
        this.Scheme = scheme;
        this.DatabaseName = databaseName;
        this.PathPrefix = pathPrefix;
      }
    }
  }
}
