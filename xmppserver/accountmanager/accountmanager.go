package accountmanager

import (
	"fmt"
	"os"
	"sync"
	"github.com/xweskingx/ACS560_course_project/xmppserver/dataaccess"
	"github.com/xweskingx/ACS560_course_project/xmppserver/logger"
)

type AccountManager struct {
	UserPresence  map[string]string
	Online        map[string]bool
	RosterVersion int
	da            *dataaccess.DataAccess
	lock          *sync.Mutex
	log           logger.Logger
}

var manager *AccountManager
var once sync.Once

func (man AccountManager) RegisterUser(jid string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug("Marking user with jid " + jid + " online.")
	manager.Online[jid] = true
	manager.UserPresence[jid] = "chat"
	manager.RosterVersion = manager.RosterVersion + 1
}

func (man AccountManager) UnRegisterUser(jid string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug("Marking user with jid " + jid + " offline.")
	delete(manager.Online, jid)
	delete(manager.UserPresence, jid)
	manager.RosterVersion = manager.RosterVersion + 1
}

func (man AccountManager) SetUserPresence(jid, presence string) (success bool, err error) {
	manager.log.Debug("Setting user presence for " + jid + " to " + presence)
	manager.UserPresence[jid] = presence
	success = true
	return
}

func (man AccountManager) GetUserPresence(jid string) string {
	return manager.UserPresence[jid]
}

func (man AccountManager) Authenticate(username, password string) (success bool, err error) {
	manager.lock.Lock()
	manager.lock.Unlock()
	u, success := manager.da.LookupUserByLogin(username)
	if success {
		if u != nil {
			if u.GetPass() == password {
				success = true
				return
			}
		}
		manager.log.Debug("Failed to authenticate user: " + username + " with provided username/password pair")
	}
	manager.log.Error("Something went wrong when trying to authenticate the user with login " + password)
	success = false
	return
}

func (man AccountManager) CreateAccount(username, password string) (success bool, err error) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	_, success = manager.da.CreateUser("", "", username, password)
	return
}

func (man AccountManager) OnlineRoster(jid string) (online []string, err error) {
	manager.lock.Lock()
	defer manager.lock.Unlock()

	manager.log.Debug(fmt.Sprintf("Retrieving roster for: %s", jid))

	for person, _ := range manager.Online {
		online = append(online, person)
	}
	return
}

func GetAccountManager() *AccountManager {
	once.Do(func() {
		newlog := logger.Logger{LogLevel: 0, TAG: "AccountManager"}
		manager = &AccountManager{log: newlog, lock: &sync.Mutex{}, UserPresence: make(map[string]string), Online: make(map[string]bool), RosterVersion: 0}
		manager.log.Init(os.Stdout)
		manager.da = dataaccess.GetDataAccess()
	})
	return manager
}

func (man AccountManager) checkErr(msg string, err error) {
	if err != nil {
		manager.log.Error(fmt.Sprintf("%s %s", msg, err.Error()))
		os.Exit(1)
	}
}
