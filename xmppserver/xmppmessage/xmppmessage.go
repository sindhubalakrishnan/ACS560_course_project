package xmppmessage

import (
	"github.com/xweskingx/ACS560_course_project/xmppserver/conversation"
	"github.com/xweskingx/ACS560_course_project/xmppserver/user"
)

type Message struct {
	created_at   string
	body         string
	from         *user.User
	conversation *conversation.Conversation
	id           int
}

func Init(created_at string, body string, from *user.User, conversation *conversation.Conversation, id int) *Message {
	return &Message{created_at: created_at, body: body, from: from, conversation: conversation, id: id}
}

func (m *Message) GetID() int {
	return m.id
}

func (m *Message) SetID(id int) {
	m.id = id
}
func (m *Message) GetBody() string {
	return m.body
}

func (m *Message) SetBody(body string) {
	m.body = body
}

func (m *Message) GetCreatedAt() string {
	return m.created_at
}

func (m *Message) SetCreatedAt(created_at string) {
	m.created_at = created_at
}

func (m *Message) GetFrom() *user.User {
	return m.from
}

func (m *Message) SetFrom(from *user.User) {
	m.from = from
}

func (m *Message) GetConversation() *conversation.Conversation {
	return m.conversation
}

func (m *Message) SetConversation(conversation *conversation.Conversation) {
	m.conversation = conversation
}
