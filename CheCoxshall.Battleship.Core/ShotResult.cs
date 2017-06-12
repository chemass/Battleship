namespace CheCoxshall.Battleship.Core
{
    public class ShotResult
    {
        public bool IsHit => Ship != ShipType.None;
        public ShipType Ship { get; private set; }
        public bool IsSunk { get; private set; }
        
        public ShotResult(ShipType hitTarget = ShipType.None, bool isSunk = false)
        {
            Ship = hitTarget;
            IsSunk = isSunk;
        }
    }
}