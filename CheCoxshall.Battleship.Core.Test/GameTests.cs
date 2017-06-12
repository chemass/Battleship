using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CheCoxshall.Battleship.Core.Test
{
    [TestClass]
    public class GameTests
    {


        [TestMethod]
        public void EnsureReady()
        {
            var gb = new Game();

            gb.PlaceShip(0, ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(0, ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(0, ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(0, ShipType.Destroyer, new Cell('c', 2), false);
            gb.PlaceShip(0, ShipType.Submarine, new Cell('D', 2), false);

            gb.PlaceShip(1, ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(1, ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(1, ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(1, ShipType.Destroyer, new Cell('c', 2), false);
            gb.PlaceShip(1, ShipType.Submarine, new Cell('D', 2), false);

            Assert.IsTrue(gb.Ready);
        }

        [TestMethod]
        public void EnsureEvents()
        {
            var gb = new Game();
            var events = new List<string>();

            gb.Hit += (sender, args) => events.Add("hit");
            gb.Miss += (sender, args) => events.Add("miss");
            gb.ShipDestroyed += (sender, args) => events.Add("dest" + args.DestroyedShip.ToString());

            gb.PlaceShip(0, ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(0, ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(0, ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(0, ShipType.Destroyer, new Cell('c', 2), false);
            gb.PlaceShip(0, ShipType.Submarine, new Cell('D', 2), false);

            gb.PlaceShip(1, ShipType.AircraftCarrier, new Cell('A', 1), true);
            gb.PlaceShip(1, ShipType.Battleship, new Cell('a', 2), false);
            gb.PlaceShip(1, ShipType.Cruiser, new Cell('b', 2), false);
            gb.PlaceShip(1, ShipType.Destroyer, new Cell('c', 2), false);
            gb.PlaceShip(1, ShipType.Submarine, new Cell('D', 2), false);

            gb.TakeShot(0, new Cell('f', 9));
            Assert.AreEqual(1, events.Count(x => x == "miss"));
            gb.TakeShot(0, new Cell('c', 2));
            Assert.AreEqual(1, events.Count(x => x == "hit"));
            gb.TakeShot(0, new Cell('c', 3));
            Assert.AreEqual(2, events.Count(x => x == "hit"));
            Assert.AreEqual(1, events.Count(x => x == "dest" + ShipType.Destroyer.ToString()));
        }
    }
}
