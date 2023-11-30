class Day15 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day15");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day15-simple.txt");
        string? line;
        while ((line = reader.ReadLine()) != null)
        {

            Console.WriteLine($"{line}");


        }
        Console.WriteLine($"nic");
    }
}
