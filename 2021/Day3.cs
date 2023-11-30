class Day3 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day3");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day3.txt");
        string? line;
        HashSet<string> oxygenGenerator = new();
        HashSet<string> co2Scrubber = new();
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {

                oxygenGenerator.Add(line);
                co2Scrubber.Add(line);
                Console.WriteLine($"{line}");
            }
        } while (line != null);
        const int BITS = 12;
        for (int i = 0; i < BITS; i++)
        {
            int mostCommon = 0;
            foreach (string bits in oxygenGenerator)
            {
                char c = bits[i];
                if (c == '1')
                {
                    mostCommon++;
                }
                else
                {
                    mostCommon--;
                }
            }
            foreach (string c in oxygenGenerator)
            {
                if (oxygenGenerator.Count == 1) break;
                if ((mostCommon > 0 && c[i] != '1') || (mostCommon < 0 && c[i] != '0') || (mostCommon == 0 && c[i] == '0'))
                {
                    oxygenGenerator.Remove(c);
                }
            }
        }
        for (int i = 0; i < BITS; i++)
        {
            int mostCommon = 0;
            foreach (string bits in co2Scrubber)
            {
                char c = bits[i];
                if (c == '1')
                {
                    mostCommon++;
                }
                else
                {
                    mostCommon--;
                }
            }
            foreach (string c in co2Scrubber)
            {
                if (co2Scrubber.Count == 1) break;
                if ((mostCommon > 0 && c[i] != '0') || (mostCommon < 0 && c[i] != '1') || (mostCommon == 0 && c[i] == '1'))
                {
                    co2Scrubber.Remove(c);
                }
            }
        }

        Console.WriteLine($"");
        Console.WriteLine($"{co2Scrubber.Last()} -- {oxygenGenerator.Last()}");

        Console.WriteLine($"Result: {convertToInt(co2Scrubber.Last()) * convertToInt(oxygenGenerator.Last())}\n");
    }

    private static int convertToInt(string v)
    {
        int res = 0;
        for (int i = 0; i < v.Length; i++)
        {
            if (v[i] == '1')
            {
                double exp = v.Length - 1 - i;
                res += (int)Math.Pow(2.0, exp);
            }
        }
        return res;
    }
}
