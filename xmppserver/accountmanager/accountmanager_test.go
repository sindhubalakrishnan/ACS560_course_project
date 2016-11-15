package accountmanager

import (
	"testing"
)

func TestGetAccountManager(t *testing.T) {
	manager := GetAccountManager()
	if manager == nil {
		t.Error("AccountManager was nil")
	}
	new_manager := GetAccountManager()
	if manager != new_manager {
		t.Error("Second instance of Account Manager was created")
	}
}
