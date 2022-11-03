namespace BattleShipLibrary.Models;

public class GridSpotModel
{
    public string spotLetter { get; set; }
    public int spotNumber { get; set; }
    public GridSpotStatus status { get; set; } = GridSpotStatus.Empty;

}