enum Shape
{
    Rock,
    Paper,
    Scissors
}
enum Outcome
{
    Win,
    Draw,
    Lose
}

class Day2Part2 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day2 Part2");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day2.txt");
        string? line;
        int totalScore = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                string[] input = line.Split(" ");
                Shape player1 = ParseShape(input[0]);
                Shape player2 = SelectShape(player1, ParseOutcome(input[1]));

                int roundScore = 0;
                roundScore += ShapeScore(player2);
                roundScore += OutcomeScore(ParseOutcome(input[1]));
                totalScore += roundScore;
            }
        } while (line != null);
        Console.WriteLine($"Total Score: {totalScore}\n"); // 12725
    }

    private static Shape SelectShape(Shape player1, Outcome expected)
    {
        if (expected == Outcome.Lose)
        {
            return player1 switch
            {
                Shape.Rock => Shape.Scissors,
                Shape.Paper => Shape.Rock,
                Shape.Scissors => Shape.Paper,
                _ => throw new Exception("Bad shape"),
            };
        }
        else if (expected == Outcome.Draw)
        {
            return player1;
        }
        else
        {
            return player1 switch
            {
                Shape.Rock => Shape.Paper,
                Shape.Paper => Shape.Scissors,
                Shape.Scissors => Shape.Rock,
                _ => throw new Exception("Bad shape"),
            };
        }
    }
    private static Shape ParseShape(string shape)
    {
        switch (shape)
        {
            case "A":
                return Shape.Rock;
            case "B":
                return Shape.Paper;
            case "C":
                return Shape.Scissors;
            default:
                throw new Exception($"Invalid shape {shape}");

        }
    }
    private static Outcome ParseOutcome(string outcome)
    {
        switch (outcome)
        {
            case "X":
                return Outcome.Lose;
            case "Y":
                return Outcome.Draw;
            case "Z":
                return Outcome.Win;
            default:
                throw new Exception($"Invalid shape {outcome}");

        }
    }
    private static int OutcomeScore(Outcome outcome)
    {
        switch (outcome)
        {
            case Outcome.Lose:
                return 0;
            case Outcome.Draw:
                return 3;
            case Outcome.Win:
                return 6;
            default:
                throw new Exception($"Invalid outcome {outcome}");
        }
    }

    public static int ShapeScore(Shape player1)
    {
        switch (player1)
        {
            case Shape.Rock:
                return 1;
            case Shape.Paper:
                return 2;
            case Shape.Scissors:
                return 3;
            default:
                return 0;
        }
    }
}
