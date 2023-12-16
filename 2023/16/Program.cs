
using System.Security.Cryptography.X509Certificates;

class Program
{
    const int Right = 0;
    const int Left = 1;
    const int Up = 2;
    const int Down = 3;
    static void Main(string[] args)
    {

        var sr = new StreamReader(args[0]);
        List<List<char>> grid = new();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            grid.Add(line.ToList());
        }
        long maxCharged = 0;
        for (int c = 0; c < grid[0].Count; c++)
        {
            maxCharged = Math.Max(maxCharged, BFS(grid, 0, c, Down));
            maxCharged = Math.Max(maxCharged, BFS(grid, grid.Count - 1, c, Up));
        }
        for (int r = 0; r < grid.Count; r++)
        {
            maxCharged = Math.Max(maxCharged, BFS(grid, r, 0, Right));
            maxCharged = Math.Max(maxCharged, BFS(grid, r, grid[0].Count - 1, Left));
        }


        Console.WriteLine($"res: {maxCharged}");
    }

    private static long BFS(List<List<char>> grid, int startR, int startC, int startDir)
    {
        HashSet<(int r, int c, int direction)> visited = new();
        Queue<(int r, int c, int direction)> que = new();
        que.Enqueue((startR, startC, startDir));
        while (que.Count > 0)
        {
            var pos = que.Dequeue();
            if (!InBounds(grid, pos))
            {
                continue;
            }
            if (visited.Contains(pos))
            {
                continue;
            }
            visited.Add(pos);
            if (grid[pos.r][pos.c] == '.')
            {
                if (pos.direction == Right)
                {
                    que.Enqueue((pos.r, pos.c + 1, pos.direction));
                }
                else if (pos.direction == Left)
                {
                    que.Enqueue((pos.r, pos.c - 1, pos.direction));
                }
                else if (pos.direction == Up)
                {
                    que.Enqueue((pos.r - 1, pos.c, pos.direction));
                }
                else if (pos.direction == Down)
                {
                    que.Enqueue((pos.r + 1, pos.c, pos.direction));
                }
            }
            else if (grid[pos.r][pos.c] == '/')
            {
                if (pos.direction == Right)
                {
                    que.Enqueue((pos.r - 1, pos.c, Up));
                }
                else if (pos.direction == Left)
                {
                    que.Enqueue((pos.r + 1, pos.c, Down));
                }
                else if (pos.direction == Up)
                {
                    que.Enqueue((pos.r, pos.c + 1, Right));
                }
                else if (pos.direction == Down)
                {
                    que.Enqueue((pos.r, pos.c - 1, Left));
                }
            }
            else if (grid[pos.r][pos.c] == '\\')
            {
                if (pos.direction == Right)
                {
                    que.Enqueue((pos.r + 1, pos.c, Down));
                }
                else if (pos.direction == Left)
                {
                    que.Enqueue((pos.r - 1, pos.c, Up));
                }
                else if (pos.direction == Up)
                {
                    que.Enqueue((pos.r, pos.c - 1, Left));
                }
                else if (pos.direction == Down)
                {
                    que.Enqueue((pos.r, pos.c + 1, Right));
                }
            }
            else if (grid[pos.r][pos.c] == '-')
            {
                if (pos.direction == Right)
                {
                    que.Enqueue((pos.r, pos.c + 1, Right));
                }
                else if (pos.direction == Left)
                {
                    que.Enqueue((pos.r, pos.c - 1, Left));
                }
                else if (pos.direction == Up)
                {
                    que.Enqueue((pos.r, pos.c - 1, Left));
                    que.Enqueue((pos.r, pos.c + 1, Right));
                }
                else if (pos.direction == Down)
                {
                    que.Enqueue((pos.r, pos.c - 1, Left));
                    que.Enqueue((pos.r, pos.c + 1, Right));
                }
            }
            else if (grid[pos.r][pos.c] == '|')
            {
                if (pos.direction == Right)
                {
                    que.Enqueue((pos.r - 1, pos.c, Up));
                    que.Enqueue((pos.r + 1, pos.c, Down));
                }
                else if (pos.direction == Left)
                {
                    que.Enqueue((pos.r - 1, pos.c, Up));
                    que.Enqueue((pos.r + 1, pos.c, Down));
                }
                else if (pos.direction == Up)
                {
                    que.Enqueue((pos.r - 1, pos.c, Up));
                }
                else if (pos.direction == Down)
                {
                    que.Enqueue((pos.r + 1, pos.c, Down));
                }
            }
        }
        HashSet<(int r, int c)> charged = new();
        foreach (var pos in visited)
        {
            charged.Add((pos.r, pos.c));
        }

        return charged.Count;
    }

    private static bool InBounds(List<List<char>> grid, (int r, int c, int direction) pos)
    {
        if (pos.r < 0 || pos.r >= grid.Count || pos.c < 0 || pos.c >= grid[0].Count)
        {
            return false;
        }
        return true;
    }
}