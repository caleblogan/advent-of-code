

class Program
{
    const int Up = 0;
    const int Down = 1;
    const int Right = 2;
    const int Left = 3;
    static void Main(string[] args)
    {
        var sr = new StreamReader(args[0]);
        string? line;
        List<List<int>> map = new();
        while ((line = sr.ReadLine()) != null)
        {
            List<int> row = new();
            foreach (int c in line)
            {
                row.Add(c - '0');
                Console.WriteLine($"{c - '0'}");
            }
            map.Add(row);
        }
        long loss = 90000000000;
        PriorityQueue<(int r, int c, int dir, int str, int cost), int> frontier = new();
        HashSet<(int r, int c, int dir, int str)> visited = new();
        frontier.Enqueue((0, 0, Down, 0, 0), 0); // No sure on this start state
        while (frontier.Count > 0)
        {
            (int r, int c, int dir, int str, int cost) = frontier.Dequeue();
            if (visited.Contains((r, c, dir, str)))
            {
                continue;
            }
            // Console.WriteLine($"FRONTIER: {frontier.Count} {r} {c} {dir} {str} {cost} map: {map.Count} {map[0].Count}");
            if (r < 0 || r >= map.Count || c < 0 || c >= map[0].Count)
            {
                continue;
            }

            visited.Add((r, c, dir, str));
            // check goal
            // not guaranteed to be the best path... need to check que i believe?
            // possible better lower cost still on stack
            if (r == map.Count - 1 && c == map[0].Count - 1)
            {
                loss = cost;
                Console.WriteLine($"FOUND LOSS {loss}");

                break;
            }
            // min 4 blocks straight
            // max 10 blocks straight
            if (dir == Right && str < 3)
            {
                frontier.Enqueue((r, c + 1, Right, str + 1, cost + GetCost(map, r, c + 1)), cost + GetCost(map, r, c + 1));
            }
            if (dir == Left && str < 3)
            {
                frontier.Enqueue((r, c - 1, Left, str + 1, cost + GetCost(map, r, c - 1)), cost + GetCost(map, r, c - 1));
            }
            if (dir == Left || dir == Right)
            {
                frontier.Enqueue((r - 1, c, Up, 1, cost + GetCost(map, r - 1, c)), cost + GetCost(map, r - 1, c));
                frontier.Enqueue((r + 1, c, Down, 1, cost + GetCost(map, r + 1, c)), cost + GetCost(map, r + 1, c));
            }

            if (dir == Up && str < 3)
            {
                frontier.Enqueue((r - 1, c, Up, str + 1, cost + GetCost(map, r - 1, c)), cost + GetCost(map, r - 1, c));
            }
            if (dir == Down && str < 3)
            {
                frontier.Enqueue((r + 1, c, Down, str + 1, cost + GetCost(map, r + 1, c)), cost + GetCost(map, r + 1, c));
            }

            if (dir == Up || dir == Down)
            {
                frontier.Enqueue((r, c - 1, Left, 1, cost + GetCost(map, r, c - 1)), cost + GetCost(map, r, c - 1));
                frontier.Enqueue((r, c + 1, Right, 1, cost + GetCost(map, r, c + 1)), cost + GetCost(map, r, c + 1));
            }
        }
        Console.WriteLine($"Heat loss: {loss}");
    }

    private static int GetCost(List<List<int>> map, int r, int c)
    {
        if (r < 0 || r >= map.Count || c < 0 || c >= map[0].Count)
        {
            return 20;
        }
        return map[r][c];
    }
}