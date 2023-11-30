class Day13 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day13");
        Console.WriteLine($"=============");

        string filename = "input/day13.txt";
        var reader = new StreamReader(filename);

        List<string> packets = new();
        string? a;
        string? b;
        do
        {
            a = reader.ReadLine();
            b = reader.ReadLine();

            if (a == null && b == null) break;
            packets.Add(a);
            packets.Add(b);
            reader.ReadLine();
        } while (true);

        for (int i = 0; i < packets.Count - 1; i++)
        {
            for (int j = 0; j < packets.Count - 1; j++)
            {
                if (InOrder(packets[j], packets[j + 1]) == -1)
                {
                    var temp = packets[j];
                    packets[j] = packets[j + 1];
                    packets[j + 1] = temp;
                }
            }
        }

        int two = 0;
        int six = 0;
        for (int i = 0; i < packets.Count; i++)
        {
            if (packets[i] == "[[2]]")
            {
                two = i + 1;
            }
            else if (packets[i] == "[[6]]")
            {
                six = i + 1;
            }
            Console.WriteLine($"{packets[i]}");
        }
        Console.WriteLine($"LEN: {packets.Count}");
        Console.WriteLine($"RES: {two * six}");

    }

    private static void PrintArray(List<string> a)
    {
        foreach (var item in a)
        {
            Console.Write($"{item}  ");
        }
        Console.WriteLine($"");
    }

    // [[4,4],4,4]
    private static List<string> ToArray(string? a)
    {
        List<string> res = new();
        if (a == null || a == "") return res;
        if (!a.Contains('['))
        {
            res.Add(a);
            return res;
        }
        int i = 1;
        while (i < a.Length - 1)
        {
            if (a[i] == ',')
            {
                i++;
            }
            else if (a[i] == '[')
            {
                string elem = "[";
                int left = 1;
                while (left > 0)
                {
                    i++;
                    elem += a[i];
                    if (a[i] == ']') left--;
                    else if (a[i] == '[') left++;
                }
                i++;
                res.Add(elem);
            }
            else
            {
                string buff = "";
                while (i < a.Length - 1 && a[i] != ',')
                {
                    buff += a[i];
                    i++;
                }
                res.Add(buff);
                i++;
            }
        }

        return res;
    }

    private static int InOrder(string? a, string? b)
    {
        var left = ToArray(a);
        var right = ToArray(b);
        PrintArray(left);
        PrintArray(right);
        Console.WriteLine($"");
        for (int i = 0; i < left.Count; i++)
        {
            Console.WriteLine($"left: {left[i]}");

            if (i >= right.Count)
            {
                Console.WriteLine($"LEFT: {left[0]} {left.Count}");

                Console.WriteLine($"RIGHT {i}");

                return -1;
            }
            else if (!left[i].Contains('[') && !right[i].Contains('['))
            {
                int iLeft = int.Parse(left[i]);
                int iRight = int.Parse(right[i]);
                if (iLeft < iRight)
                {
                    return 1;
                }
                else if (iLeft > iRight)
                {
                    return -1;
                }
            }
            else
            {
                var v = InOrder(left[i], right[i]);
                if (v != 0) return v;
            }
        }

        if (left.Count < right.Count) return 1;

        return 0;
    }
}
