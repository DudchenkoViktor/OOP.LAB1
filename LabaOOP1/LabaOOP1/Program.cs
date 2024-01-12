using System;
using System.Collections.Generic;

class GameAccount
{
    private string UserName;
    private int CurrentRating;
    private int GamesCount;
    private List<GameResult> GameHistory;
    private Random random;

    public GameAccount(string userName, int initialRating)
    {
        UserName = userName;
        CurrentRating = initialRating;
        GamesCount = 0;
        GameHistory = new List<GameResult>();
        random = new Random();
    }

    public void RecordGameResult(string opponentName, string outcome, int rating)
    {
        GamesCount++;
        if (outcome.ToLower() == "win")
        {
            CurrentRating += rating;
        }
        else if (outcome.ToLower() == "loss")
        {
            CurrentRating -= rating;
        }

        GameHistory.Add(new GameResult(opponentName, outcome, rating, GamesCount - 1));
    }

    public void GetStats()
    {
        Console.WriteLine($"Game history for {UserName}:");
        foreach (var result in GameHistory)
        {
            Console.WriteLine($"Game {result.GameIndex + 1}: Against {result.OpponentName}, {result.Outcome} with rating {result.Rating}");
        }
        Console.WriteLine($"Total games played: {GamesCount}, Current Rating: {CurrentRating}");
    }

    public void PlayGames(int numberOfGames)
    {
        for (int i = 0; i < numberOfGames; i++)
        {
            Console.Write($"Enter opponent name for game {i + 1}: ");
            string opponentName = Console.ReadLine();

            Console.Write($"Enter rating for game {i + 1}: ");
            if (int.TryParse(Console.ReadLine(), out int rating))
            {
                // Randomly determine the outcome
                string outcome = (random.Next(2) == 0) ? "Win" : "Loss";

                Console.WriteLine($"Game result for game {i + 1}: {outcome}");
                RecordGameResult(opponentName, outcome, rating);
            }
            else
            {
                Console.WriteLine($"Invalid rating. Game {i + 1} not recorded.");
            }
        }
    }

    public static GameAccount CreatePlayer()
    {
        Console.Write("Enter player name: ");
        string playerName = Console.ReadLine();

        Console.Write("Enter initial rating: ");
        if (int.TryParse(Console.ReadLine(), out int initialRating))
        {
            return new GameAccount(playerName, initialRating);
        }
        else
        {
            Console.WriteLine("Invalid initial rating. Creating player with default rating of 1000.");
            return new GameAccount(playerName, 1000);
        }
    }
}

class GameResult
{
    public string OpponentName;
    public string Outcome;
    public int Rating;
    public int GameIndex;

    public GameResult(string opponentName, string outcome, int rating, int gameIndex)
    {
        OpponentName = opponentName;
        Outcome = outcome;
        Rating = rating;
        GameIndex = gameIndex;
    }
}

class Program
{
    static void Main()
    {
        GameAccount player = GameAccount.CreatePlayer();

        Console.Write("Enter the number of games to play: ");
        if (int.TryParse(Console.ReadLine(), out int numberOfGames) && numberOfGames > 0)
        {
            player.PlayGames(numberOfGames);
            player.GetStats();
        }
        else
        {
            Console.WriteLine("Invalid number of games. Exiting.");
        }
    }
}
