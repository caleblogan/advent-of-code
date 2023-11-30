using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

class Day3 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day3");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day3.txt");
        string? line;
        int sumPriorities = 0;
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                Dictionary<char, int> ruck1 = GetRucksack(line);
                Dictionary<char, int> ruck2 = GetRucksack(reader.ReadLine());
                Dictionary<char, int> ruck3 = GetRucksack(reader.ReadLine());
                foreach (char key in ruck1.Keys)
                {
                    if (ruck2.ContainsKey(key) && ruck3.ContainsKey(key))
                    {
                        sumPriorities += Priority(key);
                        break;
                    }
                }
            }
        } while (line != null);
        Console.WriteLine($"Badge Sums: {sumPriorities}\n"); // 2738
    }

    public static Dictionary<char, int> GetRucksack(string? str)
    {
        str ??= "";
        var rucksack = new Dictionary<char, int>();
        for (int i = 0; i < str.Length; i++)
        {
            if (!rucksack.ContainsKey(str[i]))
            {
                rucksack[str[i]] = 0;
            }
            rucksack[str[i]]++;
        }
        return rucksack;
    }

    public static int Priority(char type)
    {
        if (type >= 'a' && type <= 'z')
        {
            return type - 'a' + 1;
        }
        else
        {
            return type - 'A' + 27;
        }
    }
}
