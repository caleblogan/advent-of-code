



class Day12 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day12");
        Console.WriteLine($"=============");

        string filename = "input/day12.txt";
        var reader = new StreamReader(filename);

        List<List<char>> map = new();

        string? line;
        while ((line = reader.ReadLine()) != "" && line != null)
        {
            List<char> row = new();
            foreach (char c in line) row.Add(c);
            map.Add(row);
        }
        PrintMap(map);

        int min = int.MaxValue;
        for (int r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[0].Count; c++)
            {
                if (map[r][c] == 'a')
                {
                    int steps = BFS(map, r, c);
                    min = Math.Min(min, steps);
                }
            }
        }

        Console.WriteLine($"Min path: {min}");
    }

    private static int BFS(List<List<char>> map, int sr, int sc)
    {
        // Assumes rules still find shortest path
        Queue<((int, int), int)> frontier = new();
        HashSet<(int, int)> visited = new();
        frontier.Enqueue(((sr, sc), 0));
        while (frontier.Count > 0)
        {
            (var next, int cost) = frontier.Dequeue();
            if (visited.Contains(next)) continue;
            visited.Add(next);
            (int r, int c) = next;
            if (map[r][c] == 'E')
            {
                Console.WriteLine($"FOUND: E at ({r},{c}) cost={cost}");
                return cost;
            }
            if (ValidStep(map, (r, c), (r - 1, c)))
            {
                frontier.Enqueue(((r - 1, c), cost + 1));
            }
            if (ValidStep(map, (r, c), (r, c + 1)))
            {
                frontier.Enqueue(((r, c + 1), cost + 1));
            }
            if (ValidStep(map, (r, c), (r + 1, c)))
            {
                frontier.Enqueue(((r + 1, c), cost + 1));
            }
            if (ValidStep(map, (r, c), (r, c - 1)))
            {
                frontier.Enqueue(((r, c - 1), cost + 1));
            }
        }
        return int.MaxValue;
    }

    private static (int, int) FindStart(List<List<char>> map, char targ)
    {
        for (int r = 0; r < map.Count; r++)
        {
            for (int c = 0; c < map[0].Count; c++)
            {
                if (map[r][c] == targ) return (r, c);
            }
        }
        throw new Exception("Couldn't find start");
    }

    private static bool ValidStep(List<List<char>> map, (int, int) start, (int, int) end)
    {
        (int sr, int sc) = start;
        (int er, int ec) = end;
        if (er < 0 || er >= map.Count || ec < 0 || ec >= map[0].Count) return false;

        char startChar = map[sr][sc];
        if (startChar == 'S') startChar = 'a';
        char endChar = map[er][ec];
        if (endChar == 'E') endChar = 'z';
        Console.WriteLine($"s={startChar} e={endChar} {endChar - startChar}");

        return (byte)endChar - (byte)startChar < 2;
    }


    private static void PrintMap(List<List<char>> map)
    {
        foreach (var row in map)
        {
            Console.WriteLine($"{string.Join("", row.ToArray())}");
        }
    }
}
