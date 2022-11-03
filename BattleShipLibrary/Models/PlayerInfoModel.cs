namespace BattleShipLibrary.Models;

public class PlayerInfoModel
{
    public string userName { get; set; }
    public List<GridSpotModel> shipLocation { get; set; }
    public List<GridSpotModel> shotGrid { get; set; }
}