Feature: Simple Messaging
	In order to communicate privately
	As a chat user
	I want to be able to send a direct message to one of my friends

Scenario: Simple message exchange
	Given a user Johnny
    And a user Benjamin
    And that Johnny is friends with him
	When They send the following messages in order:
    | From     | To       | Text       |
    | Johnny   | Benjamin | Hello!     |
    | Benjamin | Johnny   | Hi!        |
    | Johnny   | Benjamin | What's up? |
    | Benjamin | Johnny   | Nothing.   |
	Then Johnny should see the following messages:
    | From     | Text       |
    | Me       | Hello!     |
    | Benjamin | Hi!        |
    | Me       | What's up? |
    | Benjamin | Nothing.   |
	And Benjamin should see the following messages:
    | From   | Text       |
    | Johnny | Hello!     |
    | Me     | Hi!        |
    | Johnny | What's up? |
    | Me     | Nothing.   |

Scenario: Trying to message somebody who is not in friends group
	Given the following users:
    | Username |
    | Johnny   |
    | Benjamin |
    | Dan      |
    And a friends group that consists of
    | Username |
    | Johnny   |
    | Benjamin |
	When They send the following messages in order:
    | From     | To       | Text       |
    | Johnny   | Benjamin | Hello!     |
    | Benjamin | Johnny   | Hi!        |
    | Johnny   | Benjamin | What's up? |
    | Benjamin | Johnny   | Nothing.   |
	Then Johnny should see the following messages:
    | From     | Text       |
    | Me       | Hello!     |
    | Benjamin | Hi!        |
    | Me       | What's up? |
    | Benjamin | Nothing.   |
	And Benjamin should see the following messages:
    | From   | Text       |
    | Johnny | Hello!     |
    | Me     | Hi!        |
    | Johnny | What's up? |
    | Me     | Nothing.   |
