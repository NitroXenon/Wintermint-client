// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.WintermintService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.JsApi.Helpers;
using WintermintData.Account;
using WintermintData.Protocol;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("wintermint", Preload = true)]
  public class WintermintService : JsApiService
  {
    private string email;
    private string password;

    public WintermintService()
    {
      JsApiService.Client.Disconnected += new EventHandler(this.OnClientDisconnected);
    }

    private async void OnClientDisconnected(object sender, EventArgs e)
    {
      JsApiService.Push("auth:disconnected", (object) null);
      try
      {
        await JsApiService.Client.AuthenticateAsync(this.email, this.password);
        JsApiService.Push("auth:success", (object) JsApiService.Client.Account);
        goto label_10;
      }
      catch (ProtocolVersionNegotiationException ex)
      {
        if (ex.IsOld)
          WintermintService.InitiateCaptiveUpdate();
        JsApiService.Push("auth:fail", (object) new JsApiException("protocol-mismatch"));
      }
      catch (Exception ex)
      {
        Exception exception = WintermintService.TransformAccountException(ex);
        if (exception != null)
          JsApiService.Push("auth:fail", (object) exception);
      }
      await Task.Delay(5000);
      ThreadPool.QueueUserWorkItem((WaitCallback) (x => this.OnClientDisconnected(sender, e)));
label_10:;
    }

    [MicroApiMethod("login")]
    public async Task Login(object args)
    {
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (WintermintService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> func1 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> callSite1 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site7;
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "email", typeof (WintermintService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site8.Target((CallSite) WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site8, args);
        this.email = func1((CallSite) callSite1, obj1);
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (WintermintService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> func2 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site9.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> callSite2 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Site9;
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Sitea == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "password", typeof (WintermintService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Sitea.Target((CallSite) WintermintService.\u003CLogin\u003Eo__SiteContainer6.\u003C\u003Ep__Sitea, args);
        this.password = func2((CallSite) callSite2, obj2);
        await JsApiService.Client.AuthenticateAsync(this.email, this.password);
        JsApiService.Push("auth:success", (object) JsApiService.Client.Account);
      }
      catch (Exception ex)
      {
        Exception exception = WintermintService.TransformAccountException(ex);
        if (exception != null)
          throw exception;
        else
          throw new JsApiException("unknown");
      }
    }

    [MicroApiMethod("edit.password")]
    public async Task ChangePassword(object args)
    {
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Sitef == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (WintermintService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> func1 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Sitef.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> callSite1 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Sitef;
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "oldPassword", typeof (WintermintService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site10.Target((CallSite) WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site10, args);
        string oldPassword = func1((CallSite) callSite1, obj1);
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (WintermintService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> func2 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> callSite2 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site11;
        // ISSUE: reference to a compiler-generated field
        if (WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "newPassword", typeof (WintermintService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site12.Target((CallSite) WintermintService.\u003CChangePassword\u003Eo__SiteContainere.\u003C\u003Ep__Site12, args);
        string newPassword = func2((CallSite) callSite2, obj2);
        await JsApiService.Client.Invoke("account.edit.password", new object[2]
        {
          (object) oldPassword,
          (object) newPassword
        });
        this.password = newPassword;
      }
      catch (ProtocolVersionNegotiationException ex)
      {
        if (ex.IsOld)
          WintermintService.InitiateCaptiveUpdate();
        throw new JsApiException("protocol-mismatch");
      }
      catch (Exception ex)
      {
        Exception exception = WintermintService.TransformAccountException(ex);
        if (exception != null)
          throw exception;
        else
          throw new JsApiException("unknown");
      }
    }

    private static Exception TransformAccountException(Exception exception)
    {
      AccountException accountException = exception as AccountException;
      if (accountException != null)
      {
        if (accountException is AccountNotFoundException)
          return (Exception) new JsApiException("account-not-found");
        if (accountException is AccountNotUniqueException)
          return (Exception) new JsApiException("account-not-unique");
        if (accountException is InvalidCredentialsException)
          return (Exception) new JsApiException("invalid-credentials");
        if (accountException is CredentialStrengthException)
          return (Exception) new JsApiException("credential-strength");
        HistoricalCredentialsException credentialsException = accountException as HistoricalCredentialsException;
        if (credentialsException != null)
          return (Exception) new JsApiException("historial-credentials", (object) new
          {
            LastValid = credentialsException.LastValid
          });
        else
          return (Exception) accountException;
      }
      else if (exception is ProtocolVersionNegotiationException)
        return (Exception) new JsApiException("protocol-mismatch");
      else
        return (Exception) null;
    }

    private static void InitiateCaptiveUpdate()
    {
      foreach (Process process in Enumerable.Where<Process>((IEnumerable<Process>) Process.GetProcesses(), (Func<Process, bool>) (x =>
      {
        if (!(x.ProcessName == "wintermint-update"))
          return x.ProcessName == "wintermint-update-ui";
        else
          return true;
      })))
      {
        try
        {
          process.Kill();
        }
        catch
        {
        }
      }
      if (LaunchData.TryLaunch("wintermint-update-ui", "role:captive-update"))
        Environment.Exit(0);
      int num = (int) MessageBox.Show("Wintermint needs to be updated because it's too old to talk with the server.\n\nHowever, the updater could not be started. Please update Wintermint manually.", "Wintermint Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }
}
