using System.Text;

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
            var refY = FindReflectionY(puzzle, -1);
            var refX = FindReflectionX(puzzle, -1);
            res += FindPuzzleValue(puzzle, refX, refY);

            puzzle = new();
        }
    }

    private static long FindPuzzleValue(List<List<char>> puzzle, int origRefX, int origRefY)
    {
        for (int r = 0; r < puzzle.Count; r++)
        {
            for (int c = 0; c < puzzle[0].Count; c++)
            {
                char sym = puzzle[r][c];
                puzzle[r][c] = sym == '.' ? '#' : '.';

                var refY = FindReflectionY(puzzle, origRefY) + 1;
                if (refY > 0)
                {
                    Console.WriteLine($"Ref Y: {refY}");
                    return refY;
                }

                var refX = FindReflectionX(puzzle, origRefX) + 1;
                if (refX > 0)
                {
                    Console.WriteLine($"Ref X: {refX}");
                    return refX * 100;
                }

                puzzle[r][c] = sym;
            }
        }

        return -1;
    }

    private static int FindReflectionX(List<List<char>> puzzle, int origRefX)
    {
        for (int i = 0; i < puzzle.Count - 1; i++)
        {
            if (FindReflectionXAt(puzzle, i, i + 1) && i != origRefX)
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

    private static int FindReflectionY(List<List<char>> puzzle, int origRefY)
    {
        for (int i = 0; i < puzzle[0].Count - 1; i++)
        {
            if (FindReflectionYAt(puzzle, i, i + 1) && i != origRefY)
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