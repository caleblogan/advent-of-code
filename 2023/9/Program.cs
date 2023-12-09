using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        var res = 0L;
        string? line;
        Regex re = new Regex(@"(-?\d)+");
        while ((line = sr.ReadLine()) != null)
        {
            var matches = re.Matches(line);
            List<long> nums = new();
            foreach (var m in matches)
            {
                nums.Add(long.Parse(m.ToString()));
            }
            res += NextNum(nums);
        }
        Console.WriteLine($"Res: {res}");

    }

    private static long NextNum(List<long> nums)
    {
        if (nums.All((long val) => val == 0))
        {
            return 0;
        }
        var newList = new List<long>();
        for (int i = 1; i < nums.Count; i++)
        {
            newList.Add(nums[i] - nums[i - 1]);
        }
        return nums.First() - NextNum(newList);
    }
}