package user

type Presence struct {
	show   string
	status string
}

func (p Presence) GetShow() string {
  return p.show
}

func (p Presence) GetStatus() string {
  return p.status
}

type User struct {
	fname      string
	lname      string
	login      string
	pass       string
	created_at string
	id         int
	presence   Presence
}

func InitPresence(show string, status string) Presence {
  return Presence{show: show, status: status}
}


func Init(fname string, lname string, login string, pass string, created_at string, id int, p Presence) *User {
	return &User{fname: fname, lname: lname, login: login, pass: pass, created_at: created_at, id: id, presence: p}
}

func (u *User) GetID() int {
  return u.id
}

func (u *User) SetID(id int) {
  u.id = id
}


func (u *User) GetPresence() Presence {
	return u.presence
}

func (u *User) GetFirstName() string {
	return u.fname
}

func (u *User) GetLastName() string {
	return u.lname
}

func (u *User) GetNick() string {
  return (u.fname + " " + u.lname)
}

func (u *User) GetLogin() string {
	return u.login
}

func (u *User) GetPass() string {
	return u.pass
}

func (u *User) SetFirstName(fname string) {
	u.fname = fname
}

func (u *User) SetLastName(lname string) {
	u.lname = lname
}

func (u *User) SetLogin(login string) {
	u.login = login
}

func (u *User) SetPass(pass string) {
	u.pass = pass
}
