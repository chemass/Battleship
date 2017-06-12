using System;

namespace CheCoxshall.Battleship.Core
{
    public class ShipDestroyedEventArgs : EventArgs
    {
        public ShipType DestroyedShip { get; set; }
    }
}
