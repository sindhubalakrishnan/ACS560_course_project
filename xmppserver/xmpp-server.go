package main

import (
	"github.com/xweskingx/ACS560_course_project/xmppserver/accountmanager"
	"github.com/xweskingx/ACS560_course_project/xmppserver/connectionmanager"

	"crypto/tls"
	"flag"
	"fmt"
	"net"
	"os"
	"github.com/xweskingx/ACS560_course_project/xmppserver/xmpp"
)

/* Inject logging into xmpp library */

type Logger struct {
	info  bool
	debug bool
}

func (l Logger) Info(msg string) (err error) {
	if l.info {
		_, err = fmt.Printf("INFO: %s\n", msg)
	}
	return err
}

func (l Logger) Debug(msg string) (err error) {
	if l.debug {
		_, err = fmt.Printf("DEBUG: %s\n", msg)
	}
	return err
}

func (l Logger) Error(msg string) (err error) {
	_, err = fmt.Printf("ERROR: %s\n", msg)
	return err
}

/* Main server loop */

func main() {
	portPtr := flag.Int("port", 5222, "port number to listen on")
	debugPtr := flag.Bool("debug", true, "turn on debug logging")
	flag.Parse()

	//var registered = make(map[string]string)
	//registered["tmurphy"] = "password"

	//var activeUsers = make(map[string]chan<- interface{})

	var l = Logger{info: true, debug: *debugPtr}

	var messagebus = make(chan xmpp.Message)
	var connectbus = make(chan xmpp.Connect)
	var disconnectbus = make(chan xmpp.Disconnect)

	//var am = AccountManager{Users: registered, Online: activeUsers, log: l, lock: &sync.Mutex{}}
	var am = accountmanager.GetAccountManager()
	am.CreateAccount("atreidesp", "hulud1")
	am.CreateAccount("kingw", "1234")
  am.CreateAccount("sindhu", "1234")

	var cert, _ = tls.LoadX509KeyPair("./cert.pem", "./key.pem")
	var tlsConfig = tls.Config{
		MinVersion:   tls.VersionTLS10,
		Certificates: []tls.Certificate{cert},
		CipherSuites: []uint16{tls.TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256,
			tls.TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256,
			tls.TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA,
			tls.TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA,
			tls.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA,
			tls.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA},
	}

	xmppServer := &xmpp.Server{
		Log:        l,
		Accounts:   am,
		ConnectBus: connectbus,
		Extensions: []xmpp.Extension{
			&xmpp.DebugExtension{Log: l},
			&xmpp.NormalMessageExtension{MessageBus: messagebus},
			&xmpp.RosterExtension{Accounts: am},
			&xmpp.PresenceExtension{Accounts: am, MessageBus: messagebus},
      &xmpp.SessionExtension{Accounts: am, MessageBus: messagebus, Log: l},
		},
		DisconnectBus: disconnectbus,
		Domain:        "example.com",
		TLSConfig:     &tlsConfig,
	}

	l.Info("Starting server")
	l.Info("Listening on localhost:" + fmt.Sprintf("%d", *portPtr))

	// Listen for incoming connections.

	listener, err := net.Listen("tcp", fmt.Sprintf(":%d", *portPtr))
	if err != nil {
		l.Error(fmt.Sprintf("Could not listen for connections: %s", err.Error()))
		os.Exit(1)
	}
	defer listener.Close()

	cm := connectionmanager.GetConnectionManager()
	go cm.RouteRoutine(messagebus)
	go cm.ConnectRoutine(connectbus)
	go cm.DisconnectRoutine(disconnectbus)

	// Handle each connection.
	for {
		conn, err := listener.Accept()

		if err != nil {
			l.Error(fmt.Sprintf("Could not accept connection: %s", err.Error()))
			os.Exit(1)
		}

		go xmppServer.TCPAnswer(conn)
	}
}
