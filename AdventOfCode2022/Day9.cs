using System.ComponentModel.DataAnnotations;
using System.Data;

class Day9 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day9");
        Console.WriteLine($"=============");

        string filename = "input/day9.txt";

        var reader = new StreamReader(filename);
        string? line;
        int left = 0;
        int right = 0;
        int up = 0;
        int down = 0;
        int posx = 0;
        int posy = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                string[] cmd = line.Split(' ');
                int offset = int.Parse(cmd[1]);
                switch (cmd[0])
                {
                    case "U":
                        posy -= offset;
                        up = Math.Min(up, posy);
                        break;
                    case "D":
                        posy += offset;
                        down = Math.Max(down, posy);
                        break;
                    case "L":
                        posx -= offset;
                        left = Math.Min(left, posx);
                        break;
                    case "R":
                        posx += offset;
                        right = Math.Max(right, posx);
                        break;
                }
            }
        } while (line != null);

        posx = Math.Abs(left);
        posy = Math.Abs(up);
        char[,] graph = new char[Math.Abs(up) + down + 1, Math.Abs(left) + right + 1];
        graph[posy, posx] = 's';
        Console.WriteLine($"Rows:{graph.GetLength(0)} Cols:{graph.GetLength(1)}");
        Console.WriteLine($"left:{left} right:{right} up:{up} down:{down} {posx} {posy}");

        reader = new StreamReader(filename);
        int[,] knots = new int[10, 2];
        for (int i = 0; i < knots.GetLength(0); i++)
        {
            knots[i, 0] = posy;
            knots[i, 1] = posx;
        }
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                string[] cmd = line.Split(' ');
                int offset = int.Parse(cmd[1]);
                Console.WriteLine($"Line: {cmd[0]} {offset}");

                switch (cmd[0])
                {
                    case "U":
                        for (int i = 1; i <= offset; i++)
                        {
                            knots[0, 0]--;
                            for (int j = 1; j < knots.GetLength(0); j++)
                            {
                                UpdateTail(knots, j);
                            }
                            graph[knots[9, 0], knots[9, 1]] = '#';
                        }
                        break;
                    case "D":
                        for (int i = 1; i <= offset; i++)
                        {
                            knots[0, 0]++;
                            for (int j = 1; j < knots.GetLength(0); j++)
                            {
                                UpdateTail(knots, j);
                            }
                            graph[knots[9, 0], knots[9, 1]] = '#';

                        }
                        break;
                    case "L":
                        for (int i = 1; i <= offset; i++)
                        {
                            knots[0, 1]--;
                            for (int j = 1; j < knots.GetLength(0); j++)
                            {
                                UpdateTail(knots, j);
                            }
                            graph[knots[9, 0], knots[9, 1]] = '#';

                        }
                        break;
                    case "R":
                        for (int i = 1; i <= offset; i++)
                        {
                            knots[0, 1]++;
                            for (int j = 1; j < knots.GetLength(0); j++)
                            {
                                UpdateTail(knots, j);
                            }
                            graph[knots[9, 0], knots[9, 1]] = '#';

                        }
                        break;
                }
            }
        } while (line != null);

        PrintGraph(graph);
        Console.WriteLine($"Part 1: \n");
        int count = 0;
        for (int row = 0; row < graph.GetLength(0); row++)
        {
            for (int col = 0; col < graph.GetLength(1); col++)
            {
                if (graph[row, col] == '#') count++;
            }
        }
        Console.WriteLine($"Count: {count}");

    }

    private static void UpdateTail(int[,] knots, int i)
    {
        int posy = knots[i - 1, 0];
        int posx = knots[i - 1, 1];
        int ydiff = posy - knots[i, 0];
        int xdiff = posx - knots[i, 1];
        if (xdiff == 0 && ydiff < -1)
        {
            knots[i, 0]--;
        }
        else if (xdiff == 0 && ydiff > 1)
        {
            knots[i, 0]++;
        }
        else if (xdiff < -1 && ydiff == 0)
        {
            knots[i, 1]--;
        }
        else if (xdiff > 1 && ydiff == 0)
        {
            knots[i, 1]++;
        }
        else if ((ydiff < -1 && xdiff >= 1) || (ydiff <= -1 && xdiff > 1))
        { // top right
            knots[i, 0]--;
            knots[i, 1]++;
        }
        else if ((ydiff > 1 && xdiff >= 1) || (ydiff >= 1 && xdiff > 1))
        { // bottom right
            knots[i, 0]++;
            knots[i, 1]++;
        }
        else if ((ydiff > 1 && xdiff <= -1) || (ydiff >= 1 && xdiff < -1))
        { // bottom left 
            knots[i, 0]++;
            knots[i, 1]--;

        }
        else if ((ydiff < -1 && xdiff <= -1) || (ydiff <= -1 && xdiff < -1))
        { // top left
            knots[i, 0]--;
            knots[i, 1]--;
        }
    }

    public static void PrintGraph(char[,] graph)
    {
        for (int row = 0; row < graph.GetLength(0); row++)
        {
            for (int col = 0; col < graph.GetLength(1); col++)
            {
                char myChar = graph[row, col] != '\0' ? graph[row, col] : '.';
                Console.Write($"{myChar} ");
            }
            Console.WriteLine($"");
        }
    }

}
