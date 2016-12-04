package xmpp

import (
  "fmt"
  "strconv"
  "strings"
)

// Extension interface for processing normal messages
type Extension interface {
	Process(message interface{}, from *Client)
}

// DebugExtension just dumps data
type DebugExtension struct {
	Log Logging
}

// Process a message (write to debug logger)
func (e *DebugExtension) Process(message interface{}, from *Client) {
	e.Log.Debug(fmt.Sprintf("Processing message: %s", message))
}

// NormalMessageExtension handles client messages
type NormalMessageExtension struct {
	MessageBus chan<- Message
  Log        Logging
}

// Process sends `ClientMessage`s from a client down the `MessageBus`
func (e *NormalMessageExtension) Process(message interface{}, from *Client) {
	parsed, ok := message.(*ClientMessage)
	if ok {
		e.MessageBus <- Message{To: parsed.To, Data: message}
	}
}

// RosterExtension handles ClientIQ presence requests and updates
type RosterExtension struct {
	Accounts AccountManager
  Log      Logging
}

// Process responds to Presence requests from a client
func (e *RosterExtension) Process(message interface{}, from *Client) {
	parsed, ok := message.(*ClientIQ)

	// handle things we need to handle
	if ok && string(parsed.Query) == "<query xmlns='jabber:iq:roster'/>" {
		// respond with roster
		roster, _ := e.Accounts.GetAllUsers(from.jid)
		msg := "<iq id='" + parsed.ID + "' to='" + parsed.From + "' type='result'><query xmlns='jabber:iq:roster' ver='ver" + strconv.Itoa(e.Accounts.GetRosterVersion()) + "'>"
		for _, v := range roster {
			msg = msg + "<item jid='" + v + "'/>"
		}
		msg = msg + "</query></iq>"
    /*online, _ := e.Accounts.OnlineRoster(from.jid)
		for _, v := range online {
      msg = msg + "<presence from='" + v + "' to='" + from.jid +"' xml:lang='en'><show>" + e.Accounts.GetUserPresence(v).GetShow() + "</show><status>" + e.Accounts.GetUserPresence(v).GetStatus() + "</status><nick xmplns='http://jabber.org/protocol/nick'></nick></presence>"
		}*/
    e.Log.Debug(fmt.Sprintf("Writing Roster response and presence", msg))
		// respond to client
		from.messages <- msg
	}
}

type PresenceExtension struct {
	Accounts          AccountManager
  CustomMessageBus  chan<- CustomMessage
  Log               Logging
}

func (e *PresenceExtension) Process(message interface{}, from *Client) {
	parsed, ok := message.(*ClientPresence)

	if ok {
    var pres_type string
		var show string
    var status string
    var type_msg string
    if parsed.Type == "" {
      pres_type = ""
    } else {
      pres_type = parsed.Type
      type_msg = "type='" + pres_type + "'"
    }

		if parsed.Show == "" {
			show = "chat"
		} else {
		  show = parsed.Show
		}
    
    if parsed.Status == "" {
      status = "Available"
    } else {
      status = parsed.Status
    }
    var msg string
    var nick string
	
    if parsed.To != "" && pres_type == "subscribe" {
      online,_ := e.Accounts.OnlineRoster(from.jid)
      for _,u := range online {
        if u  == parsed.To {
          e.Accounts.Subscribe(from.jid, parsed.To)
          nick,_ = e.Accounts.GetUserNick(from.jid)
          msg = "<presence from='" + u + "' to='" + from.jid +"' xml:lang='en'><show>" + e.Accounts.GetUserPresence(u).GetShow() + "</show><status>" + e.Accounts.GetUserPresence(u).GetStatus() + "</status><nick xmlns='http://jabber.org/protocol/nick'>" + nick + "</nick></presence>"
          e.Log.Debug(fmt.Sprintf("Writing presence message %s", msg))
          e.CustomMessageBus <- CustomMessage{To: from.jid, Data: msg}
          break
        }
      }
    } else {

		  e.Accounts.SetUserPresence(from.jid, show, status)

      recipients := e.Accounts.GetSubscriptionsToUser(from.jid)
   	  for _, v := range recipients {
        nick,_ = e.Accounts.GetUserNick(from.jid)
        msg = "<presence from='" + getBareJid(from.jid) + "' to='" + v +"' " + type_msg + " xml:lang='en'><show>" + e.Accounts.GetUserPresence(from.jid).GetShow() + "</show><status>" + e.Accounts.GetUserPresence(from.jid).GetStatus() + "</status><nick xmlns='http://jabber.org/protocol/nick'>" + nick + "</nick></presence>"
        e.Log.Debug(fmt.Sprintf("Writing presence message %s", msg))
        e.CustomMessageBus <- CustomMessage{To: v, Data: msg}
		  }
    }
	}
}

type SessionExtension struct {
  Accounts AccountManager
  Log Logging
}

func (e *SessionExtension) Process(message interface{}, from *Client) {
  parsed, ok := message.(*ClientIQ)
  var msg = ""

  if ok && string(parsed.Query) == "<session xmlns='urn:ietf:params:xml:ns:xmpp-session'/>" {
    msg = "<iq id='" + parsed.ID + "' from='" + parsed.To + "' type='result'/>"
    e.Log.Debug(fmt.Sprintf("Established Session for %s", from.jid))
    from.messages <- msg
  }
}

func getBareJid(jid string) string {
  return strings.Split(jid, "@")[0]
}


