// Decompiled with JetBrains decompiler
// Type: WintermintClient.Riot.StateChangedEventArgs
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;

namespace WintermintClient.Riot
{
  public class StateChangedEventArgs : EventArgs
  {
    public ConnectionState OldState;
    public ConnectionState NewState;

    public StateChangedEventArgs(ConnectionState oldState, ConnectionState newState)
    {
      this.OldState = oldState;
      this.NewState = newState;
    }
  }
}
