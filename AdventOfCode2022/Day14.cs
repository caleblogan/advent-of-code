using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

class Day14 : IRunnable
{
    private const int NORMALIZE_X = 250;

    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day14");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day14.txt");
        string? line;
        // build map
        var paths = new List<List<(int, int)>>();
        while ((line = reader.ReadLine()) != null)
        {
            var thisPath = new List<(int, int)>();
            var pointsStr = line.Split(" -> ");
            foreach (var p in pointsStr)
            {
                var xy = p.Split(",");
                var x = int.Parse(xy[0]) - NORMALIZE_X; // TODO: noramlize hack
                var y = int.Parse(xy[1]);
                thisPath.Add((x, y));
            }
            paths.Add(thisPath);
        }
        var floor = new List<(int, int)>();
        (int widthBefore, int heightBefore) = DeterminSize(paths);
        floor.Add((0, heightBefore + 1));
        floor.Add((widthBefore * 2, heightBefore + 1));
        paths.Add(floor);

        (int width, int height) = DeterminSize(paths);
        Console.WriteLine($"WIDTH={width} HEIGHT={height}");
        var map = new Map(width, height);
        foreach (var path in paths)
        {
            map.AddPath(path);
        }

        map.Print();

        Simulate(map);

        Console.WriteLine($"res");
    }

    private static void Simulate(Map map)
    {
        int count = 0; // Maybe an off by 1
        while (DropSand(map))
        {
            if (count % 4000 == 0)
            {
                map.Print();
            }
            count++;
        }
        map.Print();
        Console.WriteLine($"DROPPED: {count}");
        Console.WriteLine($"Finished dropping sand");
    }

    private static bool DropSand(Map map)
    {
        // What happens if there is already a piece of sand in position 500,0?
        int x = 500 - NORMALIZE_X;
        int y = -1;
        if (map.Data[0, x] == 'o')
        {
            return false;
        }
        while (true)
        {
            // out of bounds; can't place
            if (y + 1 >= map.Height)
            {
                return false;
            }
            if (map.Data[y + 1, x] == map.AIR_SYM)
            {
                y++;
            }
            else if (x - 1 >= 0 && map.Data[y + 1, x - 1] == map.AIR_SYM)
            {
                y++;
                x--;
            }
            else if (x + 1 < map.Width && map.Data[y + 1, x + 1] == map.AIR_SYM)
            {
                y++;
                x++;
            }
            else
            {
                break;
            }
        }
        map.Data[y, x] = 'o';
        return true;
    }

    public static (int width, int height) DeterminSize(List<List<(int, int)>> paths)
    {
        int maxx = int.MinValue, maxy = int.MinValue;
        foreach (var path in paths)
        {
            foreach (var p in path)
            {
                (int x, int y) = p;
                maxx = Math.Max(maxx, x);
                maxy = Math.Max(maxy, y);
            }
        }
        return (maxx + 1, maxy + 1);
    }

}

class Map
{
    public readonly char ROCK_SYM = '#';
    public readonly char AIR_SYM = '.';
    public char[,] Data;
    public int Width { get; }
    public int Height { get; }

    public Map(int width, int height)
    {
        Data = new char[height, width];
        this.Width = width;
        this.Height = height;
        Init();
    }

    private void Init()
    {
        for (int r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                Data[r, c] = AIR_SYM;
            }
        }
    }

    public void Print()
    {
        Console.WriteLine($"");
        for (int r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                Console.Write(Data[r, c]);
            }
            Console.WriteLine($"");
        }
    }

    internal void AddPath(List<(int, int)> path)
    {
        if (path.Count == 1)
        {
            (int x, int y) = path[0];
            Data[y, x] = ROCK_SYM;
            return;
        }
        // last point is part of previous connection
        for (int i = 0; i < path.Count - 1; i++)
        {
            AddConnection(path[i], path[i + 1]);
        }
    }

    // lines can either be horizontal or vertical (not diagonal)
    private void AddConnection((int x, int y) start, (int x, int y) end)
    {
        if (start.x == end.x)
        {
            AddVerticalConneciton(start, end);
        }
        else
        {
            AddHorizontalConneciton(start, end);
        }
    }

    private void AddVerticalConneciton((int x, int y) a, (int x, int y) b)
    {
        int start = Math.Min(a.y, b.y);
        int end = Math.Max(a.y, b.y);
        for (int i = start; i <= end; i++)
        {
            Data[i, a.x] = ROCK_SYM;
        }
    }
    private void AddHorizontalConneciton((int x, int y) a, (int x, int y) b)
    {
        int start = Math.Min(a.x, b.x);
        int end = Math.Max(a.x, b.x);
        for (int i = start; i <= end; i++)
        {
            Data[a.y, i] = ROCK_SYM;
        }
    }
}