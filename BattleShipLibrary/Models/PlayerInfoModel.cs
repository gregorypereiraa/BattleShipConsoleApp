namespace BattleShipLibrary.Models;

public class PlayerInfoModel
{
    public string UserName { get; set; }
    public List<GridSpotModel> ShipLocation { get; set; }
    public List<GridSpotModel> DisplayGrid { get; set; }
}