package dataaccess

import (
	"database/sql"
	"fmt"
	"github.com/xweskingx/ACS560_course_project/xmppserver/conversation"
	"github.com/xweskingx/ACS560_course_project/xmppserver/logger"
	"github.com/xweskingx/ACS560_course_project/xmppserver/user"
	"github.com/xweskingx/ACS560_course_project/xmppserver/xmppmessage"
	"os"
	"strconv"
	"sync"

	_ "github.com/mattn/go-sqlite3"
)

var dataaccess *DataAccess
var once sync.Once

type DataAccess struct {
	dbname string
	log    logger.Logger
}

func (dataaccess DataAccess) InitDB() bool {
	dataaccess.log.Debug("Initializing Database")
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
    // Create users table
    stmt, err := db.Prepare("CREATE TABLE IF NOT EXISTS 'user' ( 'id' INTEGER   PRIMARY KEY AUTOINCREMENT, 'login' VARCHAR(64) UNIQUE, 'password' VARCHAR(64),  'first_name' VARCHAR(64) NULL, 'last_name' VARCHAR(64) NULL, 'created_at'       DATETIME DEFAULT CURRENT_TIMESTAMP );")
    if !dataaccess.checkErr("Error preparing statement for users", err) {
      _, err := stmt.Exec()
      if !dataaccess.checkErr("Error executing statment", err) {
        dataaccess.log.Debug("Initialized users table")
      }
    }
    stmt.Close()

    // Create conversations table
    stmt, err = db.Prepare("CREATE TABLE IF NOT EXISTS 'conversations' ( 'id'   INTEGER PRIMARY KEY AUTOINCREMENT, 'type' VARCHAR(3), '_user1' INTEGER,         '_user2' INTEGER, 'subject' VARCHAR(128), conv_type VARCHAR(16), 'created_at' DATETIME DEFAULT         CURRENT_TIMESTAMP, FOREIGN KEY (_user1) REFERENCES users(id), FOREIGN KEY       (_user2) REFERENCES users(id));")
    if !dataaccess.checkErr("Error preparing statement for conversations", err) {
      _, err := stmt.Exec()
      if !dataaccess.checkErr("Error executing statment", err) {
        dataaccess.log.Debug("Initialized conversations table")
      }
    }
    stmt.Close()


    // Create conversations table
    stmt, err = db.Prepare("CREATE TABLE IF NOT EXISTS 'conversations' ( 'id'   INTEGER PRIMARY KEY AUTOINCREMENT, 'type' VARCHAR(3), '_user1' INTEGER,         '_user2' INTEGER, 'subject' VARCHAR(128), 'created_at' DATETIME DEFAULT         CURRENT_TIMESTAMP, FOREIGN KEY (_user1) REFERENCES users(id), FOREIGN KEY       (_user2) REFERENCES users(id));")
    if !dataaccess.checkErr("Error preparing statement for conversations", err) {
      _, err := stmt.Exec()
      if !dataaccess.checkErr("Error executing statment", err) {
        dataaccess.log.Debug("Initialized conversations table")
      }
    }
    stmt.Close()

    // Create messages table
    stmt, err = db.Prepare("CREATE TABLE IF NOT EXISTS 'messages' ( 'id'        INTEGER PRIMARY KEY AUTOINCREMENT, 'body' VARCHAR(1024), 'created_at' DATETIME  DEFAULT CURRENT_TIMESTAMP, '_from' INTEGER, '_conversation' INTEGER,FOREIGN KEY (_from) REFERENCES users(id), FOREIGN KEY (_conversation) REFERENCES            conversations(id));")
    if !dataaccess.checkErr("Error preparing statement for messages", err) {
      _, err := stmt.Exec()
      if !dataaccess.checkErr("Error executing statment", err) {
        dataaccess.log.Debug("Initialized messages table")
      }
    }
    stmt.Close()

    // Create user_conversations table
    stmt, err = db.Prepare("CREATE TABLE IF NOT EXISTS 'user_conversations' (   'id' INTEGER PRIMARY KEY AUTOINCREMENT, 'created_at' DATETIME DEFAULT           CURRENT_TIMESTAMP, '_user' INTEGER, '_conversation' INTEGER, FOREIGN KEY        (_user) REFERENCES users(id), FOREIGN KEY (_conversation) REFERENCES            conversations(id));")
    if !dataaccess.checkErr("Error preparing statement for user_conversations", err) {
      _, err := stmt.Exec()
      if !dataaccess.checkErr("Error executing statment", err) {
        dataaccess.log.Debug("Initialized user_conversations table")
      }
    }
    stmt.Close()

	}
	return false
}

func (dataaccess DataAccess) connect() (*sql.DB, bool) {
	dataaccess.log.Debug("Connecting to database")
	db, err := sql.Open("sqlite3", dataaccess.dbname)
	return db, !dataaccess.checkErr("Error connecting to database", err)
}

