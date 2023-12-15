class Program
{
    static void Main(string[] args)
    {
        var sr = new StreamReader(args[0]);
        long res = 0;
        Console.WriteLine($"{Hash("qp=3")}");
        foreach (var s in sr.ReadToEnd().Split(','))
        {
            res += Hash(s);
        }

        Console.WriteLine($"RES: {res}");
    }
    public static long Hash(string s)
    {
        long res = 0;
        for (int i = 0; i < s.Length; i++)
        {
            res += (byte)s[i];
            res *= 17;
            res %= 256;
        }
        return res;
    }
}