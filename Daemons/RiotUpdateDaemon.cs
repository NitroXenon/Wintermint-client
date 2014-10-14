// Decompiled with JetBrains decompiler
// Type: WintermintClient.Daemons.RiotUpdateDaemon
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Complete.Interop.OS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WintermintClient;
using WintermintClient.Data;
using WintermintClient.Riot;
using WintermintData.Riot.RealmDownload;

namespace WintermintClient.Daemons
{
  internal class RiotUpdateDaemon
  {
    private static readonly TimeSpan UpdateCooldownInterval = TimeSpan.FromMinutes(5.0);
    private static TimeSpan InitialUpdateDelay = TimeSpan.FromMinutes(5.0);
    private static TimeSpan UpdateDelay = TimeSpan.FromHours(1.0);
    private static Regex RiotUpdaterProgressParser = new Regex("^(?<status>.*?)\\|(?<position>.*?)\\|(?<length>.*?)$", RegexOptions.Compiled | RegexOptions.ECMAScript);
    private const string ExecutableName = "riot-update";
    private readonly ConcurrentDictionary<string, RiotUpdateDaemon.UpdaterState> updates;
    private readonly Dictionary<string, DateTime> lastUpdates;

    public RiotUpdateDaemon.UpdaterState[] Updates
    {
      get
      {
        return Enumerable.ToArray<RiotUpdateDaemon.UpdaterState>(Enumerable.Where<RiotUpdateDaemon.UpdaterState>((IEnumerable<RiotUpdateDaemon.UpdaterState>) this.updates.Values, (Func<RiotUpdateDaemon.UpdaterState, bool>) (x => !x.Completed)));
      }
    }

    public event EventHandler<RiotUpdateDaemon.UpdaterState[]> Changed;

    public event EventHandler<RiotUpdateDaemon.UpdaterState> Completed;

