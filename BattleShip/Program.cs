using BattleShipLibrary.Models;
using Spectre.Console;

WelcomeMessage();
var activePlayer = CreatePlayer("Player 1");
Console.Clear();
var Opponent = CreatePlayer("Player 2");
PlayerInfoModel winner = null;

do
{
    DisplayShotGrid(activePlayer);
    RecordPlayerShot(activePlayer, Opponent);
    var doesGameContinue = GameLogic.PlayerStillActive(Opponent);


    if (doesGameContinue)
        (activePlayer, Opponent) = (Opponent, activePlayer);
    else
        winner = activePlayer;
} while (winner == null);

MessageToWinner(winner);


static void WelcomeMessage()
{
    Console.WriteLine("*****************************");
    Console.WriteLine("Welcome to the BattleShip App");
    Console.WriteLine("Created by Gregory");
    Console.WriteLine("*****************************");
    Console.WriteLine();
}

static PlayerInfoModel CreatePlayer(string PlayerTitle)
{
    var output = new PlayerInfoModel();
    Console.WriteLine($"Player information for {PlayerTitle}");
    output.UserName = GetPlayerName();
    GameLogic.InitializationGrid(output);
    Console.WriteLine("Find below the grid\n");
    DisplayShotGrid(output);
    LineReturn(2);
    PlaceShips(output);
    Console.Clear();

    return output;
}

static string? GetPlayerName()
{
    var ouptut = AnsiConsole.Ask<string>("What is your name: ");
    return ouptut;
}

static void PlaceShips(PlayerInfoModel model)
{
    do
    {
        var location =
            AnsiConsole.Ask<string>(
                $"Out of 5 ships, where do you want to put your ship number{model.ShipLocation.Count + 1}:?");
        var isValidLocation = false;
        try
        {
            isValidLocation = GameLogic.PlaceShip(model, location);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (isValidLocation == false) Console.WriteLine("That is not a valid location, please try again");

        ;
    } while (model.ShipLocation.Count < 2);
}

static void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    var currentRow = activePlayer.ShotGrid[0].SpotLetter;
    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.SpotLetter != currentRow)
        {
            LineReturn(1);
            currentRow = gridSpot.SpotLetter;
        }

        if (gridSpot.Status == GridSpotStatus.Empty)
            Console.Write($"{gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
        else if (gridSpot.Status == GridSpotStatus.Hit)
            Console.Write(" X ");
        else if (gridSpot.Status == GridSpotStatus.Miss)
            Console.Write(" O ");
        else
            Console.Write(" ? ");
    }
}

static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel passivePlayer)
{
    var isValidShot = false;
    var row = "";
    var column = 0;

    do
    {
        var shot = AskForShot(activePlayer);

        try
        {
            (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
            isValidShot = GameLogic.ValidateShot(row, column, activePlayer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            isValidShot = false;
        }

        if (isValidShot == false) Console.WriteLine("Invalid shot Location");
    } while (isValidShot == false);

    var isAHit = GameLogic.IdentifyShotResult(row, column, passivePlayer);
    GameLogic.MarkShotResult(row, column, activePlayer, isAHit);
    InformationAfterTheShot(isAHit, activePlayer);
}

static void InformationAfterTheShot(bool isAHit, PlayerInfoModel activePlayer)
{
    Console.Clear();
    DisplayShotGrid(activePlayer);
    LineReturn(2);
    if (isAHit)
        Console.WriteLine("Congratulation that's a hit!");
    else Console.WriteLine("Unfortunately it's a miss ");
    LineReturn(1);
    Thread.Sleep(3000);
    Console.Clear();
}

static string AskForShot(PlayerInfoModel player)
{
    LineReturn(2);
    var output = AnsiConsole.Ask<string>($"{player.UserName} please enter your shot on this grid: ");
    return output;
}


static void MessageToWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulation to {winner.UserName} for wining");
    Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots.");
}

static void LineReturn(int number)
{
    var count = 0;
    do
    {
        Console.WriteLine();
        count += 1;
    } while (number != count);
}