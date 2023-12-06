using System.Text.RegularExpressions;

class Program
{
    public static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        Regex rgx = new(@"\d+");
        var raceTime = rgx.Match(sr.ReadLine());
        var raceDist = rgx.Match(sr.ReadLine());

        var time = long.Parse(raceTime.Value);
        var dist = long.Parse(raceDist.Value);
        int waysToWin = 0;
        for (int hold = 1; hold < time; hold++)
        {
            int speed = hold;
            if ((time - hold) * speed > dist)
            {
                waysToWin++;
            }
        }
        Console.WriteLine($"DONE: {waysToWin}");
    }
}