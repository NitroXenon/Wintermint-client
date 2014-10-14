// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.AccountNotFoundException
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Runtime.Serialization;

namespace WintermintClient.Riot
{
  internal class AccountNotFoundException : Exception
  {
    public AccountNotFoundException()
    {
    }

    public AccountNotFoundException(string message)
      : base(message)
    {
    }

    public AccountNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected AccountNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
