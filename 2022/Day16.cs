class Day16 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day16");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day16.txt");
        string? line;
        while ((line = reader.ReadLine()) != null)
        {


        }
        Console.WriteLine($"Res: ");
    }
}

class Valve
{
    public int Rate { get; private set; }
    public List<Valve> Neighbours = new();

    public Valve(int rate)
    {
        Rate = rate;
    }
}

class Graph
{
    public Dictionary<string, Valve> Valves = new();

    public void AddValve(string name, int rate)
    {
        Valves[name] = new Valve(rate);
    }
    public void AddEdge(string start, string end)
    {
        Valves[start].Neighbours.Add(Valves[end]);
    }
}