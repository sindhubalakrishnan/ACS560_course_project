package xmpp

import "fmt"

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
}

// Process responds to Presence requests from a client
func (e *RosterExtension) Process(message interface{}, from *Client) {
	parsed, ok := message.(*ClientIQ)

	// handle things we need to handle
	if ok && string(parsed.Query) == "<query xmlns='jabber:iq:roster'/>" {
		// respond with roster
		roster, _ := e.Accounts.OnlineRoster(from.jid)
		msg := "<iq id='" + parsed.ID + "' to='" + parsed.From + "' type='result'><query xmlns='jabber:iq:roster' ver='ver7'>"
		for _, v := range roster {
			msg = msg + "<item jid='" + v + "'/>"
		}
		msg = msg + "</query></iq>"
		for _, v := range roster {
			msg += "<presence from='" + v + "'><show>" + e.Accounts.GetUserPresence(v) + "</show><status></status><priority></priority></presence>"
		}

		// respond to client
		from.messages <- msg
	}
}

type PresenceExtension struct {
	Accounts   AccountManager
	MessageBus chan<- Message
}

func (e *PresenceExtension) Process(message interface{}, from *Client) {
	parsed, ok := message.(*ClientPresence)

	roster, _ := e.Accounts.OnlineRoster(from.jid)
	if ok {
		var presence string
		if parsed.Show == "" {
			presence = "chat"
		} else {
			presence = parsed.Show
		}

		e.Accounts.SetUserPresence(from.jid, presence)
		for _, v := range roster {
			message.(*ClientPresence).From = from.jid
			e.MessageBus <- Message{To: v, Data: message}
		}
	}
}

type SessionExtension struct {
  Accounts AccountManager
  MessageBus chan<- Message
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
