class Day10 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day10");
        Console.WriteLine($"=============");

        string filename = "input/day10.txt";

        var reader = new StreamReader(filename);
        string? line;
        int[] crt = new int[2000];
        int cycle = 0;
        int reg = 1;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                string[] cmd = line.Split(' ');
                Console.WriteLine($"Cycle: {cycle}");

                switch (cmd[0])
                {
                    case "noop":
                        UpdateCRT(crt, cycle, reg);
                        cycle++;
                        break;
                    case "addx":
                        UpdateCRT(crt, cycle, reg);
                        cycle++;
                        UpdateCRT(crt, cycle, reg);
                        cycle++;
                        reg += int.Parse(cmd[1]);
                        break;
                    default:
                        throw new Exception($"Bad cmd -- cycle:{cycle} cmd:{cmd[0]}");
                }
            }
        } while (line != null);
        // while (cycle < 240)
        // {
        //     crt[cycle] = '@';
        // }
        RenderCRT(crt);
    }

    public static void UpdateCRT(int[] crt, int cycle, int reg)
    {
        // if (cycle >= reg - 1 && cycle <= reg + 1)
        if (cycle % 40 == reg - 1 || cycle % 40 == reg || cycle % 40 == reg + 1)
        {
            crt[cycle] = 1;
        }
    }

    public static void RenderCRT(int[] cycles)
    {
        for (int i = 0; i < 240; i++)
        {
            if (i % 40 == 0 && i != 0) Console.WriteLine($"");
            char symbol = cycles[i] == 1 ? '#' : '.';
            Console.Write(symbol);
        }
        Console.WriteLine($"");
    }
}
