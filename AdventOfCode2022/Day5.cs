
class Day5 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day5");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day5.txt");
        string? line;
        List<List<char>> stacks = new();
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                for (int i = 0; i < line.Length; i += 4)
                {
                    var box = line[i + 1];
                    if (box != ' ')
                    {
                        int stackIndex = i / 4;
                        while (stackIndex >= stacks.Count)
                        {
                            stacks.Add(new());
                        }
                        stacks[stackIndex].Add(box);
                    }
                }
            }
            if (line == "")
            {
                break;
            }
        } while (line != null);
        foreach (var stack in stacks)
        {
            stack.RemoveAt(stack.Count - 1);
            stack.Reverse();
            Console.WriteLine($"{string.Join(", ", stack)}");
        }
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {

                var instruction = line.Split(' ');
                var move = int.Parse(instruction[1]);
                var from = int.Parse(instruction[3]);
                var to = int.Parse(instruction[5]);
                // Console.WriteLine($"move={move} from={from} to={to}");
                for (int i = 0; i < move; i++)
                {
                    stacks[to - 1].Add(stacks[from - 1][stacks[from - 1].Count - move + i]);
                }
                for (int _ = 0; _ < move; _++)
                {
                    stacks[from - 1].RemoveAt(stacks[from - 1].Count - 1);
                }
            }
            if (line == "")
            {
                break;
            }
        } while (line != null);

        Console.WriteLine($"\n\n----------------------- RES");

        foreach (var stack in stacks)
        {
            Console.WriteLine($"{string.Join(", ", stack)}");
        }

        // Console.WriteLine($"Part 1: \n"); // SVFDLGLWV
        Console.WriteLine($"Part 2: \n"); // DCVTCVPCL
    }
}