    public RiotUpdateDaemon()
    {
      this.updates = new ConcurrentDictionary<string, RiotUpdateDaemon.UpdaterState>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.lastUpdates = new Dictionary<string, DateTime>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    public void Initialize()
    {
      this.RunLoop();
    }

    public Task TryUpdate()
    {
      return this.TryUpdate(Enumerable.ToArray<string>(Enumerable.Distinct<string>(Enumerable.Select<RiotAccount, string>((IEnumerable<RiotAccount>) Instances.AccountBag.GetAll(), (Func<RiotAccount, string>) (x => x.RealmId)))));
    }

    public async Task TryUpdate(string[] realmIdSubset)
    {
      string[] realms = Enumerable.ToArray<string>(Enumerable.Where<string>((IEnumerable<string>) realmIdSubset, new Func<string, bool>(this.CanUpdate)));
      if (realms.Length != 0)
      {
        try
        {
          RealmDownloadConfig[] allConfigurations = await Instances.Client.Invoke<RealmDownloadConfig[]>("riot.downloads.getPatchConfig", new object[2]
          {
            (object) "~> 0.0.0",
            (object) new string[4]
            {
              "windows",
              "legacy-v0",
              "pando-interop-disabled",
              "p2p-disabled"
            }
          });
          RealmDownloadConfig[] configurations = Enumerable.ToArray<RealmDownloadConfig>(Enumerable.Where<RealmDownloadConfig>((IEnumerable<RealmDownloadConfig>) allConfigurations, (Func<RealmDownloadConfig, bool>) (x => Enumerable.Contains<string>((IEnumerable<string>) realms, x.RealmId, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))));
          if (configurations.Length != 0)
            this.UpdateInstallations(allConfigurations);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void UpdateInstallations(RealmDownloadConfig[] configs)
    {
      IEnumerable<string> nonBusyRealms = Enumerable.Where<string>(Enumerable.Distinct<string>(Enumerable.Select<RiotAccount, string>(Enumerable.Where<RiotAccount>((IEnumerable<RiotAccount>) Instances.AccountBag.GetAll(), (Func<RiotAccount, bool>) (account => !account.IsBlocked)), (Func<RiotAccount, string>) (account => account.RealmId))), new Func<string, bool>(this.CanUpdate));
      foreach (RealmDownloadConfig realmDownloadConfig in Enumerable.ToArray<RealmDownloadConfig>(Enumerable.Where<RealmDownloadConfig>((IEnumerable<RealmDownloadConfig>) configs, (Func<RealmDownloadConfig, bool>) (x => Enumerable.Any<string>(nonBusyRealms, (Func<string, bool>) (y => y.Equals(x.RealmId, StringComparison.OrdinalIgnoreCase)))))))
      {
        string[] arguments = Enumerable.ToArray<string>(Enumerable.Select<RealmDownloadParameter, string>((IEnumerable<RealmDownloadParameter>) Enumerable.ToArray<RealmDownloadParameter>(Enumerable.Concat<RealmDownloadParameter>((IEnumerable<RealmDownloadParameter>) new RealmDownloadParameter[2]
        {
          new RealmDownloadParameter("rads-directory", Path.Combine(Path.Combine(LaunchData.RiotContainerDirectory, this.GetGameInstallationDirectory(realmDownloadConfig.RealmId)), "rads")),
          new RealmDownloadParameter("temporary-directory", LaunchData.GetTemporaryFolder("league-update"))
        }, (IEnumerable<RealmDownloadParameter>) realmDownloadConfig.Parameters)), (Func<RealmDownloadParameter, string>) (x => string.Format("{0}:{1}", (object) x.Name, (object) x.Value))));
        this.DoLocalUpdate(realmDownloadConfig.RealmId, arguments);
      }
    }

    private void RemoveUnusedInstallations(RealmDownloadConfig[] configs)
    {
      string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<RealmDownloadConfig, string>((IEnumerable<RealmDownloadConfig>) configs, (Func<RealmDownloadConfig, string>) (x => this.GetGameInstallationDirectory(x.RealmId))));
      foreach (DirectoryInfo directoryInfo in new DirectoryInfo(LaunchData.RiotContainerDirectory).GetDirectories("league#*"))
      {
        string directoryName = directoryInfo.Name;
        if (!Enumerable.Any<string>((IEnumerable<string>) strArray, (Func<string, bool>) (x => directoryName.Equals(x, StringComparison.OrdinalIgnoreCase))))
        {
          try
          {
            directoryInfo.Delete(true);
          }
          catch
          {
          }
        }
      }
    }

    private bool CanUpdate(string realmId)
    {
      lock (this.lastUpdates)
      {
        DateTime local_0;
        if (this.lastUpdates.TryGetValue(realmId, out local_0))
          return DateTime.UtcNow - local_0 > RiotUpdateDaemon.UpdateCooldownInterval;
        else
          return true;
      }
    }

    private void DoLocalUpdate(string realmId, string[] arguments)
    {
      if (!this.CanUpdate(realmId))
        return;
      lock (this.lastUpdates)
      {
        RiotUpdateDaemon.UpdaterState local_0;
        if (this.updates.TryGetValue(realmId, out local_0) && !local_0.Completed)
          return;
        this.updates[realmId] = RiotUpdateDaemon.UpdaterState.Create(realmId, arguments, new Action(this.OnChanged), new Action<RiotUpdateDaemon.UpdaterState>(this.OnCompleted));
        this.lastUpdates[realmId] = DateTime.UtcNow;
      }
    }

    private string GetGameInstallationDirectory(string realmId)
    {
      return string.Format("league#{0}", (object) realmId);
    }

    private void OnChanged()
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, this.Updates);
    }

    private void OnCompleted(RiotUpdateDaemon.UpdaterState completedState)
    {
      if (this.Completed == null)
        return;
      this.Completed((object) this, completedState);
    }
      public int state;
    private async Task RunLoop()
    {
      await Task.Delay(RiotUpdateDaemon.InitialUpdateDelay);
      TaskAwaiter awaiter1;
      TaskAwaiter awaiter2;
      while (true)
      {
        awaiter1 = this.TryUpdate().GetAwaiter();
        if (awaiter1.IsCompleted)
        {
          awaiter1.GetResult();
          awaiter1 = new TaskAwaiter();
          awaiter2 = Task.Delay(RiotUpdateDaemon.UpdateDelay).GetAwaiter();
          if (awaiter2.IsCompleted)
          {
            awaiter2.GetResult();
            awaiter2 = new TaskAwaiter();
          }
          else
            goto label_6;
        }
        else
          break;
      }
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      state = 1;
      TaskAwaiter taskAwaiter = awaiter1;
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RiotUpdateDaemon.\u003CRunLoop\u003Ed__1f>(ref awaiter1, this);
      return;
label_6:
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003C\u003E1__state = 2;
      taskAwaiter = awaiter2;
      // ISSUE: explicit reference operation
      // ISSUE: reference to a compiler-generated field
      (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RiotUpdateDaemon.\u003CRunLoop\u003Ed__1f>(ref awaiter2, this);
    }

    public class UpdaterState
    {
      public Process Process;
      public string RealmId;
      public string Status;
      public long Position;
      public long Length;
      public bool Completed;
      private readonly Action notifyChange;
      private readonly Action<RiotUpdateDaemon.UpdaterState> notifyCompleted;

      public UpdaterState()
      {
      }

      private UpdaterState(string realmId, Process process, Action notifyChange, Action<RiotUpdateDaemon.UpdaterState> notifyCompleted)
      {
        this.Process = process;
        this.RealmId = realmId;
        this.notifyChange = notifyChange;
        this.notifyCompleted = notifyCompleted;
        process.Exited += (EventHandler) ((sender, e) => this.SetCompleted());
        this.TrackProgressAsync().ContinueWith((Action<Task>) (x => this.SetCompleted()));
      }

      internal static bool TryCreate(string realmId, string[] arguments, Action notifyChange, Action<RiotUpdateDaemon.UpdaterState> notifyCompleted, out RiotUpdateDaemon.UpdaterState updaterState)
      {
        try
        {
          updaterState = RiotUpdateDaemon.UpdaterState.Create(realmId, arguments, notifyChange, notifyCompleted);
          return true;
        }
        catch
        {
          updaterState = (RiotUpdateDaemon.UpdaterState) null;
          return false;
        }
      }

      internal static RiotUpdateDaemon.UpdaterState Create(string realmId, string[] arguments, Action notifyChange, Action<RiotUpdateDaemon.UpdaterState> notifyCompleted)
      {
        Process process = Process.Start(new ProcessStartInfo()
        {
          FileName = Path.Combine(LaunchData.ApplicationDirectory, "riot-update"),
          Arguments = CommandLine.Escape(arguments),
          UseShellExecute = false,
          RedirectStandardError = true,
          CreateNoWindow = true,
          WindowStyle = ProcessWindowStyle.Hidden
        });
        return new RiotUpdateDaemon.UpdaterState(realmId, process, notifyChange, notifyCompleted);
      }

      private async Task TrackProgressAsync()
      {
        StreamReader stream = this.Process.StandardError;
        TaskAwaiter<string> awaiter;
        while (true)
        {
          awaiter = stream.ReadLineAsync().GetAwaiter();
          if (awaiter.IsCompleted)
          {
            string result = awaiter.GetResult();
            awaiter = new TaskAwaiter<string>();
            string line;
            if ((line = result) != null)
            {
              GroupCollection groups = RiotUpdateDaemon.RiotUpdaterProgressParser.Match(line).Groups;
              string s1 = groups["position"].Value;
              string s2 = groups["length"].Value;
              this.Status = groups["status"].Value;
              long.TryParse(s1, out this.Position);
              long.TryParse(s2, out this.Length);
              this.notifyChange();
            }
            else
              break;
          }
          else
            goto label_5;
        }
        this.SetCompleted();
label_5:
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = 0;
        TaskAwaiter<string> taskAwaiter = awaiter;
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, RiotUpdateDaemon.UpdaterState.\u003CTrackProgressAsync\u003Ed__27>(ref awaiter, this);
      }

      private void SetCompleted()
      {
        if (this.Completed)
          return;
        this.Completed = true;
        this.notifyCompleted(this);
        this.notifyChange();
      }
    }
  }
}
