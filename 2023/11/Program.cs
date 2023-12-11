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
        var galaxies = FindGalaxies(grid);
        var emptyRows = EmptyRows(grid);
        var emptyCols = EmptyCols(grid);
        long res = 0;
        for (int a = 0; a < galaxies.Count - 1; a++)
        {
            for (int b = a + 1; b < galaxies.Count; b++)
            {
                res += Distance(galaxies[a], galaxies[b], emptyRows, emptyCols);
            }
        }

        Console.WriteLine($"Res: {res}");
    }

    private static HashSet<int> EmptyRows(List<List<char>> grid)
    {
        HashSet<int> empty = new();
        for (int r = 0; r < grid.Count; r++)
        {
            bool isEmpty = true;
            for (int c = 0; c < grid[0].Count; c++)
            {
                if (grid[r][c] == Galaxy)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                empty.Add(r);
            }
        }
        return empty;
    }

    private static List<Vector2> FindGalaxies(List<List<char>> grid)
    {
        List<Vector2> galaxies = new();
        for (int r = 0; r < grid.Count; r++)
        {
            for (int c = 0; c < grid[0].Count; c++)
            {
                if (grid[r][c] == Galaxy)
                {
                    galaxies.Add(new Vector2(c, r));
                }
            }
        }
        return galaxies;
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
    static long Distance(Vector2 a, Vector2 b, HashSet<int> emptyRows, HashSet<int> emptyCols)
    {
        const int Mult = 1000000;
        // const int Mult = 10;
        // count cols between
        long expandingRows = 0;
        foreach (int i in emptyRows)
        {
            if ((i > a.Y && i < b.Y) || (i > b.Y && i < a.Y))
            {
                expandingRows++;
            }
        }
        // count rows between
        long expandingCols = 0;
        foreach (int i in emptyCols)
        {
            if ((i > a.X && i < b.X) || (i > b.X && i < a.X))
            {
                expandingCols++;
            }
        }
        return (long)(Math.Abs(a.X - b.X) - expandingRows - expandingCols + expandingRows * Mult + expandingCols * Mult + Math.Abs(a.Y - b.Y));
    }
}