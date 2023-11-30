class Day1Part2 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day1 Part2");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day1.txt");
        string? line;
        int[] topCarriers = new int[3];
        int currentBag = 0;
        do
        {
            line = reader.ReadLine();
            if (line == "")
            {
                int min = topCarriers.Min();
                if (currentBag > min)
                {
                    int i = Array.IndexOf(topCarriers, min);
                    topCarriers[i] = currentBag;
                }
                currentBag = 0;
            }
            else if (line != null)
            {
                currentBag += int.Parse(line);
            }
            // Console.WriteLine($"{line}");
        } while (line != null);
        Console.WriteLine($"Top 3 Calories: {topCarriers.Sum()}\n");
    }
}
