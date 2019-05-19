Feature: Simple Messaging

Scenario: Simple message exchange
Given the following users:
	| User     |
	| Johnny   |
	| Benjamin |
	When they send the following messages in order:
    | From     | To       | Text       |
    | Johnny   | Benjamin | Hello!     |
    | Benjamin | Johnny   | Hi!        |
    | Johnny   | Benjamin | What's up? |
    | Benjamin | Johnny   | Nothing.   |
	Then Johnny should see:
    | From     | Text       |
    | Me       | Hello!     |
    | Benjamin | Hi!        |
    | Me       | What's up? |
    | Benjamin | Nothing.   |
	And Benjamin should see:
    | From   | Text       |
    | Johnny | Hello!     |
    | Me     | Hi!        |
    | Johnny | What's up? |
    | Me     | Nothing.   |



