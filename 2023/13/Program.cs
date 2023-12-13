class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        long res = 0;
        List<List<char>> puzzle = new();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (line != "")
            {
                puzzle.Add(new List<char>(line));
                continue;
            }
            var refY = FindReflectionY(puzzle);
            Console.WriteLine($"Ref Y: {refY}");
            if (refY > -1) res += refY + 1;

            var refX = FindReflectionX(puzzle);
            Console.WriteLine($"Ref X: {refX}");
            if (refX > -1) res += (refX + 1) * 100;

            puzzle = new();
        }
        Console.WriteLine($"Res: {res}");
    }

    private static int FindReflectionX(List<List<char>> puzzle)
    {
        for (int i = 0; i < puzzle.Count - 1; i++)
        {
            if (FindReflectionXAt(puzzle, i, i + 1))
            {
                return i;
            }
        }
        return -1;
    }

    private static bool FindReflectionXAt(List<List<char>> puzzle, int i, int j)
    {
        if (i < 0 || j >= puzzle.Count) { return true; }
        for (int col = 0; col < puzzle[0].Count; col++)
        {
            if (puzzle[i][col] != puzzle[j][col]) return false;
        }
        return FindReflectionXAt(puzzle, i - 1, j + 1);
    }

    private static int FindReflectionY(List<List<char>> puzzle)
    {
        for (int i = 0; i < puzzle[0].Count - 1; i++)
        {
            if (FindReflectionYAt(puzzle, i, i + 1))
            {
                return i;
            }
        }
        return -1;
    }

    private static bool FindReflectionYAt(List<List<char>> puzzle, int i, int j)
    {
        if (i < 0 || j >= puzzle[0].Count) { return true; }
        for (int row = 0; row < puzzle.Count; row++)
        {
            if (puzzle[row][i] != puzzle[row][j]) return false;
        }
        return FindReflectionYAt(puzzle, i - 1, j + 1);
    }
}