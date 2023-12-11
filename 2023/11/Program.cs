using System.ComponentModel;
using System.Numerics;
using System.Reflection;

class Program
{
    public const char Galaxy = '#';
    public const char Empty = '.';

    static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        List<List<char>> grid = new();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            grid.Add(new(line));
        }

        // expand
        var expanededGrid = Expand(grid);
        List<Vector2> points = new();
        for (int r = 0; r < expanededGrid.Count; r++)
        {
            for (int c = 0; c < expanededGrid[r].Count; c++)
            {
                if (expanededGrid[r][c] == Galaxy)
                {
                    points.Add(new Vector2(c, r));
                }
                Console.Write($"{expanededGrid[r][c]}");
            }
            Console.WriteLine($"");
        }
        long res = SumShortestPaths(points);


        Console.WriteLine($"Res: {res}");
    }

    private static List<List<char>> Expand(List<List<char>> grid)
    {
        List<List<char>> res = new();
        var emptyCols = EmptyCols(grid);
        for (int r = 0; r < grid.Count; r++)
        {
            bool isEmptyRow = true;
            var newRow = new List<char>();
            for (int c = 0; c < grid[0].Count; c++)
            {
                if (emptyCols.Contains(c)) newRow.Add('.');
                if (grid[r][c] == Galaxy) isEmptyRow = false;
                newRow.Add(grid[r][c]);
            }
            res.Add(newRow);
            if (isEmptyRow)
            {
                res.Add(new());
            }
        }

        return res;
    }

    static HashSet<int> EmptyCols(List<List<char>> grid)
    {
        HashSet<int> empty = new();
        for (int c = 0; c < grid.Count; c++)
        {
            bool isEmpty = true;
            for (int r = 0; r < grid[0].Count; r++)
            {
                if (grid[r][c] == Galaxy)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                empty.Add(c);
            }
        }
        return empty;
    }

    static long SumShortestPaths(List<Vector2> points)
    {
        long res = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                res += Distance(points[i], points[j]);
            }
        }
        return res;
    }

    static long Distance(Vector2 a, Vector2 b)
    {
        return (long)(Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y));
    }
}