using System.Transactions;

class Program
{
    /**
        | is a vertical pipe connecting north and south.
        - is a horizontal pipe connecting east and west.
        L is a 90-degree bend connecting north and east.
        J is a 90-degree bend connecting north and west.
        7 is a 90-degree bend connecting south and west.
        F is a 90-degree bend connecting south and east.
        . is ground; there is no pipe in this tile.
        S is the starting position of the animal;
    */
    private static Dictionary<char, (bool north, bool east, bool south, bool west)> Directions = new() {
        {'|', (north: true, east: false, south: true, west: false)},
        {'-', (north: false, east: true, south: false, west: true)},
        {'L', (north: true, east: true, south: false, west: false)},
        {'J', (north: true, east: false, south: false, west: true)},
        {'7', (north: false, east: false, south: true, west: true)},
        {'F', (north: false, east: true, south: true, west: false)},
        {'.', (north: false, east: false, south: false, west: false)},
        {'S', (north: true, east: true, south: true, west: true)},
    };
    static void Main(string[] args)
    {
        var sr = new StreamReader(args[0]);
        long res = 0;
        var grid = new List<List<char>>();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            grid.Add(new List<char>(line));
        }
        (int r, int c) = FindStart(grid);
        var loopCells = new HashSet<(int r, int c)>();
        int i = 0;
        while (true)
        {
            i++;
            Console.WriteLine($"rc={r},{c} sym={grid[r][c]}");
            loopCells.Add((r, c));
            var before = (r, c);
            foreach (var next in Neighbours(grid, r, c, loopCells))
            {
                if (!loopCells.Contains(next))
                {
                    r = next.r;
                    c = next.c;
                }
            }
            if (before == (r, c))
            {
                Console.WriteLine($"DONE");
                break;
            }
        }

        Console.WriteLine($"RES: {res} LoopSize={i} mid={i / 2}");
    }

    private static (int r, int c) FindStart(List<List<char>> grid)
    {
        int r = 0;
        int c = 0;
        for (r = 0; r < grid.Count; r++)
        {
            for (c = 0; c < grid[0].Count; c++)
            {
                if (grid[r][c] == 'S')
                {
                    return (r, c);
                }
            }
        }
        throw new Exception("No start found");
    }
    private static (int r, int c)[] Neighbours(List<List<char>> grid, int r, int c, HashSet<(int, int)> seen)
    {
        var res = new List<(int, int)>();
        var dir = Directions[grid[r][c]];
        if (dir.north && GetCell(grid, r - 1, c).south)
        {
            res.Add((r - 1, c));
        }
        if (dir.east && GetCell(grid, r, c + 1).west)
        {
            res.Add((r, c + 1));
        }
        if (dir.south && GetCell(grid, r + 1, c).north)
        {
            res.Add((r + 1, c));
        }
        if (dir.west && GetCell(grid, r, c - 1).east)
        {
            res.Add((r, c - 1));
        }
        return res.ToArray();
    }

    private static (bool north, bool east, bool south, bool west) GetCell(List<List<char>> grid, int r, int c)
    {
        if (r < 0 || r >= grid.Count || c < 0 || c >= grid[0].Count)
        {
            return Directions['.'];
        }
        return Directions[grid[r][c]];
    }
}



