using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CheCoxshall.Battleship.Core.Test
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        [ExpectedException(typeof(CannotPlaceException))]
        public void EnsureSingleShipTypes()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 2), true);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotPlaceException))]
        public void EnsureVerticalBounds()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 7), false);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotPlaceException))]
        public void EnsureHorizontalBounds()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('g', 1), true);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotPlaceException))]
        public void EnsureNoOverlap()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(ShipType.Battleship, new Cell('B', 1), false);
        }

        [TestMethod]
        public void EnsureReadiness()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(ShipType.Destroyer, new Cell('c', 2), false);

            Assert.IsFalse(gb.Ready);

            gb.PlaceShip(ShipType.Submarine, new Cell('D', 2), false);

            Assert.IsTrue(gb.Ready);
        }

        [TestMethod]
        public void EnsureDestruction()
        {
            var gb = new GameBoard();

            gb.PlaceShip(ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(ShipType.Destroyer, new Cell('c', 2), false);
            gb.PlaceShip(ShipType.Submarine, new Cell('D', 2), false);

            Assert.IsTrue(gb.RegisterShot(new Cell('c', 2)).IsHit);
            var result = gb.RegisterShot(new Cell('c', 3));
            Assert.IsTrue(result.IsHit);
            Assert.IsTrue(result.IsSunk);
            Assert.AreEqual(ShipType.Destroyer, result.Ship);
        }
    }
}