func (dataaccess DataAccess) CreateConversation(subject string, conv_type string, user1 string, user2 string) (*conversation.Conversation, bool) {
  dataaccess.log.Debug("Create Conversation")
  db, success := dataaccess.connect()
  defer db.Close()
  if success {
    dataaccess.log.Debug("Creating conversation with subject: " + subject)
    var u1, u2 *user.User
    if conv_type == "SUC" {
      u1,_ = dataaccess.LookupUserByLogin(user1)
      u2,_ = dataaccess.LookupUserByLogin(user2)
    }

    stmt, err := db.Prepare("INSERT INTO conversations (subject, conv_type, _user1, _user2) VALUES (?,?,?,?);")
    defer stmt.Close()
    if !dataaccess.checkErr("Error preparing statement", err) {
      res, err := stmt.Exec(subject, conv_type, u1.GetID(), u2.GetID())
      if !dataaccess.checkErr("Error executing statment", err) {
        id, _ := res.LastInsertId()
        idstr := strconv.FormatInt(id, 10)
        dataaccess.log.Debug("Created conversation with id: " + idstr)
        c := conversation.Init(subject, conv_type, "", u1.GetLogin(), u2.GetLogin(), int(id))
        return c, true
      }
    }
  }
  return nil, false
}

func (dataaccess DataAccess) AddUserToMUC(c *conversation.Conversation, u *user.User) (string, bool) {
  db, success := dataaccess.connect()
  defer db.Close()
  if success {
    stmt, err := db.Prepare("INSERT INTO user_conversations (_user, _conversation) VALUES (?,?);")
    defer stmt.Close()
    if !dataaccess.checkErr("Error preparing statement for user update", err) {
      res, err := stmt.Exec(u.GetID(), c.GetID())
      if !dataaccess.checkErr("Error executing statment", err) {
        id, _ := res.LastInsertId()
        idstr := strconv.FormatInt(id, 10)
        dataaccess.log.Debug("Added user_conversation with id: " + idstr)
        return idstr, true
      }
    }
  }
  return "-1", false
}

func (dataaccess DataAccess) AddMessageToConversation(m *xmppmessage.Message,   c *conversation.Conversation) (string, bool) {
  db, success := dataaccess.connect()
  defer db.Close()
  if success {
    stmt, err := db.Prepare("INSERT INTO messages (body, _from, _conversation)  VALUES (?,?,?);")
    defer stmt.Close()
    if !dataaccess.checkErr("Error preparing statement for user update", err) {
      res, err := stmt.Exec(m.GetBody(), m.GetFrom().GetID(), c.GetID())
      if !dataaccess.checkErr("Error executing statment", err) {
        id, _ := res.LastInsertId()
        idstr := strconv.FormatInt(id, 10)
        dataaccess.log.Debug("Created message with id: " + idstr)
        return idstr, true
      }
    }
  }
  return "-1", false
}

func (dataaccess DataAccess) LookupConversationByID(id int) (*conversation.Conversation, bool) {
  db, success := dataaccess.connect()
  defer db.Close()
  if success {
    dataaccess.log.Debug(fmt.Sprintf("Looking up conversation by id %d", id))
    rows, err := db.Query("SELECT subject, conv_type, user1, user2, created_at  FROM conversations WHERE id = " + strconv.Itoa(id) + ";")
    defer rows.Close()
    if !dataaccess.checkErr("Error looking up user", err) {
      for rows.Next() {
        var user1, user2 int
        var u1, u2 *user.User
        var subject, conv_type, created_at string
        err = rows.Scan(&subject, &conv_type, &user1, &user2, &created_at)
        if conv_type == "SUC" {
          u1,_ = dataaccess.LookupUserById(user1)
          u2,_ = dataaccess.LookupUserById(user2)
        }
        c := conversation.Init(subject, conv_type, created_at, u1.GetLogin(), u2.GetLogin(), id)
        if dataaccess.checkErr("Error scanning row", err) {
          return nil, false
        }
        return c, true
      }
    }
    return nil, true
  }
  return nil, false
}

func (dataaccess DataAccess) LookupSUCByUsers(user1 string, user2 string) (*    conversation.Conversation, bool) {
  db, success := dataaccess.connect()
  defer db.Close()
  if success {
    dataaccess.log.Debug(fmt.Sprintf("Looking up conversation with users %s and %s", user1, user2))
    u1,suc1 := dataaccess.LookupUserByLogin(user1)
    u2,suc2 := dataaccess.LookupUserByLogin(user2)
    if suc1 && suc2 {
      var u1id, u2id string 
      dataaccess.log.Debug(fmt.Sprintf("Got users %s %d and %s %d", u1.GetLogin(), u1.GetID(), u2.GetLogin(), u2.GetID()))
      u1id = strconv.Itoa(u1.GetID())
      u2id = strconv.Itoa(u2.GetID())
      rows, err := db.Query("SELECT id, subject, created_at FROM conversations    WHERE lower(type)='SUC' and (_user1 = " + u2id + " AND _user2 = " + u1id + ") OR (_user1 = " + u1id + " AND _user2 = " + u2id + ");")
      defer rows.Close()
      if !dataaccess.checkErr("Error looking up user", err) {
        for rows.Next() {
          var id int
          var subject, created_at string
          err = rows.Scan(&id, &subject, &created_at)
          c := conversation.Init(subject, "SUC", created_at, user1, user2, id)
          if dataaccess.checkErr("Error scanning row", err) {
            return nil, false
          }
          return c, true
         }
      }
    }
    return nil, false
  }
  return nil, false
}

