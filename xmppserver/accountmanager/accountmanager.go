package accountmanager

import (
	"fmt"
	"os"
	"sync"
  "strings"
	"github.com/xweskingx/ACS560_course_project/xmppserver/dataaccess"
	"github.com/xweskingx/ACS560_course_project/xmppserver/logger"
  "github.com/xweskingx/ACS560_course_project/xmppserver/user"
)

type AccountManager struct {
	Roster            map[string]user.Presence
	Online            map[string]bool
  SubscriptionsTo   map[string][]string
  SubscriptionsFrom map[string][]string
	RosterVersion     int
	da                *dataaccess.DataAccess
	lock              *sync.Mutex
	log               logger.Logger
}

var manager *AccountManager
var once sync.Once

func getBareJid(jid string) string {
  return strings.Split(jid, "@")[0]
}

func (man AccountManager) RegisterUser(jid string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug("Marking user with jid " + jid + " online.")
	manager.Online[getBareJid(jid)] = true
  manager.Roster[getBareJid(jid)] = user.InitPresence("chat", "Available")
	manager.RosterVersion = manager.RosterVersion + 1
}

func (man AccountManager) UnRegisterUser(jid string) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	manager.log.Debug("Marking user with jid " + jid + " offline.")
	//delete(manager.Online, jid)
  manager.Online[getBareJid(jid)] = false
  delete(manager.SubscriptionsFrom, jid)
  delete(manager.SubscriptionsTo, jid)
  manager.Roster[getBareJid(jid)] = user.InitPresence("", "Unavailable")
	manager.RosterVersion = manager.RosterVersion + 1
}

func (man AccountManager) Subscribe(from string, to string) {
  manager.lock.Lock()
  defer manager.lock.Unlock()
  manager.SubscriptionsFrom[getBareJid(from)] = append(manager.SubscriptionsFrom[getBareJid(from)], getBareJid(to))
  manager.SubscriptionsTo[getBareJid(to)] = append(manager.SubscriptionsTo[getBareJid(to)], getBareJid(from))
  for _,user := range manager.SubscriptionsFrom[getBareJid(from)] {
    manager.log.Debug("SubscriptionsFrom["+getBareJid(from)+"] = " + user)
  }
  for _,user := range manager.SubscriptionsTo[getBareJid(from)] {
    manager.log.Debug("SubscriptionsTo["+getBareJid(from)+"] = " + user)
  }
}

func (man AccountManager) Unsubscribe(from string, to string) {
  index := indexOf(manager.SubscriptionsFrom[getBareJid(from)], getBareJid(to))
  if index != -1 {
    deleteIndex(manager.SubscriptionsFrom[getBareJid(from)], index)
  }
  index = indexOf(manager.SubscriptionsTo[getBareJid(to)], getBareJid(from))
  if index != -1 {
    deleteIndex(manager.SubscriptionsTo[getBareJid(to)], index)
  }
}

func (man AccountManager) GetUserSubscriptions(jid string) ([]string) {
  return manager.SubscriptionsFrom[getBareJid(jid)]
}

func (man AccountManager) GetSubscriptionsToUser(jid string) ([]string) {
  return manager.SubscriptionsTo[getBareJid(jid)]
}

func deleteIndex(arr []string, index int) {
    arr = append(arr[:index], arr[index+1])
}

func indexOf(arr []string, elem string) int {
  for index,v := range arr {
    if elem == v {
      return index
    }
  }
  return -1
}

func (man AccountManager) GetRosterVersion() int {
  return manager.RosterVersion
}

func (man AccountManager) SetUserPresence(jid string, show string, status string) (success bool, err error) {
	manager.log.Debug("Setting user presence for " + jid + " to " + show + "[" + status + "]")
  manager.Roster[getBareJid(jid)] = user.InitPresence(show, status)
	success = true
	return
}

func (man AccountManager) GetUserPresence(jid string) user.Presence {
	return manager.Roster[getBareJid(jid)]
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
	manager.log.Error("Something went wrong when trying to authenticate the user with login " + username)
	success = false
	return
}

func (man AccountManager) CreateAccount(username, password, fname, lname string) (success bool, err error) {
	manager.lock.Lock()
	defer manager.lock.Unlock()
	_, success = manager.da.CreateUser(fname, lname, username, password)
	return
}

func (man AccountManager) OnlineRoster(jid string) (online []string, err error) {
	manager.lock.Lock()
	defer manager.lock.Unlock()

	manager.log.Debug(fmt.Sprintf("Retrieving online roster for: %s", jid))

	for person, isOnline := range manager.Online {
    if isOnline {
		  online = append(online, person)
    }
	}
	return
}

func (man AccountManager) GetUserNick(jid string) (nick string, success bool) {
  u, success := manager.da.LookupUserByLogin(getBareJid(jid))
	if success {
		if u != nil {
      nick = u.GetNick()
	    return nick, success
		}
		manager.log.Debug("Failed to find nick for " + jid)
	}
	manager.log.Error("Something went wrong when trying to find a nick for user with login " + jid)
	success = false
	return nick, success
}

func (man AccountManager) GetAllUsers(jid string) (users []string, err error) {
	manager.lock.Lock()
	defer manager.lock.Unlock()

	manager.log.Debug(fmt.Sprintf("Retrieving roster for: %s", jid))

	for person, _ := range manager.Roster {
		users = append(users, person)
	}
	return
}

func (man AccountManager) InitRoster() {
  manager.lock.Lock()
  defer manager.lock.Unlock()

  manager.log.Debug("Initializing Roster")

  users, success := man.da.GetAllUsers()
  if success {
    for _, u := range users {
      manager.Roster[u.GetLogin()] = u.GetPresence()
    }
  }
}

func GetAccountManager() *AccountManager {
	once.Do(func() {
		newlog := logger.Logger{LogLevel: 0, TAG: "AccountManager"}
    manager = &AccountManager{log: newlog, lock: &sync.Mutex{}, Roster: make(map[string]user.Presence), Online: make(map[string]bool), SubscriptionsFrom: make(map[string][]string), SubscriptionsTo: make(map[string][]string), RosterVersion: 0}
		manager.log.Init(os.Stdout)
		manager.da = dataaccess.GetDataAccess()
    manager.InitRoster()
	})
	return manager
}

func (man AccountManager) checkErr(msg string, err error) {
	if err != nil {
		manager.log.Error(fmt.Sprintf("%s %s", msg, err.Error()))
		os.Exit(1)
	}
}
