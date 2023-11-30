class Monkey
{
    public List<long> Items = new();
    public Func<long, long> operation { get; set; }
    public int DivisibleBy { get; set; }
    public int TrueTestMonkey { get; set; }
    public int FalseTestMonkey { get; set; }
}
class Day11 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day11");
        Console.WriteLine($"=============");

        string filename = "input/day11.txt";

        var reader = new StreamReader(filename);
        string? line;
        List<Monkey> monkeys = new();
        do
        {
            line = reader.ReadLine();
            if (line == null)
            {
                break;
            }
            else if (line.StartsWith("Monkey"))
            {
                monkeys.Add(new Monkey());
            }
            else
            {
                line = line.Trim();
                Console.WriteLine($"LINE: {line}");

                var monkey = monkeys.Last();
                if (line.StartsWith("Starting"))
                {
                    string[] items = line.Split(":")[1].Split(",");
                    foreach (string item in items)
                    {
                        monkey.Items.Add(int.Parse(item));
                    }
                }
                else if (line.StartsWith("Operation"))
                {
                    string[] eq = line.Split(":")[1].Split("=")[1].Trim().Split(" ");
                    Func<long, long> operation = (long old) =>
                    {
                        long rightOperand = eq[2] == "old" ? old : long.Parse(eq[2]);
                        if (eq[1] == "+")
                        {
                            return old + rightOperand;
                        }
                        else
                        {
                            return old * rightOperand;
                        }
                    };
                    monkey.operation = operation;
                }
                else if (line.StartsWith("Test"))
                {
                    string num = line.Split(":")[1].Trim().Split(" ").Last().Trim();
                    monkey.DivisibleBy = int.Parse(num);
                }
                else if (line.StartsWith("If true"))
                {
                    string num = line.Split(":")[1].Trim().Split(" ").Last().Trim();
                    monkey.TrueTestMonkey = int.Parse(num);
                    Console.WriteLine($"IF TRUE: {monkey.TrueTestMonkey}");
                }
                else if (line.StartsWith("If false"))
                {
                    string num = line.Split(":")[1].Trim().Split(" ").Last().Trim();
                    monkey.FalseTestMonkey = int.Parse(num);
                    Console.WriteLine($"IF FALSE: {monkey.FalseTestMonkey}");
                }
            }
        } while (line != null);

        long[] inspections = new long[monkeys.Count];
        for (int round = 0; round < 10000; round++)
        {
            // if (round == 1000)
            // {
            //     // Array.Sort(inspections);
            //     // Array.Reverse(inspections);
            //     // Console.WriteLine($"{string.Join(", ", inspections)}");
            //     // Console.WriteLine($"Monkey Business: {inspections[0] * inspections[1]}");
            //     foreach (var monkey in monkeys)
            //     {
            //         Console.WriteLine($"WORRY: {string.Join(", ", monkey.Items)}");
            //     }
            //     break;
            // }
            for (int i = 0; i < monkeys.Count; i++)
            {
                var monkey = monkeys[i];
                inspections[i] += monkey.Items.Count;
                foreach (var item in monkey.Items)
                {
                    // long newWorry = monkey.operation(item) % 96577; // simple
                    long newWorry = monkey.operation(item) % 9699690;
                    if (newWorry % monkey.DivisibleBy == 0)
                    {
                        monkeys[monkey.TrueTestMonkey].Items.Add(newWorry);
                    }
                    else
                    {
                        monkeys[monkey.FalseTestMonkey].Items.Add(newWorry);
                    }
                }
                monkey.Items.Clear();
            }
        }

        Array.Sort(inspections);
        Array.Reverse(inspections);
        Console.WriteLine($"{string.Join(", ", inspections)}");
        Console.WriteLine($"Monkey Business: {inspections[0] * inspections[1]}");
    }
}
