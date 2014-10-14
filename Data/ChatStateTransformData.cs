// Decompiled with JetBrains decompiler
// Type: WintermintClient.Data.ChatStateTransformData
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using Complete;
using Complete.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;

namespace WintermintClient.Data
{
  internal static class ChatStateTransformData
  {
    public static void Initialize()
    {
      ChatStatic.XmlStateTransform = new ChatStatic.XmlStateTransformFunction(ChatStateTransformData.XmlStateTransform);
      ChatStatic.JsonStateTransform = new ChatStatic.JsonStateTransformFunction(ChatStateTransformData.JsonStateTransform);
      ChatStatic.StringStateTransform = new ChatStatic.StringStateTransformFunction(ChatStateTransformData.StringStateTransform);
    }

    private static object XmlStateTransform(XDocument document)
    {
      XElement body = document.Element((XName) "body");
      if (body == null)
        return (object) null;
      Func<string, string> func = (Func<string, string>) (name =>
      {
        XElement xelement = body.Element((XName) name);
        if (xelement == null)
          return (string) null;
        else
          return xelement.Value;
      });
      long result;
      long.TryParse(func("timeStamp"), out result);
      return (object) new ChatStateTransformData.JsStatus()
      {
        Message = func("statusMsg"),
        Status = StringExtensions.Dasherize(func("gameStatus")),
        Game = new ChatStateTransformData.JsGameStatus()
        {
          ChampionId = ChampionNameData.GetChampionId(func("skinname")),
          Queue = func("gameQueueType"),
          Started = UnixDateTime.Epoch.AddMilliseconds((double) result)
        }
      };
    }

    private static object JsonStateTransform(JObject obj)
    {
      Func<string, string> func = (Func<string, string>) (name =>
      {
        JToken jtoken;
        if (!obj.TryGetValue(name, out jtoken))
          return (string) null;
        else
          return (string) jtoken;
      });
      return (object) new ChatStateTransformData.JsStatus()
      {
        Message = func("message"),
        Status = func("status")
      };
    }

    private static object StringStateTransform(string str)
    {
      return (object) new ChatStateTransformData.JsStatus()
      {
        Message = str,
        Status = "out-of-game"
      };
    }

    [Serializable]
    private class JsStatus
    {
      public string Message;
      public string Status;
      public ChatStateTransformData.JsGameStatus Game;
    }

    [Serializable]
    private class JsGameStatus
    {
      public int ChampionId;
      public string Queue;
      public DateTime Started;
    }
  }
}
