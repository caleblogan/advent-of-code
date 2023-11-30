using System.Runtime.ExceptionServices;

class Day6 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day 6");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day6.txt");
        string? line;
        line = reader.ReadLine();
        if (line == null) throw new Exception("BAD INPUT");
        for (int i = 0; i < line.Length - 13; i++)
        {
            var chars = new HashSet<char>();
            for (int j = i; j < i + 14; j++)
            {
                chars.Add(line[j]);
            }
            if (chars.Count == 14)
            {
                Console.WriteLine($"Found: {i + 14}"); // 3153
                break;
            }
        }
    }
}
