Feature: Battleships Gameplay
	
@gameplay
Scenario: EnsureCorrectGameGeneration
	Given A new game is created
	Then It should have two game boards

Scenario: EnsureGameIsNotReadyBeforeEachPlayerHasPlacedTheirShips
	Given A new game is created
	And Player 1 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| submarine       | vertically   | A9   |
	| destroyer       | horizontally | I9   |
	And Player 2 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| cruiser         | horizontally | C4   |
	| submarine       | vertically   | B5   |
	| destroyer       | horizontally | I9   |
	Then the game should not be ready

Scenario: EnsureGameIsReadyAfterEachPlayerHasPlacedTheirShips
	Given A new game is created
	And Player 1 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	And Player 2 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	Then the game should be ready

Scenario: EnsureAHitIsRegisteredCorrectly
	Given A new game is created
	And Player 1 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	And Player 2 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	When Player 1 has taken a shot at A1
	Then A hit should be registered

Scenario: EnsureAMissIsRegisteredCorrectly
	Given A new game is created
	And Player 1 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	And Player 2 has placed the following ships
	| Ship Type       | Orientation  | Cell |
	| aircraftcarrier | horizontally | A1   |
	| battleship      | vertically   | A2   |
	| cruiser         | horizontally | C3   |
	| submarine       | vertically   | J2   |
	| destroyer       | horizontally | I9   |
	When Player 1 has taken a shot at A9
	Then A miss should be registered

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
