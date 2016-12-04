package dataaccess

import (
	"database/sql"
	"fmt"
	"github.com/xweskingx/ACS560_course_project/xmppserver/logger"
	"github.com/xweskingx/ACS560_course_project/xmppserver/user"
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
		stmt, err := db.Prepare("CREATE TABLE IF NOT EXISTS'user' ( 'id' INTEGER PRIMARY KEY AUTOINCREMENT, 'login' VARCHAR(64) UNIQUE, 'password' VARCHAR(64),  'first_name' VARCHAR(64) NULL, 'last_name' VARCHAR(64) NULL, 'created_at' DATETIME DEFAULT CURRENT_TIMESTAMP );")
		defer stmt.Close()
		if !dataaccess.checkErr("Error preparing statement", err) {
			_, err := stmt.Exec()
			if !dataaccess.checkErr("Error executing statment", err) {
				dataaccess.log.Debug("Initialized users table")
				return true
			}
		}
	}
	return false
}

func (dataaccess DataAccess) connect() (*sql.DB, bool) {
	dataaccess.log.Debug("Connecting to database")
	db, err := sql.Open("sqlite3", dataaccess.dbname)
	return db, !dataaccess.checkErr("Error connecting to database", err)
}

func (dataaccess DataAccess) GetAllUsers() ([]*user.User, bool) {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Gathering all users from database")
		rows, err := db.Query("SELECT login, first_name, last_name, password from user;")
		defer rows.Close()
		if !dataaccess.checkErr("Error looking up all users", err) {
			var users []*user.User
      var u *user.User
      var p user.Presence
			for rows.Next() {
				var login, fname, lname, pass string

				err = rows.Scan(&login, &fname, &lname, &pass)
        p = user.InitPresence("", "Unavailable")
        u = user.Init(fname, lname, login, pass, "", -1, p)
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

func (dataaccess DataAccess) LookupUserByLogin(login string) (*user.User, bool) {
	db, success := dataaccess.connect()
	defer db.Close()
	if success {
		dataaccess.log.Debug("Looking up user with login: " + login)
		rows, err := db.Query("SELECT first_name, last_name, password from user where login = '" + login + "'")
		defer rows.Close()
		if !dataaccess.checkErr("Error looking up user", err) {
			for rows.Next() {
				var fname, lname, pass string
				err = rows.Scan(&fname, &lname, &pass)
        p := user.InitPresence("", "")
				u := user.Init(fname, lname, login, pass, "", -1, p)
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
