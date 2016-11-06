package user

type User struct {
	fname      string
	lname      string
	login      string
	pass       string
	created_at string
	id         int
}

func Init(fname string, lname string, login string, pass string, created_at string, id int) *User {
	return &User{fname: fname, lname: lname, login: login, pass: pass, created_at: created_at, id: id}
}

func (u *User) GetFirstName() string {
	return u.fname
}

func (u *User) GetLastName() string {
	return u.lname
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
