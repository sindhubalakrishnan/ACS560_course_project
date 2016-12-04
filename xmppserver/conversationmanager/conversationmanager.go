package conversationmanager

import (
	"fmt"
	"os"
  "strings"
	"sync"
	"github.com/xweskingx/ACS560_course_project/xmppserver/dataaccess"
	"github.com/xweskingx/ACS560_course_project/xmppserver/logger"
	"github.com/xweskingx/ACS560_course_project/xmppserver/xmppmessage"
)

type ConversationManager struct {
	da   *dataaccess.DataAccess
	lock *sync.Mutex
	log  logger.Logger
}

var manager *ConversationManager
var once sync.Once

func (man ConversationManager) CreateSingleUserChat(jid1 string, jid2 string, subject string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug(fmt.Sprintf("Creating chat for users %s and %s", jid1, jid2))
	_, success := manager.da.CreateConversation(subject, "SUC", getBareJid(jid1), getBareJid(jid2))
	if success {
		manager.log.Debug(fmt.Sprintf("Successfully created conversation for users %s and %s", jid1, jid2))
	} else {
		manager.log.Debug(fmt.Sprintf("Failed to created conversation for users %s and %s", jid1, jid2))
	}
}

func Min(x, y int) int {
    if x < y {
        return x
    }
    return y
}

func (man ConversationManager) AddMessageToSUC(body string, from string, to string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug(fmt.Sprintf("Adding message to chat", body))
	c, csuccess := manager.da.LookupSUCByUsers(getBareJid(from), getBareJid(to))
	if !csuccess {
		manager.log.Debug(fmt.Sprintf("Conversation not found for users %s and %s, create conversation", getBareJid(from), getBareJid(to)))
		c, csuccess = manager.da.CreateConversation(body[:Min(len(body), 24)], "SUC", getBareJid(from), getBareJid(to))
	}
  u, usuccess := manager.da.LookupUserByLogin(getBareJid(from))
	if csuccess && usuccess {
		m := xmppmessage.Init("", body, u, c, -1)
    _, success := manager.da.AddMessageToConversation(m, c)
		if success {
			manager.log.Debug(fmt.Sprintf("Successfully added message to conversation for users %s and %s", from, to))
		} else {
			manager.log.Debug(fmt.Sprintf("Failed to add message to conversation for users %s and %s", from, to))
		}
	} else {
		manager.log.Debug(fmt.Sprintf("Failed to add message to conversation"))
	}

}

func GetConversationManager() *ConversationManager {
	once.Do(func() {
		newlog := logger.Logger{LogLevel: 0, TAG: "ConversationManager"}
		manager = &ConversationManager{log: newlog, lock: &sync.Mutex{}}
		manager.log.Init(os.Stdout)
		manager.da = dataaccess.GetDataAccess()
	})
	return manager
}

func (man ConversationManager) checkErr(msg string, err error) {
	if err != nil {
		manager.log.Error(fmt.Sprintf("%s %s", msg, err.Error()))
		os.Exit(1)
	}
}

func getBareJid(jid string) string {
  return strings.Split(jid, "@")[0]
}
