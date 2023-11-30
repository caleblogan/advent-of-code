using System.Drawing;

class Day8 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day8");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day8.txt");
        string? line;
        int lineNum = 0;
        int[,] grid = new int[99, 99];
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    grid[lineNum, i] = line[i] - '0';
                }
                Console.WriteLine($"Line {lineNum}: {line}");
            }
            lineNum++;
        } while (line != null);

        Console.WriteLine($"Part 1: \n");
        int scenic = 0;
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int thisScore = ScenicScore(grid, row, col);
                scenic = Math.Max(scenic, thisScore);
            }
        }
        // Console.WriteLine($"Visible: {visible}"); // 1700
        Console.WriteLine($"Scenic Score: {scenic}");
    }

    private static int ScenicScore(int[,] grid, int row, int col)
    {
        int height = grid[row, col];
        int up = 0;
        for (int i = row - 1; i >= 0; i--)
        {
            if (grid[i, col] >= height)
            {
                up++;
                break;
            }
            up++;
        }
        int down = 0;
        for (int i = row + 1; i < grid.GetLength(0); i++)
        {
            if (grid[i, col] >= height)
            {
                down++;
                break;
            }
            down++;
        }
        int left = 0;
        for (int i = col - 1; i >= 0; i--)
        {
            if (grid[row, i] >= height)
            {
                left++;
                break;
            }
            left++;
        }
        int right = 0;
        for (int i = col + 1; i < grid.GetLength(1); i++)
        {
            if (grid[row, i] >= height)
            {
                right++;
                break;
            }
            right++;
        }
        // return (up | down | left | right) > 0 ? Math.Max(up, 1) * Math.Max(down, 1) * Math.Max(left, 1) * Math.Max(right, 1) : 0;
        // return Math.Max(up, 1) * Math.Max(down, 1) * Math.Max(left, 1) * Math.Max(right, 1);
        return up * down * left * right;
    }
}
