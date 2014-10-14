// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.GameMaestroService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using MicroApi;
using RiotGames.Platform.Game;
using RtmpSharp.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WintermintClient.Data;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiSingleton]
  public class GameMaestroService : JsApiService
  {
    public GameMaestroService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult += new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        account.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnFlexMessageReceived);
        account.InvocationResult -= new EventHandler<InvocationResultEventArgs>(this.OnInvocationResult);
      });
    }

    private void OnInvocationResult(object sender, InvocationResultEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Result);
    }

    private void OnFlexMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      this.OnData(sender as RiotAccount, e.Body);
    }

    private void OnData(RiotAccount account, object message)
    {
      this.OnDataInternal(account, message);
    }

    private async Task OnDataInternal(RiotAccount account, object message)
    {
      PlayerCredentialsDto gameConnectionCredentials = message as PlayerCredentialsDto;
      if (gameConnectionCredentials != null)
      {
        JsApiService.PushIfActive(account, "game:launch", (object) null);
        bool result = await GameMaestroService.TryStartGame(account.RealmId, gameConnectionCredentials);
        if (!result)
          JsApiService.Push("game:launch:fail", (object) null);
      }
    }

    public static async Task<bool> TryStartGame(string realmId, PlayerCredentialsDto game)
    {
      bool flag;
      try
      {
        await GameMaestroService.StartGame(realmId, game);
        flag = true;
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public static Task StartGame(string realmId, PlayerCredentialsDto game)
    {
      if (game == null)
        return (Task) Task.FromResult<bool>(true);
      JsApiService.Push("game:reconnect", (object) null);
      return GameMaestroService.RunLeagueOfLegends(realmId, string.Format("\"56471\" \"wintermint-delegator\" \"wintermint-delegator\" \"{0} {1} {2} {3}\"", (object) game.ServerIp, (object) game.ServerPort, (object) game.EncryptionKey, (object) game.SummonerId));
    }

    public static async Task<bool> TryStartSpectatorGame(string realmId, string platformId, PlayerCredentialsDto game)
    {
      bool flag;
      try
      {
        await GameMaestroService.StartSpectatorGame(realmId, platformId, game);
        flag = true;
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public static Task StartSpectatorGame(string realmId, string platformId, PlayerCredentialsDto game)
    {
      if (game == null)
        return (Task) Task.FromResult<bool>(true);
      JsApiService.Push("game:reconnect", (object) null);
      return GameMaestroService.RunLeagueOfLegends(realmId, string.Format("\"56471\" \"wintermint-delegator\" \"wintermint-delegator\" \"spectator {0}:{1} {2} {3} {4}\"", (object) game.ObserverServerIp, (object) game.ObserverServerPort, (object) game.ObserverEncryptionKey, (object) game.GameId, (object) platformId));
    }

    private static async Task RunLeagueOfLegends(string realmId, string arguments)
    {
      await GameMaestroService.CopyRadsDependenciesAsync(realmId);
      string solutionDirectory = GameMaestroService.GetLatestDeploy(realmId, "solutions", "lol_game_client_sln");
      string projectDirectoryDirectory = GameMaestroService.GetLatestDeploy(realmId, "projects", "lol_game_client");
      Process process = new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          FileName = string.Format("{0}/League of Legends.exe", (object) projectDirectoryDirectory),
          Arguments = arguments,
          WorkingDirectory = solutionDirectory,
          UseShellExecute = false
        }
      };
      process.Start();
    }

    private static string GetRadsDirectory(string realmId)
    {
      return Path.Combine(Path.Combine(LaunchData.RiotContainerDirectory, "league#" + realmId), "RADS");
    }

    private static string GetLatestDeploy(string realmId, string category, string name)
    {
      return Path.Combine(GameMaestroService.GetLatest(Path.Combine(GameMaestroService.GetRadsDirectory(realmId), category, name, "releases")), "deploy");
    }

    private static string GetLatest(string directory)
    {
      return Enumerable.First<string>(Enumerable.Select(Enumerable.OrderByDescending(Enumerable.Where(Enumerable.Select((IEnumerable<string>) Directory.GetDirectories(directory), dir => new
      {
        dir = dir,
        name = new DirectoryInfo(dir).Name
      }), param0 => ReleasePackage.IsVersionString(param0.name)), param0 => new ReleasePackage(param0.name).Version), param0 => param0.dir));
    }

    private static Task CopyRadsDependenciesAsync(string realmId)
    {
      string radsDirectory = GameMaestroService.GetRadsDirectory(realmId);
      string source = Path.Combine(GameMaestroService.GetLatestDeploy(realmId, "projects", "lol_launcher"), "riotradsio.dll");
      string destination = Path.Combine(radsDirectory, "riotradsio.dll");
      TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
      ThreadPool.QueueUserWorkItem((WaitCallback) (state =>
      {
        try
        {
          File.Copy(source, destination, true);
        }
        catch
        {
        }
        finally
        {
          tcs.SetResult((object) null);
        }
      }));
      return (Task) tcs.Task;
    }
  }
}
