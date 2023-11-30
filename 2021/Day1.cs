class Day1 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day1");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day1.txt");
        string? line;
        int increasing = 0;
        List<int> heights = new();
        int sum = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                int curHeight = int.Parse(line);
                if (heights.Count < 3)
                {
                    heights.Add(curHeight);
                    sum += curHeight;
                }
                else
                {
                    int prevHeight = sum;
                    sum -= heights[0];
                    sum += curHeight;
                    heights.RemoveAt(0);
                    heights.Add(curHeight);
                    if (prevHeight < sum)
                    {
                        increasing++;
                    }
                }
            }
        } while (line != null);
        Console.WriteLine($"Increasing: {increasing}\n");
    }
}
