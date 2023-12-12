

class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        long res = 0;
        string? line;
        int i = 1;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(" ");
            List<char> conditions = new List<char>(parts[0]);
            List<int> ranges = new();
            foreach (string c in parts[1].Split(","))
            {
                ranges.Add(int.Parse(c));
            }
            // Console.WriteLine($"Line: {i}  --  {parts[1]}");

            res += Arrangements(conditions, ranges);
            // Console.WriteLine($"\n");

            i++;
        }

        Console.WriteLine($"RES: {res}");
    }

    private const char Damaged = '#';
    private const char Operational = '.';
    private const char Unknown = '?';
    // Contiguous range groups represent the order in which they appear
    private static long Arrangements(List<char> conditions, List<int> ranges, int i = 0)
    {
        if (i >= conditions.Count)
        {
            return IsValidArrangement(conditions, ranges) ? 1 : 0;
        }
        if (conditions[i] == Damaged)
        {
            return Arrangements(conditions, ranges, i + 1);
        }
        else if (conditions[i] == Operational)
        {
            return Arrangements(conditions, ranges, i + 1);
        }
        else if (conditions[i] == Unknown)
        {
            long res = 0;
            conditions[i] = '#';
            res += Arrangements(conditions, ranges, i + 1);
            conditions[i] = '.';
            res += Arrangements(conditions, ranges, i + 1);
            conditions[i] = '?';
            return res;
        }
        throw new Exception("Invlaid symobl");
    }

    private static bool IsValidArrangement(List<char> conditions, List<int> ranges)
    {
        int i = 0;
        int j = 0;
        int buffer = 0;
        while (i < conditions.Count)
        {
            if (conditions[i] == Operational)
            {
                if (buffer > 0)
                {
                    if (j >= ranges.Count || buffer != ranges[j])
                    {
                        return false;
                    }
                    j++;
                    buffer = 0;
                }
            }
            else
            {
                buffer++;
            }
            i++;
        }
        if (buffer > 0 && j < ranges.Count && buffer == ranges[j])
        {
            j++;
            buffer = 0;
        }
        if (j != ranges.Count || buffer > 0) return false;
        // Console.WriteLine($"{string.Join("", conditions)}");

        return true;
    }
}