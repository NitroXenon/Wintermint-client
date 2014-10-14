// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.HostService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using WintermintClient;
using WintermintClient.JsApi;
using WintermintClient.Native;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("host")]
  public class HostService : JsApiService
  {
    [MicroApiMethod("openUrl")]
    public void OpenUri(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (HostService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite = HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "href", typeof (HostService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) HostService.\u003COpenUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      Process.Start(func((CallSite) callSite, obj));
    }

    [MicroApiMethod("spotlight")]
    public void Spotlight(object args)
    {
      IntPtr handle = Instances.WindowHandle;
      // ISSUE: reference to a compiler-generated field
      if (HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (HostService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func1 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite1 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site4;
      // ISSUE: reference to a compiler-generated field
      if (HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (HostService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> func2 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> callSite2 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site5;
      // ISSUE: reference to a compiler-generated field
      if (HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "count", typeof (HostService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site6.Target((CallSite) HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site6, args);
      // ISSUE: variable of the null type
      __Null local = null;
      object obj2 = func2((CallSite) callSite2, obj1, (object) local);
      int count;
      if (!func1((CallSite) callSite1, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (HostService)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int> func3 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int>> callSite3 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site7;
        // ISSUE: reference to a compiler-generated field
        if (HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "count", typeof (HostService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site8.Target((CallSite) HostService.\u003CSpotlight\u003Eo__SiteContainer3.\u003C\u003Ep__Site8, args);
        count = func3((CallSite) callSite3, obj3);
      }
      else
        count = 1;
      WindowFlasher.Flash(handle, count);
    }

    [MicroApiMethod("spotlight.pulse")]
    [MicroApiMethod("spotlight.start")]
    public void Pulse()
    {
      WindowFlasher.Pulse(Instances.WindowHandle);
    }

    [MicroApiMethod("spotlight.stop")]
    public void Stop()
    {
      WindowFlasher.Stop(Instances.WindowHandle);
    }
  }
}
