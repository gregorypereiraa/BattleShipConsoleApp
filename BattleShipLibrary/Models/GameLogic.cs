namespace BattleShipLibrary.Models;

public static class GameLogic
{
     public static void InitializationGrid(PlayerInfoModel model)
    {
        var letters = new List<string>
        {
            "A",
            "B",
            "C",
            "D",
            "E"
        };

        var numbers = new List<int>
        {
            1, 2, 3, 4, 5
        };

        foreach (var letter in letters)
        {
            foreach (var number in numbers)
            {
                AddGridSpot(model,letter,number);
            }
        }
    }
    private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
    {
        GridSpotModel spot = new GridSpotModel
        {
            spotLetter = letter,
            spotNumber = number,
            status = GridSpotStatus.Empty
        };
        model.ShotGrid.Add(spot);
    }


    public static bool PlaceShip(PlayerInfoModel model, string location)
    {
        return true;
    }

    public static bool PlayerStillActive(PlayerInfoModel passivePlayer)
    {
        throw new NotImplementedException();
    }

    public static int GetShotCount(PlayerInfoModel winner)
    {
        throw new NotImplementedException();
    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        throw new NotImplementedException();
    }

    public static bool ValidateShot(string row, int column, PlayerInfoModel activePlayer)
    {
        throw new NotImplementedException();
    }

    public static bool IdentifyShotResult(string row, int column, PlayerInfoModel passivePlayer)
    {
        throw new NotImplementedException();
    }

    public static void MarkShotResult(string row, int column, PlayerInfoModel activePlayer)
    {
        throw new NotImplementedException();
    }
}