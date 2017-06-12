using System;

namespace CheCoxshall.Battleship.Core
{
    [Flags]
    public enum ShipType
    {
        None = 0,
        AircraftCarrier = 1,
        Battleship = 2,
        Cruiser = 4,
        Submarine = 8,
        Destroyer = 16,
        All = AircraftCarrier | Battleship | Cruiser | Submarine | Destroyer
    }
}
