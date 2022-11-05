namespace BattleShipLibrary.Models;

public class PlayerInfoModel
{
    public string UserName { get; set; }
    public List<GridSpotModel> ShipLocation { get; set; } = new();
    public List<GridSpotModel> ShotGrid { get; set; } = new();
}