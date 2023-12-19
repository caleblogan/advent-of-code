using System.ComponentModel;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(args[0]);
        string? line;
        Dictionary<string, List<(char id, char comp, int compTarg, string dest)>> workflows = new();
        while ((line = sr.ReadLine()) != null && line != "")
        {
            var segments = line.TrimEnd('}').Split("{");
            string label = segments[0].Trim();
            string[] rules = segments[1].Split(",");
            workflows[label] = new();
            int i = 0;
            for (; i < rules.Length - 1; i++)
            {
                string dest = rules[i].Split(':')[1];
                char id = rules[i][0];
                char comp = rules[i][1];
                int compTarg = int.Parse(rules[i].Split(':')[0][2..]);
                workflows[label].Add((id, comp, compTarg, dest));
            }
            workflows[label].Add(('\0', '\0', 0, rules[i]));
        }

        List<(int x, int m, int a, int s)> parts = new();
        long res = 0;

        while ((line = sr.ReadLine()) != null)
        {
            line = line.Trim('{');
            line = line.Trim('}');
            line = line.Replace("x=", "");
            line = line.Replace("m=", "");
            line = line.Replace("a=", "");
            line = line.Replace("s=", "");
            var segs = line.Split(",");
            parts.Add((int.Parse(segs[0]), int.Parse(segs[1]), int.Parse(segs[2]), int.Parse(segs[3])));
        }
        foreach (var p in parts)
        {
            // Console.WriteLine($"x={p.x}, m={p.m}, a={p.a}, s={p.s}");
            string cur = "in";
            while (true)
            {
                if (cur == "A")
                {
                    res += p.x + p.m + p.a + p.s;
                    // Console.WriteLine($"ACCepted: {p}");
                    break;
                }
                if (cur == "R")
                {
                    // Console.WriteLine($"Rejected: {p}");
                    break;
                }
                cur = NextDest(workflows, p, cur);
            }
        }

        Console.WriteLine($"Res: {res}");
    }

    private static string NextDest(Dictionary<string, List<(char id, char comp, int compTarg, string dest)>> workflows, (int x, int m, int a, int s) p, string cur)
    {
        foreach (var r in workflows[cur])
        {
            switch (r.id)
            {
                case 'x':
                    if (Compare(p.x, r.comp, r.compTarg))
                        return r.dest;
                    break;
                case 'm':
                    if (Compare(p.m, r.comp, r.compTarg))
                        return r.dest;
                    break;
                case 'a':
                    if (Compare(p.a, r.comp, r.compTarg))
                        return r.dest;
                    break;
                case 's':
                    if (Compare(p.s, r.comp, r.compTarg))
                        return r.dest;
                    break;
                case '\0':
                    return r.dest;
            }
        }
        throw new Exception("No matching rule found");
    }

    private static bool Compare(int x, char comp, int compTarg)
    {
        if (comp == '>')
            return x > compTarg;
        return x < compTarg;
    }
}