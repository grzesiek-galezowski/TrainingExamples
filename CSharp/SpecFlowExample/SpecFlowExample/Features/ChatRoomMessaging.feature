Feature: GroupMessaging

Scenario: Messaging a chat room
	Given the following users:
	| User     |
	| Johnny   |
	| Benjamin |
	| Jenny    |
	| Benny    |
    And a chat room "team mates" that contains:
	| User     |
	| Johnny   |
	| Benjamin |
	| Jenny    |
	When Johnny sends a message "Hey!" to chat room "team mates"
	Then Johnny should see:
    | From     | Text       |
    | Me       | Hello!     |
	And Benjamin should see:
    | From     | Text       |
    | Me       | Hello!     |
	And Jenny should see:
    | From     | Text       |
    | Me       | Hello!     |
	And Benny should see nothing