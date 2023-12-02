using System.Runtime.Intrinsics.Arm;
using System.Transactions;

class Day16 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day16");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day16-simple.txt");
        string? line;
        List<Valve> valves = new();
        List<(string start, string[] ends)> edges = new();
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Replace(",", " ").Split(" ");
            var startV = parts[1];
            var rate = int.Parse(parts[4].Split("=")[1].TrimEnd(';'));
            var endVs = parts[9..];
            valves.Add(new Valve(startV, rate));
            edges.Add((startV, endVs));
        }
        var graph = new Graph(valves, edges);
        List<Valve> goodValves = new();
        foreach (var v in valves)
        {
            if (v.Rate > 0)
            {
                goodValves.Add(v);
            }
        }
        int[,] dist = new int[goodValves.Count, goodValves.Count];
        // Console.WriteLine($"Dist: {BFS(graph, dist, goodValves[1])}");

        Console.WriteLine($"OPEN: {goodValves.Count}");


        // Valve start = graph.Valves["AA"];
        // long res = DFS(graph, start, 30, 0);

        // Console.WriteLine($"Res: {res}");
    }

    // private static object BFS(Graph graph, int[,] dist, Valve valve)
    // {
    //     HashSet<Valve> seen = new();
    //     Queue<Valve> frontier = new();
    //     frontier.Enqueue(valve);
    // }

    /**
    time: 3
    3 -> open
    2 -> go to A
    1 -> open
    0 -> done
    */
    private static HashSet<(string start, string end)> Visited = new();
    private static Dictionary<(string valve, bool open, int time, int rate), long> cache = new();
    private static int valvesToOpen = 0;
    private static long DFS(Graph graph, Valve cur, int timeLeft, int rate)
    {
        if (valvesToOpen == 0) return timeLeft * rate;
        if (timeLeft <= 0) return 0;

        long res = 0;
        if (!cur.Open && cur.Rate > 0)
        {
            cur.Open = true;
            valvesToOpen--;
            res = DFS(graph, cur, timeLeft - 1, rate + cur.Rate) + rate;
            valvesToOpen++;
            cur.Open = false;
        }
        foreach (var n in cur.Neighbours)
        {
            var path = DFS(graph, n, timeLeft - 1, rate) + rate;
            res = Math.Max(res, path);
        }

        return Math.Max(res, (30 - timeLeft) * rate);
    }
}

class Valve
{
    public int Rate { get; private set; }
    public string Name { get; private set; }
    public bool Open { get; set; } = false;
    public List<Valve> Neighbours = new();

    public Valve(string name, int rate)
    {
        Rate = rate;
        Name = name;
    }

    public override string ToString()
    {
        return $"Name={Name} Rate={Rate}";
    }
}

class Graph
{
    public Dictionary<string, Valve> Valves = new();
    public Graph(List<Valve> valves, List<(string start, string[] ends)> edges)
    {
        foreach (var v in valves)
        {
            AddValve(v.Name, v.Rate);
        }
        foreach (var e in edges)
        {
            foreach (var end in e.ends)
            {
                if (end != "")
                    AddEdge(e.start, end);
            }
        }
    }

    public void AddValve(string name, int rate)
    {
        Valves[name] = new Valve(name, rate);
    }
    public void AddEdge(string start, string end)
    {
        Valves[start].Neighbours.Add(Valves[end]);
    }
}