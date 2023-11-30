class Day2 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day2");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day2.txt");
        string? line;
        int horizontal = 0;
        int depth = 0;
        int aim = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                Console.WriteLine($"{line}");
                string[] args = line.Split(" ");
                string cmd = args[0];
                int x = int.Parse(args[1]);
                switch (cmd)
                {
                    case "forward":
                        horizontal += x;
                        depth += aim * x;
                        break;
                    case "down":
                        aim += x;
                        break;
                    case "up":
                        aim -= x;
                        break;
                }
            }
        } while (line != null);
        Console.WriteLine($"Result: {horizontal * depth}\n");
    }
}
