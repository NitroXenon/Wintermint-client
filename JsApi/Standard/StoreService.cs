// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.StoreService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("store")]
  public class StoreService : JsApiService
  {
    [MicroApiMethod("legacy.uri")]
    public async Task<string> GetStoreUri(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (StoreService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "handle", typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) StoreService.\u003CGetStoreUri\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      int handle = func((CallSite) callSite, obj);
      RiotAccount account = JsApiService.AccountBag.Get(handle);
      return await account.InvokeAsync<string>("loginService", "getStoreUrl");
    }

    [MicroApiMethod("legacy.open")]
    public async Task OpenStore(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, StoreService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetStoreUri", (IEnumerable<Type>) null, typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Site9.Target((CallSite) StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Site9, this, args);
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetAwaiter", (IEnumerable<Type>) null, typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea.Target((CallSite) StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitea, obj1);
      object obj3 = obj2;
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (StoreService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> func = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> callSite = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Siteb;
      // ISSUE: reference to a compiler-generated field
      if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec == null)
      {
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsCompleted", typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec.Target((CallSite) StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitec, obj3);
      if (!func((CallSite) callSite, obj4))
      {
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = 0;
        object obj = obj2;
        ICriticalNotifyCompletion awaiter1 = obj2 as ICriticalNotifyCompletion;
        if (awaiter1 == null)
        {
          INotifyCompletion awaiter2 = (INotifyCompletion) obj2;
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitOnCompleted<INotifyCompletion, StoreService.\u003COpenStore\u003Ed__f>(ref awaiter2, this);
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<ICriticalNotifyCompletion, StoreService.\u003COpenStore\u003Ed__f>(ref awaiter1, this);
        }
      }
      else
      {
        object obj5 = obj2;
        // ISSUE: reference to a compiler-generated field
        if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sited == null)
        {
          // ISSUE: reference to a compiler-generated field
          StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sited = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetResult", (IEnumerable<Type>) null, typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object uri = StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sited.Target((CallSite) StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sited, obj5);
        // ISSUE: reference to a compiler-generated field
        if (StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee == null)
        {
          // ISSUE: reference to a compiler-generated field
          StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Start", (IEnumerable<Type>) null, typeof (StoreService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee.Target((CallSite) StoreService.\u003COpenStore\u003Eo__SiteContainer8.\u003C\u003Ep__Sitee, typeof (Process), uri);
      }
    }

    [MicroApiMethod("acquire")]
    public void PurchaseItem()
    {
      throw new PlatformNotSupportedException();
    }

    [MicroApiMethod("list")]
    public void ListItems()
    {
      throw new PlatformNotSupportedException();
    }

    [MicroApiMethod("dynamic")]
    public void DynamicInvoke()
    {
      throw new PlatformNotSupportedException();
    }
  }
}
