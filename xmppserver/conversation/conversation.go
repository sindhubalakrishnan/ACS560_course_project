package conversation

type Conversation struct {
	subject    string
	conv_type  string
	created_at string
	user1      string
	user2      string
	id         int
}

func Init(subject string, conv_type string, created_at string, user1 string, user2 string, id int) *Conversation {
	return &Conversation{subject: subject, conv_type: conv_type, created_at: created_at, user1: user1, user2: user2, id: id}
}

func (c *Conversation) GetID() int {
	return c.id
}

func (c *Conversation) SetID(id int) {
	c.id = id
}

func (c *Conversation) GetSubject() string {
	return c.subject
}

func (c *Conversation) SetSubject(subject string) {
	c.subject = subject
}

func (c *Conversation) GetCreatedAt() string {
	return c.created_at
}

func (c *Conversation) SetCreatedAt(created_at string) {
	c.created_at = created_at
}

func (c *Conversation) GetUser1() string {
	return c.user1
}

func (c *Conversation) SetUser1(user1 string) {
	c.user1 = user1
}

func (c *Conversation) GetUser2() string {
	return c.user2
}

func (c *Conversation) SetUser2(user2 string) {
	c.user2 = user2
}

func (c *Conversation) GetConvType() string {
	return c.conv_type
}

func (c *Conversation) SetConvType(conv_type string) {
	c.conv_type = conv_type
}

func (c *Conversation) isMUC() bool {
	return (c.conv_type == "MUC")
}
