// Decompiled with JetBrains decompiler
// Type: WintermintClient.JsApi.Notification.ContactsNotificationService
// Assembly: wintermint-client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=184fa7e06d73a8fc
// MVID: F314ACCC-EE41-45E7-909B-0326F415F7A5
// Assembly location: C:\Users\Betty\Desktop\Coding\Wintermint\Program\Dlls\wintermint-client-cleaned.dll

using Chat;
using MicroApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WintermintClient.JsApi;
using WintermintClient.Riot;

namespace WintermintClient.JsApi.Notification
{
  [MicroApiService("contacts", Preload = true)]
  public class ContactsNotificationService : JsApiService
  {
    private static readonly Presence OfflinePresence = new Presence()
    {
      PresenceType = PresenceType.Offline
    };
    private static readonly ConcurrentDictionary<string, ContactsNotificationService.JsFederatedDude> Contacts = new ConcurrentDictionary<string, ContactsNotificationService.JsFederatedDude>();

    public ContactsNotificationService()
    {
      JsApiService.AccountBag.AccountAdded += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        UberChatClient chat = account.Chat;
        chat.ContactChanged += new EventHandler<ContactChangedEventArgs>(this.OnContactChanged);
        chat.RosterReceived += new EventHandler<RosterReceivedEventArgs>(this.ChatOnRosterReceived);
        chat.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived);
        chat.MailReceived += new EventHandler<MessageReceivedEventArgs>(this.OnMailReceived);
        chat.ErrorReceived += new EventHandler<ErrorReceivedEventArgs>(this.OnErrorReceived);
      });
      JsApiService.AccountBag.AccountRemoved += (EventHandler<RiotAccount>) ((sender, account) =>
      {
        UberChatClient chat = account.Chat;
        chat.ContactChanged -= new EventHandler<ContactChangedEventArgs>(this.OnContactChanged);
        chat.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived);
        chat.MailReceived -= new EventHandler<MessageReceivedEventArgs>(this.OnMailReceived);
        chat.ErrorReceived -= new EventHandler<ErrorReceivedEventArgs>(this.OnErrorReceived);
        foreach (Contact contact in chat.Roster)
          this.OnContactChanged((object) chat, new ContactChangedEventArgs(contact, ContactChangeType.Remove));
      });
    }

    private async void ChatOnRosterReceived(object sender, RosterReceivedEventArgs e)
    {
      try
      {
        UberChatClient client = (UberChatClient) sender;
        RiotAccount account = client.Account;
        Contact[] contacts = e.Contacts;
        long[] summonerIds = Enumerable.ToArray<long>(Enumerable.Select<Contact, long>((IEnumerable<Contact>) contacts, (Func<Contact, long>) (c => JsApiService.GetSummonerIdFromJid(c.Jid))));
        string[] summonerNames = await account.InvokeAsync<string[]>("summonerService", "getSummonerNames", new object[1]
        {
          (object) summonerIds
        });
        for (int index = 0; index < contacts.Length; ++index)
        {
          Contact contact = contacts[index];
          string str = summonerNames[index];
          if (!(contact.Name == str))
          {
            contact.Name = str;
            this.OnContactUpdated(client, ContactsNotificationService.JsContact.Create(client, contact));
            client.Contacts.Update(contact.BareJid, contact.Name, contact.Groups);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void OnContactChanged(object sender, ContactChangedEventArgs e)
    {
      UberChatClient client = (UberChatClient) sender;
      ContactsNotificationService.JsContact contact = ContactsNotificationService.JsContact.Create(client, e.Contact);
      ContactsNotificationService.ApplyContactTransformations(contact);
      switch (e.ChangeType)
      {
        case ContactChangeType.Add:
          break;
        case ContactChangeType.Update:
          this.OnContactUpdated(client, contact);
          break;
        case ContactChangeType.Remove:
          this.OnContactRemoved(client, contact);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnContactUpdated(UberChatClient client, ContactsNotificationService.JsContact contact)
    {
      ContactsNotificationService.JsFederatedDude orAdd;
      lock (ContactsNotificationService.Contacts)
      {
        orAdd = ContactsNotificationService.Contacts.GetOrAdd(contact.InternalId, (Func<string, ContactsNotificationService.JsFederatedDude>) (_ => new ContactsNotificationService.JsFederatedDude(contact)));
        orAdd.Contact = contact;
        orAdd.Attach(client.AccountHandle);
      }
      ContactsNotificationService.PushContact("chat:contact:upsert", orAdd);
    }

    private void OnContactRemoved(UberChatClient client, ContactsNotificationService.JsContact contact)
    {
      ContactsNotificationService.JsFederatedDude dude;
      lock (ContactsNotificationService.Contacts)
      {
        if (ContactsNotificationService.Contacts.TryGetValue(contact.InternalId, out dude))
        {
          if (dude.Detach(client.AccountHandle) == 0)
            ContactsNotificationService.Contacts.TryRemove(contact.InternalId, out dude);
        }
      }
      if (dude == null)
        return;
      ContactsNotificationService.PushContact(dude.AccountHandles.Count > 0 ? "chat:contact:upsert" : "chat:contact:delete", dude);
    }

    [MicroApiMethod("refresh")]
    public void RefreshContacts()
    {
      JsApiService.Push("chat:contact:wipe", (object) null);
      foreach (ContactsNotificationService.JsFederatedDude dude in (IEnumerable<ContactsNotificationService.JsFederatedDude>) ContactsNotificationService.Contacts.Values)
        ContactsNotificationService.PushContact("chat:contact:upsert", dude);
    }

    private void OnErrorReceived(object sender, ErrorReceivedEventArgs e)
    {
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      this.NotifyXmppMessage("chat:message", sender, e);
    }

    private void OnMailReceived(object sender, MessageReceivedEventArgs e)
    {
      this.NotifyXmppMessage("chat:mail", sender, e);
    }

    private void NotifyXmppMessage(string publishKey, object sender, MessageReceivedEventArgs args)
    {
      if (args.MessageId != null && args.MessageId.StartsWith("hm_", StringComparison.OrdinalIgnoreCase) || args.Sender.ConferenceUser && string.IsNullOrEmpty(args.Sender.Name))
        return;
      UberChatClient client = sender as UberChatClient;
      ContactsNotificationService.JsContact jsContact = ContactsNotificationService.JsContact.Create(client, args.Sender);
      jsContact.Accounts = new List<int>((IEnumerable<int>) new int[1]
      {
        client.AccountHandle
      });
      var fAnonymousType11 = new
      {
        Sender = jsContact,
        Subject = args.Subject,
        Message = args.Message,
        Timestamp = args.Timestamp,
        RealmId = client.RealmId,
        Handle = client.AccountHandle
      };
      JsApiService.Push(publishKey, (object) fAnonymousType11);
    }

    private static void PushContact(string key, ContactsNotificationService.JsFederatedDude dude)
    {
      ContactsNotificationService.JsContact jsContact = dude.Contact;
      if (jsContact != null)
        jsContact.Accounts = dude.AccountHandles;
      JsApiService.Push(key, (object) jsContact);
    }

    private static int GetPresenceOrder(Presence presence)
    {
      switch (presence.PresenceType)
      {
        case PresenceType.Offline:
          return 3;
        case PresenceType.Online:
          return 0;
        case PresenceType.Busy:
          return 1;
        case PresenceType.Away:
          return 2;
        default:
          return 10;
      }
    }

    private static void ApplyContactTransformations(ContactsNotificationService.JsContact contact)
    {
      if (contact.Presences != null)
      {
        foreach (Presence presence in Enumerable.Where<Presence>((IEnumerable<Presence>) contact.Presences, (Func<Presence, bool>) (presence => presence.Resource == "xiff")))
          presence.Resource = "adobe air";
      }
      string[] strArray = contact.Groups;
      if (strArray == null)
        return;
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] == "**Default")
          strArray[index] = "General";
      }
      contact.Groups = Enumerable.ToArray<string>(Enumerable.Distinct<string>((IEnumerable<string>) strArray, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
    }

    public class JsFederatedDude
    {
      public ContactsNotificationService.JsContact Contact;
      public List<int> AccountHandles;

      public JsFederatedDude(ContactsNotificationService.JsContact contact)
      {
        this.Contact = contact;
        this.AccountHandles = new List<int>(1);
      }

      public void Attach(int accountHandle)
      {
        if (this.AccountHandles.Contains(accountHandle))
          return;
        this.AccountHandles.Add(accountHandle);
      }

      public int Detach(int accountHandle)
      {
        this.AccountHandles.Remove(accountHandle);
        return this.AccountHandles.Count;
      }
    }

    [Serializable]
    public class JsContact
    {
      public string InternalId;
      public string RealmId;
      public Presence Presence;
      public List<int> Accounts;
      public string Id;
      public string Jid;
      public string Name;
      public string[] Groups;
      public Presence[] Presences;
      public bool ConferenceUser;
      public string BareJid;

      public static ContactsNotificationService.JsContact Create(UberChatClient client, Contact contact)
      {
        return new ContactsNotificationService.JsContact()
        {
          InternalId = string.Format("{0}//{1}", (object) client.RealmId, (object) contact.Id),
          RealmId = client.RealmId,
          Presence = Enumerable.FirstOrDefault<Presence>((IEnumerable<Presence>) Enumerable.OrderBy<Presence, int>((IEnumerable<Presence>) contact.Presences, new Func<Presence, int>(ContactsNotificationService.GetPresenceOrder))) ?? ContactsNotificationService.OfflinePresence,
          Id = contact.Id,
          Jid = contact.Jid,
          Name = contact.Name,
          Groups = contact.Groups,
          Presences = contact.Presences,
          ConferenceUser = contact.ConferenceUser,
          BareJid = contact.BareJid
        };
      }
    }

    [Flags]
    public enum ContactUpdateType
    {
      Upsert = 0,
      Delete = 1,
    }
  }
}
