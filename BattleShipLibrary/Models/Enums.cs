namespace BattleShipLibrary.Models;

//binaire, 0 for empty, 1 ship all grid status are 0 or 1 at the begining,
// it changes to 2 when hit , 3 when missed.

public enum GridSpotStatus {
    Empty,
    Ship,
    Hit,
    Miss,
    Sunk
}