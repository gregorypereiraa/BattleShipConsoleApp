using BattleShipLibrary.Models;
using Spectre.Console;
WelcomeMessage();

 static void WelcomeMessage()
{
    Console.WriteLine("*****************************");
    Console.WriteLine("Welcome to the BattleShip App");
    Console.WriteLine("Created by Gregory");
    Console.WriteLine("*****************************");
    Console.WriteLine();
}

static PlayerInfoModel CreatePlayer()
{
    PlayerInfoModel output = new PlayerInfoModel();
    output.UserName = GetPlayerName();
    GameLogic.InitializationGrid(output);

    return output;
}

static string? GetPlayerName()
{
    string ouptut = AnsiConsole.Ask<string>("What is your name: ");
    return ouptut;
}

