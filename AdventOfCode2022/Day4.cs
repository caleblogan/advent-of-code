
class Day4 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day4");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day4.txt");
        string? line;
        int completeOverlaps = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                string[] ranges = line.Split(",");
                int[] r1 = Array.ConvertAll(ranges[0].Split("-"), s => int.Parse(s));
                int[] r2 = Array.ConvertAll(ranges[1].Split("-"), s => int.Parse(s));
                if (
                    (r1[1] >= r2[0] && r1[0] <= r2[1])
                )
                {
                    completeOverlaps++;
                }
            }
        } while (line != null);
        // Console.WriteLine($"Complete Overlaps Part 1: {completeOverlaps}\n"); // 441
        Console.WriteLine($"Complete Overlaps Part 2: {completeOverlaps}\n"); // 861
    }
}
