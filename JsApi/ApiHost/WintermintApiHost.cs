// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.ApiHost.WintermintApiHost
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Browser.BrowserWindows;
using Browser.Rpc;
using Complete.Extensions;
using MicroApi;
using RiotGames.Platform.Messaging;
using RtmpSharp.Messaging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;

namespace WintermintClient.JsApi.ApiHost
{
  public class WintermintApiHost
  {
    private readonly MicroApiHost api;
    private readonly IBrowserWindow browserWindow;

    public WintermintApiHost(IBrowserWindow browserWindow)
    {
      this.browserWindow = browserWindow;
      this.api = new MicroApiHost(InstantiationPolicy.Once);
      this.api.LoadServices(typeof (WintermintApiHost).Assembly);
    }

    public async void ProcessRequest(RequestContext request)
    {
      try
      {
        await this.ProcessRequestInternal(request);
      }
      catch (Exception ex)
      {
      }
    }

    public async Task ProcessRequestInternal(RequestContext request)
    {
      try
      {
        request.ChangeCefBrowser(this.browserWindow.CefBrowser);
        JsApiService.JsResponse onProgress = new JsApiService.JsResponse(request.OnProgress);
        JsApiService.JsResponse onResult = new JsApiService.JsResponse(request.OnResult);
        MethodData data = this.api.GetMethod(request.Method);
        ParameterInfo[] parameters = data.Parameters;
        int parameterCount = data.Parameters.Length;
        bool param1IsJsResponse = parameterCount >= 1 && parameters[0].ParameterType == typeof (JsApiService.JsResponse);
        object[] args;
        switch (parameterCount)
        {
          case 0:
            args = new object[0];
            break;
          case 1:
            object[] objArray1;
            if (!param1IsJsResponse)
              objArray1 = new object[1]
              {
                request.Argument
              };
            else
              objArray1 = new object[1]
              {
                (object) onResult
              };
            args = objArray1;
            break;
          case 2:
            object[] objArray2;
            if (!param1IsJsResponse)
              objArray2 = new object[2]
              {
                request.Argument,
                (object) onResult
              };
            else
              objArray2 = new object[2]
              {
                (object) onProgress,
                (object) onResult
              };
            args = objArray2;
            break;
          case 3:
            args = new object[3]
            {
              request.Argument,
              (object) onProgress,
              (object) onResult
            };
            break;
          default:
            throw new ArgumentException("Unknown number of parameters in microapi method.");
        }
        object result = await this.api.InvokeAsync(request.Method, args);
        JsonResult json = result as JsonResult;
        onResult(json != null ? (object) json.Json : result);
      }
      catch (MissingMethodException ex)
      {
        request.OnFault((object) new
        {
          Reason = "missing-method",
          Data = (object) null
        });
      }
      catch (Exception ex)
      {
        Exception exception = ex;
        TargetInvocationException invocationException1 = exception as TargetInvocationException;
        if (invocationException1 != null)
          exception = invocationException1.InnerException;
        string str = (string) null;
        object obj = (object) null;
        JsApiException jsApiException = exception as JsApiException;
        if (jsApiException != null)
        {
          str = jsApiException.Reason;
          obj = jsApiException.Info;
        }
        InvocationException invocationException2 = exception as InvocationException;
        if (invocationException2 != null)
        {
          PlatformException platformException = invocationException2.RootCause as PlatformException;
          if (platformException != null)
            str = platformException.RootCauseClassname;
        }
        request.OnFault((object) new
        {
          Reason = (str ?? WintermintApiHost.GetJsStyleClassName((object) exception)),
          Data = obj
        });
      }
    }

    private static string GetJsStyleClassName(object obj)
    {
      if (obj == null)
        return "null";
      else
        return StringExtensions.Dasherize(obj.GetType().Name);
    }
  }
}
