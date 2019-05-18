Feature: Simple Messaging
	In order to communicate privately
	As a chat user
	I want to be able to send a direct message to one of my friends

Scenario: Simple message exchange
	Given a user Johnny
    And a user Benjamin
    And that he is a friend of Johnny
	When They send the following messages in order:
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

Scenario: Trying to message somebody who is not a friend
	Given a user Johnny
    And a user Benjamin
    And that he is not a friend of Johnny
	When Johnny sends a message "Hey!" to Benjamin
	Then Johnny should see:
	    | From     | Text                                |
	    | Me       | Hey!                                |
	    | Benjamin | Automatic message:                  |
	    |          | You are not my friend.              |
	    |          | Either join my friends or get lost. |

