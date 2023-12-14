class Program
{
    static void Main(string[] args)
    {
        var sr = new StreamReader(args[0]);
        List<List<char>> grid = new();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            grid.Add(line.ToList());
        }
        for (int r = 1; r < grid.Count; r++)
        {
            for (int c = 0; c < grid[r].Count; c++)
            {
                int rCursor = r;
                while (rCursor > 0 && grid[rCursor][c] == 'O' && grid[rCursor - 1][c] == '.')
                {
                    grid[rCursor - 1][c] = 'O';
                    grid[rCursor][c] = '.';
                    rCursor--;
                }
            }
        }

        long load = 0;
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                if (grid[i][j] == 'O')
                {
                    load += grid.Count - i;
                }
            }
        }

        Console.WriteLine($"Load: {load}");

    }
}