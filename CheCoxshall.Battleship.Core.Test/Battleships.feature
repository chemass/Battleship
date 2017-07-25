Feature: Battleships
	Make sure the game board is correctly populated before starting a game

@boardSetup
Scenario: Ensure Readiness
	Given I have placed an aircraftcarrier horizontally at cell A1
	And I have placed a battleship vertically at cell A2
	And I have placed a cruiser vertically at cell B2
	And I have placed a destroyer vertically at cell C2
	And I have placed a submarine vertically at cell D2
	Then the game board should be ready

@boardSetup
Scenario: Ensure I cannot start a game without placing all of my ships
	Given I have placed an aircraftcarrier horizontally at cell A1
	And I have placed a battleship vertically at cell A2
	And I have placed a cruiser vertically at cell B2
	And I have placed a destroyer vertically at cell C2
	Then the game board should not be ready

@gameplay
Scenario: EnsureTheDestructionOfADestroyer
	Given I have placed an aircraftcarrier horizontally at cell A1
	And I have placed a battleship vertically at cell A2
	And I have placed a cruiser vertically at cell B2
	And I have placed a destroyer vertically at cell C2
	When A shot lands at cell C2
	And A shot lands at cell C3
	Then My destroyer should be sunk
	
@gameplay
Scenario: EnsureTheDestructionOfACarrier
	Given I have placed an aircraftcarrier horizontally at cell A1
	And I have placed a battleship vertically at cell A2
	And I have placed a cruiser vertically at cell B2
	And I have placed a destroyer vertically at cell C2
	When A shot lands at cell A1
	And A shot lands at cell B1
	And A shot lands at cell C1
	And A shot lands at cell D1
	And A shot lands at cell E1
	Then My aircraftcarrier should be sunk

@gameBoard
Scenario: Ensure game board does not allow overlapping ships
	Given I have placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C4   |
	| submarine       | vertically   | B3   |
	| destroyer       | horizontally | I9   |
	Then the game board should have thrown a CannotPlaceException with the message 'Ships may not overlap.'
	
@gameBoard
Scenario: Ensure game board does not allow ships to fall off the edge of the world
	Given I have placed an aircraftcarrier horizontally at cell G1
	Then the game board should have thrown a CannotPlaceException with the message 'Ship extends beyond board boundaries.'
	
@gameBoard
Scenario: Ensure game board does not allow duplicate ships
	Given I have placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| aircraftcarrier | vertically   | B1   |
	Then the game board should have thrown a CannotPlaceException with the message 'This type of ship has already been placed.'