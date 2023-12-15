class Lens
{
    public string Label { get; set; }
    public int FocalLen { get; set; }
    public Lens(string label, int focalLen)
    {
        Label = label;
        FocalLen = focalLen;
    }
}
class Program
{
    static void Main(string[] args)
    {
        var sr = new StreamReader(args[0]);
        Console.WriteLine($"{Hash("qp=3")}");
        Lens[,] boxes = new Lens[256, 256];
        foreach (var s in sr.ReadToEnd().Split(','))
        {
            if (s.Contains('='))
            {
                string[] parts = s.Split('=');
                string label = parts[0];
                int focalLen = int.Parse(parts[1]);
                var lens = new Lens(label, focalLen);
                var hash = Hash(label);
                for (int i = 0; i < boxes.GetLength(0); i++)
                {
                    if (boxes[hash, i] == null)
                    {
                        boxes[hash, i] = lens;
                        break;
                    }
                    if (boxes[hash, i].Label == label)
                    {
                        boxes[hash, i] = lens;
                        break;
                    }
                }

            }
            else
            {
                string label = s[0..(s.Length - 1)];
                var hash = Hash(label);
                for (int i = 0; i < boxes.GetLength(0); i++)
                {
                    if (boxes[hash, i] == null) break;
                    if (boxes[hash, i].Label == label)
                    {
                        for (int j = i; j < boxes.GetLength(1) - 1; j++)
                        {
                            boxes[hash, j] = boxes[hash, j + 1];
                        }
                        break;
                    }
                }
            }
        }

        long res = 0;
        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 0; j < boxes.GetLength(1); j++)
            {
                if (boxes[i, j] != null)
                {
                    long power = (1 + i) * (1 + j) * boxes[i, j].FocalLen;
                    res += power;
                }
                else { break; }
            }
        }

        Console.WriteLine($"RES: {res}");
    }
    public static int Hash(string s)
    {
        long res = 0;
        for (int i = 0; i < s.Length; i++)
        {
            res += (byte)s[i];
            res *= 17;
            res %= 256;
        }
        return (int)res;
    }
}