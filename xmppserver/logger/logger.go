package logger

import (
	"io"
	"log"
)

// LogLevel 0 = All, 1 = Info and Error, 2 = Error, 3 = No logging
type Logger struct {
	LogLevel int
  TAG string
}

var (
	InfoLogger  *log.Logger
	DebugLogger *log.Logger
	ErrorLogger *log.Logger
)

func (l Logger) Init(handle io.Writer) {
	InfoLogger = log.New(handle,
		"INFO: ",
		log.Ldate|log.Ltime|log.Lshortfile)

	DebugLogger = log.New(handle,
		"DEBUG: ",
		log.Ldate|log.Ltime|log.Lshortfile)

	ErrorLogger = log.New(handle,
		"ERROR: ",
		log.Ldate|log.Ltime|log.Lshortfile)
}

func (l Logger) Info(msg string) {
	if l.LogLevel <= 1 {
		InfoLogger.Printf("[" + l.TAG + "] INFO: %s\n", msg)
	}
}

func (l Logger) Debug(msg string) {
	if l.LogLevel <= 0 {
		DebugLogger.Printf("[" + l.TAG + "] DEBUG: %s\n", msg)
	}
}

func (l Logger) Error(msg string) {
	if l.LogLevel <= 2 {
		ErrorLogger.Printf("[" + l.TAG + "] ERROR: %s\n", msg)
	}
}
