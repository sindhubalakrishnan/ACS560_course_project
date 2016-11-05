package dataaccess

import (
  "database/sql"
  "os"
  "testing"
  "xmppserver/logger"
)

var testdataaccess *DataAccess

func setup(t *testing.T) (*DataAccess) {
  t.Logf("Setting up Database")
  db,_ := sql.Open("sqlite3", "testdeveloper.db")
  defer db.Close()
  _, err := db.Exec("CREATE TABLE IF NOT EXISTS user( id INTEGER PRIMARY KEY AUTOINCREMENT, login VARCHAR(64) UNIQUE, password VARCHAR(64), first_name VARCHAR(64) NULL, last_name VARCHAR(64) NULL, created_at DATETIME DEFAULT CURRENT_TIMESTAMP)")
  if err != nil {
    t.Error(err.Error())
  }
  stmt, err := db.Prepare("INSERT INTO user (first_name, last_name, login, password) VALUES (?,?,?,?)")
  defer stmt.Close()
  if err != nil {
    t.Error(err.Error())
  }
  _, err = stmt.Exec("Paul", "Atreides", "atreidesp", "hulud1")
  if err != nil {
    t.Error(err.Error())
  }
  var testdataaccess *DataAccess
  newlog := logger.Logger{LogLevel: 0, TAG: "TestDataAccess"}
  testdataaccess = &DataAccess{dbname: "testdeveloper.db", log: newlog}
  testdataaccess.log.Init(os.Stdout)
  return testdataaccess
}

func teardown(t *testing.T) {
  t.Logf("Tearing down Database")
  db, err := sql.Open("sqlite3", "testdeveloper.db")
  defer db.Close()
  stmt, err := db.Prepare("DROP TABLE IF EXISTS user")
  defer stmt.Close()
  if err != nil {
    t.Error(err.Error())
  }
  _,err = stmt.Exec()
  if err != nil {
    t.Error(err.Error())
  }
}

func TestConnect(t *testing.T) {
  t.Logf("Testing connect()")
  defer teardown(t)
  testdataaccess = setup(t)
  db, success := testdataaccess.connect()
  defer db.Close()

  if success != true {
    t.Error("Failed to get a database connection")
  }

  if db == nil {
    t.Error("Database connection was nil")
  }
}

func TestLookupUserByLogin(t *testing.T) {
  t.Logf("Testing LookupUserByLogin()")
  defer teardown(t)
  testdataaccess = setup(t)
  u, success := testdataaccess.LookupUserByLogin("atreidesp")
  if success != true {
    t.Error("Error looking up user")
  }
  if u != nil {
    if u.fname != "Paul" {
      t.Error("Found incorrect user")
    }
  } else {
    t.Error("Could not find user")
  }
}

func TestCreateUser(t *testing.T) {
  t.Logf("Testing CreateUser()")
  defer teardown(t)
  testdataaccess = setup(t)
  id, success := testdataaccess.CreateUser("Leto", "Atreides", "atreidesl", "1234")
  if !success {
    t.Error("Failed to create a user")
  }
  if id != "2" {
    t.Error("Wrong id during user creation");
  }
}

func TestUpdateUser(t *testing.T) {
  t.Logf("Testing UpdateUser()")
  testdataaccess = setup(t)
  defer teardown(t)
  testuser := &user{fname: "Paul", lname: "Atreides", login: "atreidesp", pass: "shai1"}
  success := testdataaccess.UpdateUser(testuser)
  if !success {
    t.Error("Failed to update user")
  }
  testinvaliduser := &user{fname: "Paul", lname: "Atreides", login: "doesnotexist", pass: "shai1"}
  success = testdataaccess.UpdateUser(testinvaliduser)
  if success {
    t.Error("Invalid user should not have been updated")
  }
}

func TestCreateOrUpdateUser(t *testing.T) {
  t.Logf("Testing CreateOrUpdateUser()") 
  testdataaccess = setup(t)
  defer teardown(t)
  success := testdataaccess.CreateOrUpdateUser("Leto", "", "atreidesp", "1234")
  if !success {
    t.Error("Failed to update user")
  }
}


