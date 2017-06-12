using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheCoxshall.Battleship.Core
{
    public class Game
    {
        private GameBoard[] PlayerBoards;

        public event EventHandler Hit;
        public event EventHandler Miss;
        public event EventHandler<ShipDestroyedEventArgs> ShipDestroyed;

        /// <summary>
        /// Construct a new game
        /// </summary>
        public Game() {
            PlayerBoards = new[] { new GameBoard(), new GameBoard() };
        }

        /// <summary>
        /// Place a ship on the specified board
        /// </summary>
        /// <param name="player"></param>
        /// <param name="ship"></param>
        /// <param name="prowLocation"></param>
        /// <param name="isHorizontal"></param>
        public void PlaceShip(int player, ShipType ship, Cell prowLocation, bool isHorizontal)
        {
            if (player < 0 || player > 1)
                throw new ArgumentOutOfRangeException("Only two players allowed!");

            PlayerBoards[player].PlaceShip(ship, prowLocation, isHorizontal);
        }

        /// <summary>
        /// If the game is ready to start
        /// </summary>
        public bool Ready => PlayerBoards.All(x => x.Ready);
        

        public void TakeShot(int targetPlayer, Cell targetCell)
        {
            if (targetPlayer < 0 || targetPlayer > 1)
                throw new ArgumentOutOfRangeException("Only two players allowed!");

            var shotResult = PlayerBoards[targetPlayer].RegisterShot(targetCell);

            if (shotResult.IsHit)
            {
                Hit?.Invoke(this, EventArgs.Empty);
                if (shotResult.IsSunk)
                    ShipDestroyed?.Invoke(this, new ShipDestroyedEventArgs { DestroyedShip = shotResult.Ship });
            }
            else
                Miss?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerable<char[]> GetGameBoardRows(int player)
        {
            if (player < 0 || player > 1)
                throw new ArgumentOutOfRangeException("Only two players allowed!");

            for (int i = 0; i < 100; i+=10)
                yield return PlayerBoards[player].GetBoard().Skip(i).Take(10).ToArray();
        }
    }
}
