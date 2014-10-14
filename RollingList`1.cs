// Decompiled with JetBrains decompiler
// Type: WintermintClient.RollingList`1
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace WintermintClient
{
  internal class RollingList<T>
  {
    private readonly Queue<T> items;
    private readonly int limit;

    public RollingList(int limit)
    {
      this.limit = limit;
      this.items = new Queue<T>(limit);
    }

    public void Push(T value)
    {
      while (this.items.Count >= this.limit)
        this.items.Dequeue();
      this.items.Enqueue(value);
    }

    public int Count(Func<T, bool> predicate)
    {
      return Enumerable.Count<T>((IEnumerable<T>) this.items, predicate);
    }
  }
}
