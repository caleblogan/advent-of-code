class Day1 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day1");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day1.txt");
        string? line;
        int maxCalories = 0;
        int currentBag = 0;
        do
        {
            line = reader.ReadLine();
            if (line == "")
            {
                maxCalories = Math.Max(maxCalories, currentBag);
                currentBag = 0;
            }
            else if (line != null)
            {
                currentBag += int.Parse(line);
            }
            // Console.WriteLine($"{line}");
        } while (line != null);
        Console.WriteLine($"Max Calories: {maxCalories}\n"); // 69528
    }
}
