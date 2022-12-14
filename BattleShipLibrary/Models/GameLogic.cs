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
        foreach (var number in numbers)
            AddGridSpot(model, letter, number);
    }

    private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
    {
        var spot = new GridSpotModel
        {
            SpotLetter = letter,
            SpotNumber = number,
            Status = GridSpotStatus.Empty
        };
        model.ShotGrid.Add(spot);
    }


    public static bool PlaceShip(PlayerInfoModel player, string location)
    {
        var output = false;
        var (row, column) = SplitShotIntoRowAndColumn(location);
        var isValidLocation = ValidateGridLocation(player, row, column);
        var isShipAtLocation = ValidateShipPlacement(player, row, column);
        if (isValidLocation && isShipAtLocation)
        {
            player.ShipLocation.Add(new GridSpotModel
            {
                SpotLetter = row,
                SpotNumber = column,
                Status = GridSpotStatus.Ship
            });
            output = true;
        }

        return output;
    }

    private static bool ValidateShipPlacement(PlayerInfoModel player, string row, int column)
    {
        var isValidLocation = true;
        foreach (var ship in player.ShipLocation)
            if (ship.SpotLetter == row && ship.SpotNumber == column)
                isValidLocation = false;
        return isValidLocation;
    }

    private static bool ValidateGridLocation(PlayerInfoModel player, string row, int column)
    {
        var isValidLocation = false;

        foreach (var grid in player.ShotGrid)
            if (grid.SpotLetter == row && grid.SpotNumber == column)
                isValidLocation = true;

        return isValidLocation;
    }

    public static bool PlayerStillActive(PlayerInfoModel passivePlayer)
    {
        var isActive = false;
        foreach (var ship in passivePlayer.ShipLocation)
            if (ship.Status != GridSpotStatus.Sunk)
                isActive = true;
        return isActive;
    }

    public static int GetShotCount(PlayerInfoModel player)
    {
        var shotCount = 0;
        foreach (var shot in player.ShotGrid)
            if (shot.Status != GridSpotStatus.Empty)
                shotCount += 1;
        return shotCount;
    }

    public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
    {
        var row = "";
        var column = 0;
        if (shot.Length != 2) throw new ArgumentException("This was an invalid shot type", "shot");
        var shotArray = shot.ToCharArray();
        row = shotArray[0].ToString().ToUpper();
        column = int.Parse(shotArray[1].ToString());
        return (row, column);
    }

    public static bool ValidateShot(string row, int column, PlayerInfoModel player)
    {
        var isValidShot = false;
        foreach (var gridSpot in player.ShotGrid)
            if (gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
                if (gridSpot.Status == GridSpotStatus.Empty)
                    isValidShot = true;
        return isValidShot;
    }

    public static bool IdentifyShotResult(string row, int column, PlayerInfoModel Opponent)
    {
        var isAHit = false;
        foreach (var ship in Opponent.ShipLocation)
            if (ship.SpotLetter == row && ship.SpotNumber == column)
            {
                ship.Status = GridSpotStatus.Sunk;
                isAHit = true;
            }
        return isAHit;
    }

    public static void MarkShotResult(string row, int column, PlayerInfoModel player, bool isAHit)
    {
        foreach (var gridSpot in player.ShotGrid)
            if (gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
                if (isAHit)
                    gridSpot.Status = GridSpotStatus.Hit;
                else
                    gridSpot.Status = GridSpotStatus.Miss;
    }

    /*public static void ComputerShipsPlacement(PlayerInfoModel output)
    {
        string chars = "ABCDE";
        Random rd = new Random();
        for (int i = 0; i < 5;)
        {
            string row = chars[rd.Next(5)].ToString();
            int column = rd.Next(5);
            if (ValidateShipPlacement(output,row,column))
            {
                output.ShipLocation.Add(new GridSpotModel(
                {
                    SpotLetter = row,
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });
                i++;

            }
            
        }
    }*/
}