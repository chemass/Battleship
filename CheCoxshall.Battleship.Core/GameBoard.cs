using System;
using System.Collections.Generic;
using System.Linq;

namespace CheCoxshall.Battleship.Core
{
    internal class GameBoard
    {
        private char[] _board = new char[100];
        private ShipType _placedShips = ShipType.None;

        public bool Ready => _placedShips.HasFlag(ShipType.All); 

        /// <summary>
        /// Place a ship on the game board
        /// </summary>
        /// <param name="ship">Type of ship to place</param>
        /// <param name="cell">Starting cell</param>
        /// <param name="isHorizontal">Indicates if the ship is horizontal</param>
        public void PlaceShip(ShipType ship, Cell cell, bool isHorizontal)
        {
            if (_placedShips.HasFlag(ship))
                throw new CannotPlaceException("This type of ship has already been placed.");

            var shipLength = GetShipLength(ship);
            Cell[] targetRange = null;

            try
            {
                targetRange = cell.Range(shipLength, isHorizontal).ToArray();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new CannotPlaceException("Ship extends beyond board boundaries.", ex);
            }

            if (targetRange.Any(x => _board[x.ToBoardIndex()] != 0))
                throw new CannotPlaceException("Ships may not overlap.");

            foreach (var targetCell in targetRange)
                _board[targetCell.ToBoardIndex()] = ship.ToString()[0];

            _placedShips = _placedShips | ship;
        }

        internal char[] GetBoard()
        {
            return _board;
        }

        /// <summary>
        /// Registers a shot on the board
        /// </summary>
        /// <param name="targetCell"></param>
        /// <returns></returns>
        public ShotResult RegisterShot(Cell targetCell)
        {
            switch (_board[targetCell.ToBoardIndex()])
            {
                case 'A':
                    return RegisterHit(ShipType.AircraftCarrier, targetCell);
                case 'B':
                    return RegisterHit(ShipType.Battleship, targetCell);
                case 'C':
                    return RegisterHit(ShipType.Cruiser, targetCell);
                case 'D':
                    return RegisterHit(ShipType.Destroyer, targetCell);
                case 'S':
                    return RegisterHit(ShipType.Submarine, targetCell);
                case 'H':
                    throw new InvalidShotException("Cannot target the same cell twice.");
                default:
                    _board[targetCell.ToBoardIndex()] = 'M';
                    return new ShotResult();
            }
        }

        private ShotResult RegisterHit(ShipType ship, Cell targetCell)
        {
            _board[targetCell.ToBoardIndex()] = 'H';
            return new ShotResult(ship, IsDestroyed(ship, targetCell));
        }

        private bool IsDestroyed(ShipType ship, Cell targetCell)
        {
            var c = ship.ToString()[0];
            return !_board.Any(x => x == c);
        }

        private static int GetShipLength (ShipType type)
        {
            switch (type)
            {
                case ShipType.AircraftCarrier:
                    return 5;
                case ShipType.Battleship:
                    return 4;
                case ShipType.Cruiser:
                case ShipType.Submarine:
                    return 3;
                case ShipType.Destroyer:
                    return 2;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
