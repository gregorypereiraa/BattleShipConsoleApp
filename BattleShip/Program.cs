using System.Collections.Specialized;
using BattleShipLibrary.Models;
using Spectre.Console;



WelcomeMessage();
PlayerInfoModel activePlayer = CreatePlayer("Player 1");
Console.Clear();
PlayerInfoModel Opponent = CreatePlayer("Player 2");
PlayerInfoModel winner = null;

do
{
    DisplayShotGrid(activePlayer);
    RecordPlayerShot(activePlayer, Opponent);
    bool doesGameContinue = GameLogic.PlayerStillActive(Opponent);
    
    
    
    if (doesGameContinue == true)
    {
        (activePlayer, Opponent) = (Opponent, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }

} while (winner==null);

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
    PlayerInfoModel output = new PlayerInfoModel();
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
    string ouptut = AnsiConsole.Ask<string>("What is your name: ");
    return ouptut;
}

static void PlaceShips(PlayerInfoModel model)
{
    do
    {
        
        string location = AnsiConsole.Ask<string>($"Out of 5 ships, where do you want to put your ship number{ model.ShipLocation.Count+1}:?");
        bool isValidLocation = false;
        try
        {
            isValidLocation = GameLogic.PlaceShip(model, location);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        if (isValidLocation==false)
        {Console.WriteLine("That is not a valid location, please try again");}

        ;
    } while (model.ShipLocation.Count<2);
}

static void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;
    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.SpotLetter != currentRow)
        {
            LineReturn(1);
            currentRow = gridSpot.SpotLetter;
        }
        if (gridSpot.Status == GridSpotStatus.Empty)
        {
            Console.Write($"{gridSpot.SpotLetter }{gridSpot.SpotNumber } ");
        }
        else if (gridSpot.Status == GridSpotStatus.Hit)
        {
            Console.Write(" X ");
        }
        else if (gridSpot.Status == GridSpotStatus.Miss)
        {
            Console.Write(" O ");
        }
        else
        {
            Console.Write(" ? ");
        }
    }
}

static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel passivePlayer)
{
    bool isValidShot = false;
    string row = "";
    int column = 0;
    
    do
    {
        string shot = AskForShot(activePlayer);

        try
        {
            ( row,column) = GameLogic.SplitShotIntoRowAndColumn(shot);
            isValidShot = GameLogic.ValidateShot(row, column, activePlayer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            isValidShot = false;

        }

        if (isValidShot == false)
        {
            Console.WriteLine("Invalid shot Location");
        }
    } while (isValidShot==false);

    bool isAHit = GameLogic.IdentifyShotResult(row, column, passivePlayer);
    GameLogic.ShipSank(passivePlayer,row,column);
    GameLogic.MarkShotResult(row, column, activePlayer, isAHit);
    InformationAfterTheShot(isAHit,activePlayer);
}

static void InformationAfterTheShot(bool isAHit,PlayerInfoModel activePlayer)
{
    Console.Clear();
    DisplayShotGrid(activePlayer);
    LineReturn(2);
    if (isAHit)
    {
        Console.WriteLine("Congratulation that's a hit!");
    }else Console.WriteLine("Unfortunately it's a miss ");
    LineReturn(1);
    Thread.Sleep(3000);
    Console.Clear();
}

static string AskForShot(PlayerInfoModel player)
{
    LineReturn(2);
    string output = AnsiConsole.Ask<string>($"{player.UserName} please enter your shot on this grid: ");
    return output;
}


static void MessageToWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulation to {winner.UserName} for wining");
    Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots.");
}

static void LineReturn(int number)
{
    int count = 0;
    do
    {
        Console.WriteLine();
        count += 1;
    } while (number!=count);
}