func (dataaccess DataAccess) GetAllUsers() ([]*user.User, bool) {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Gathering all users from database")
		rows, err := db.Query("SELECT id,login, first_name, last_name, password from user;")
		defer rows.Close()
		if !dataaccess.checkErr("Error looking up all users", err) {
			var users []*user.User
      var u *user.User
      var p user.Presence
			for rows.Next() {
				var login, fname, lname, pass string
        var id int

				err = rows.Scan(&id, &login, &fname, &lname, &pass)
        p = user.InitPresence("", "Unavailable")
        u = user.Init(fname, lname, login, pass, "", id, p)
				users = append(users, u)
				if dataaccess.checkErr("Error scanning row", err) {
					return nil, false
				}
			}
			return users, true
		}
		return nil, true
	}
	return nil, false

}

func (dataaccess DataAccess) LookupUserById(id int) (*user.User, bool) {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Looking up user with id: " + strconv.Itoa(id))
		rows, err := db.Query("SELECT login, first_name, last_name, password from user where id = '" + strconv.Itoa(id) + "'")
		defer rows.Close()
		if !dataaccess.checkErr("Error looking up user", err) {
			for rows.Next() {
				var fname, lname, pass, login string
				err = rows.Scan(&login, &fname, &lname, &pass)
        p := user.InitPresence("", "")
				u := user.Init(fname, lname, login, pass, "", id, p)
				if dataaccess.checkErr("Error scanning row", err) {
					return nil, false
				}
				return u, true
			}
		}
		return nil, true
	}
	return nil, false
}
func (dataaccess DataAccess) LookupUserByLogin(login string) (*user.User, bool) {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Looking up user with login: " + login)
		rows, err := db.Query("SELECT id, first_name, last_name, password from user where login = '" + login + "'")
		defer rows.Close()
		if !dataaccess.checkErr("Error looking up user", err) {
			for rows.Next() {
				var fname, lname, pass string
        var id int
				err = rows.Scan(&id, &fname, &lname, &pass)
        p := user.InitPresence("", "")
				u := user.Init(fname, lname, login, pass, "", id, p)
				if dataaccess.checkErr("Error scanning row", err) {
					return nil, false
				}
				return u, true
			}
		}
		return nil, false
	}
	return nil, false
}

func (dataaccess DataAccess) CreateUser(fname string, lname string, login string, pass string) (string, bool) {
	dataaccess.log.Debug("Create User")
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Creating user with login: " + login)
		stmt, err := db.Prepare("INSERT INTO user (first_name, last_name, login, password) VALUES (?,?,?,?)")
		defer stmt.Close()
		if !dataaccess.checkErr("Error preparing statement", err) {
			res, err := stmt.Exec(fname, lname, login, pass)
			if !dataaccess.checkErr("Error executing statment", err) {
				id, _ := res.LastInsertId()
				idstr := strconv.FormatInt(id, 10)
				dataaccess.log.Debug("Created user with id: " + idstr)
				return idstr, true
			}
		}
	}
	return "-1", false
}

func (dataaccess DataAccess) UpdateUser(u *user.User) bool {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		stmt, err := db.Prepare("UPDATE user SET first_name = ?, last_name = ?, password = ? where login = ?")
		defer stmt.Close()
		if !dataaccess.checkErr("Error preparing statement for user update", err) {
			res, err := stmt.Exec(u.GetFirstName(), u.GetLastName(), u.GetPass(), u.GetLogin())
			if !dataaccess.checkErr("Error updating user", err) {
				affect, _ := res.RowsAffected()
				if affect > 0 {
					dataaccess.log.Debug("Updated user with login: " + u.GetLogin())
					return true
				}
			}
		}
	}
	return false
}

func (dataaccess DataAccess) CreateOrUpdateUser(fname string, lname string, login string, pass string) bool {
	u, success := dataaccess.LookupUserByLogin(login)
	if success && u != nil {
		u.SetLogin(login)
		if lname != "" {
			u.SetLastName(lname)
		}
		if fname != "" {
			u.SetFirstName(fname)
		}
		if pass != "" {
			u.SetPass(pass)
		}
		return dataaccess.UpdateUser(u)
	} else if success {
		_, success = dataaccess.CreateUser(fname, lname, login, pass)
		return success
	}
	return false
}

func GetDataAccess() *DataAccess {
	newlog := logger.Logger{LogLevel: 0, TAG: "DataAccess"}
	once.Do(func() {
		dataaccess = &DataAccess{dbname: "developer.db", log: newlog}
		dataaccess.log.Init(os.Stdout)
		dataaccess.InitDB()
	})
	return dataaccess
}

func (dataaccess DataAccess) checkErr(msg string, err error) bool {
	if err != nil {
		dataaccess.log.Error(fmt.Sprintf("%s %s", msg, err.Error()))
		return true
	} else {
		return false
	}
}
