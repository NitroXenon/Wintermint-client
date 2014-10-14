// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Standard.ChatService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using MicroApi;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WintermintClient.JsApi;

namespace WintermintClient.JsApi.Standard
{
  [MicroApiService("chat")]
  public class ChatService : JsApiService
  {
    private static ChatClient GetChatClient(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int> func = ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int>> callSite = ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site1;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "accountHandle", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site2.Target((CallSite) ChatService.\u003CGetChatClient\u003Eo__SiteContainer0.\u003C\u003Ep__Site2, args);
      return (ChatClient) JsApiService.AccountBag.Get(func((CallSite) callSite, obj)).Chat;
    }

    [MicroApiMethod("chat")]
    public void Chat(object args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatService.\u003C\u003Ec__DisplayClassb cDisplayClassb1 = new ChatService.\u003C\u003Ec__DisplayClassb();
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site4 = CallSite<Func<CallSite, object, ChatClient>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ChatClient), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, ChatClient> func1 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, ChatClient>> callSite1 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site4;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site5 = CallSite<Func<CallSite, ChatService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetChatClient", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site5.Target((CallSite) ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site5, this, args);
      ChatClient chatClient = func1((CallSite) callSite1, obj1);
      ChatClient.__Chat chat = chatClient.Chat;
      // ISSUE: variable of a compiler-generated type
      ChatService.\u003C\u003Ec__DisplayClassb cDisplayClassb2 = cDisplayClassb1;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site6;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site7.Target((CallSite) ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site7, args);
      string str = func2((CallSite) callSite2, obj2);
      // ISSUE: reference to a compiler-generated field
      cDisplayClassb2.jid = str;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site8 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site8;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "message", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site9.Target((CallSite) ChatService.\u003CChat\u003Eo__SiteContainer3.\u003C\u003Ep__Site9, args);
      string message = func3((CallSite) callSite3, obj3);
      // ISSUE: reference to a compiler-generated method
      if (Enumerable.Any<string>((IEnumerable<string>) chatClient.ConferenceServers, new Func<string, bool>(cDisplayClassb1.\u003CChat\u003Eb__a)))
      {
        // ISSUE: reference to a compiler-generated field
        chat.GroupChat(cDisplayClassb1.jid, message);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        chat.Chat(cDisplayClassb1.jid, message);
      }
    }

    [MicroApiMethod("send")]
    public void Message(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee = CallSite<Func<CallSite, ChatService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetChatClient", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee.Target((CallSite) ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitee, this, args);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Sitef;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site10.Target((CallSite) ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site10, args);
      string str1 = func1((CallSite) callSite1, obj2);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site11 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site11.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site11;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "subject", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site12.Target((CallSite) ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site12, args);
      string str2 = func2((CallSite) callSite2, obj3);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site13 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func3 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite3 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site13;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "message", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site14.Target((CallSite) ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site14, args);
      string str3 = func3((CallSite) callSite3, obj4);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site15 = CallSite<Action<CallSite, object, string, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Message", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, object, string, string, string> action = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site15.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, object, string, string, string>> callSite4 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site15;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Chat", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site16.Target((CallSite) ChatService.\u003CMessage\u003Eo__SiteContainerd.\u003C\u003Ep__Site16, obj1);
      string str4 = str1;
      string str5 = str2;
      string str6 = str3;
      action((CallSite) callSite4, obj5, str4, str5, str6);
    }

    [MicroApiMethod("join")]
    public void JoinRoom(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site18 = CallSite<Func<CallSite, ChatService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetChatClient", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site18.Target((CallSite) ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site18, this, args);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site19 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func1 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site19.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site19;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1a == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1a.Target((CallSite) ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1a, args);
      string str1 = func1((CallSite) callSite1, obj2);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1b == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1b = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func2 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1b.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite2 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1b;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1c == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1c = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "password", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1c.Target((CallSite) ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1c, args);
      string str2 = func2((CallSite) callSite2, obj3);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1d == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1d = CallSite<Action<CallSite, object, string, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Join", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, object, string, string> action = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1d.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, object, string, string>> callSite3 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1d;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1e == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1e = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Muc", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1e.Target((CallSite) ChatService.\u003CJoinRoom\u003Eo__SiteContainer17.\u003C\u003Ep__Site1e, obj1);
      string str3 = str1;
      string str4 = str2;
      action((CallSite) callSite3, obj4, str3, str4);
    }

    [MicroApiMethod("leave")]
    public void LeaveRoom(object args)
    {
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site20 = CallSite<Func<CallSite, ChatService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "GetChatClient", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site20.Target((CallSite) ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site20, this, args);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site21 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (ChatService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> func = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site21.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> callSite1 = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site21;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "jid", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site22.Target((CallSite) ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site22, args);
      string str1 = func((CallSite) callSite1, obj2);
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site23 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site23 = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Leave", (IEnumerable<Type>) null, typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Action<CallSite, object, string> action = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site23.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Action<CallSite, object, string>> callSite2 = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site23;
      // ISSUE: reference to a compiler-generated field
      if (ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Muc", typeof (ChatService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site24.Target((CallSite) ChatService.\u003CLeaveRoom\u003Eo__SiteContainer1f.\u003C\u003Ep__Site24, obj1);
      string str2 = str1;
      action((CallSite) callSite2, obj3, str2);
    }
  }
}
