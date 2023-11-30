class Day2 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day2");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day2.txt");
        string? line;
        int totalScore = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                // Console.WriteLine($"{line}");
                string[] shapes = line.Split(" ");
                string player1 = shapes[0];
                string player2 = ConvertShape(shapes[1]);
                Console.WriteLine($"{player1} {player2}");


                int roundScore = 0;
                roundScore += ShapeScore(player2);
                roundScore += OutcomeScore(player2, player1);
                totalScore += roundScore;
            }
        } while (line != null);
        Console.WriteLine($"Total Score: {totalScore}\n"); // 11603
    }

    private static string ConvertShape(string shape)
    {
        switch (shape)
        {
            case "A":
            case "X":
                return "rock";
            case "B":
            case "Y":
                return "paper";
            case "C":
            case "Z":
                return "scissors";
            default:
                throw new Exception($"Invalid shape {shape}");

        }
    }

    private static int OutcomeScore(string player1, string player2)
    {
        // Dray
        if (player1 == player2)
        {
            return 3;
        }
        // Wins
        if (
            (player1 == "rock" && player2 == "scissors") ||
            (player1 == "paper" && player2 == "rock") ||
            (player1 == "scissors" && player2 == "paper")
        )
        {
            return 6;
        }
        // Lost
        return 0;
    }

    public static int ShapeScore(string player1)
    {
        switch (player1)
        {
            case "rock":
                return 1;
            case "paper":
                return 2;
            case "scissors":
                return 3;
            default:
                return 0;
        }
    }
}
