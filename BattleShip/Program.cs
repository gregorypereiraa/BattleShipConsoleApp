﻿using BattleShipLibrary.Models;
using Spectre.Console;



WelcomeMessage();
PlayerInfoModel activePlayer = CreatePlayer("Player 1");
PlayerInfoModel passivePlayer = CreatePlayer("Player 2");
PlayerInfoModel winner = null;

do
{
    DisplayShotGrid(activePlayer);
    RecordPlayerShot(activePlayer, passivePlayer);
    bool doesGameContinue = GameLogic.PlayerStillActive(passivePlayer);
    if (doesGameContinue == true)
    {
        (activePlayer, passivePlayer) = (passivePlayer, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }

} while (Winner(activePlayer,passivePlayer)==null);

winner = Winner(activePlayer, passivePlayer);
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
    Console.WriteLine($"Player information for {PlayerTitle} :");
    output.UserName = GetPlayerName();
    GameLogic.InitializationGrid(output);
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
        
        string location = AnsiConsole.Ask<string>($"Out of 5 ships, where do you want to put your ship number{ model.ShipLocation.Count+1}:? ");
        bool isValidLocation = GameLogic.PlaceShip(model, location);
        if (isValidLocation==false)
        {Console.WriteLine("That is not a valid location, please try again");}

        ;
    } while (model.ShipLocation.Count<5);
}

static PlayerInfoModel Winner(PlayerInfoModel player1, PlayerInfoModel player2)
{
    var countPlayer1 = 0;
    var countPlayer2 = 0;
    foreach (var shipPlayer1 in player1.ShipLocation)
    {
        if (shipPlayer1.Status == GridSpotStatus.Sunk)
        {
            countPlayer1 += 1;
        }
    }

    foreach (var shipPlayer2 in player2.ShipLocation)
    { if (shipPlayer2.Status == GridSpotStatus.Sunk)
        {
            countPlayer2 += 1;
        }
        
    }

    if (countPlayer1==5)
    {
        return player2;
    }
    else if (countPlayer2 == 5)
    {
        return player1;
    }

    return null;
}

static void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;
    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.SpotLetter != currentRow)
        {
            Console.WriteLine("");
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
        string shot = AskForShot();
        ( row,column) = GameLogic.SplitShotIntoRowAndColumn(shot);
        isValidShot = GameLogic.ValidateShot(row, column, activePlayer);

        if (isValidShot == false)
        {
            Console.WriteLine("Invalid shot Location");
        }
    } while (isValidShot==false);

    bool isAHit = GameLogic.IdentifyShotResult(row, column, passivePlayer);

    GameLogic.MarkShotResult(row, column, activePlayer);
}

static string AskForShot()
{
    string output = AnsiConsole.Ask<string>("Please enter your shot: ");
    return output;
}

static void MessageToWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulation to {winner.UserName} for wining");
    Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots.");
}
