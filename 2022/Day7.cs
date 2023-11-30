
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;

class INode
{
    public string Name { get; set; }
    public INode? Parent { get; set; }
    public long Size { get; set; }
    public bool IsDir { get; set; } = false;
    public Dictionary<string, INode> Children { get; set; } = new(); public INode(string name, INode? parent, long size, bool? isDir)
    {
        Name = name;
        Parent = parent;
        Size = size;
        IsDir = isDir ?? false;
    }
}

class Day7 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day 7");
        Console.WriteLine($"=============");

        var reader = new StreamReader("input/day7.txt");
        // var reader = new StreamReader("input/same7.txt");
        string? line;
        INode root = new("/", null, 0L, true);
        INode cur = root;
        List<string> cmdArgs = new();
        string command = "";
        do
        {
            line = reader.ReadLine();
            if (line != null)
            {
                if (line.StartsWith("$"))
                {
                    if (command != "")
                    {
                        cur = HandleCMD(command, cmdArgs, cur);
                    }
                    string[] parts = line.Split(" ");
                    command = parts[1];
                    cmdArgs = new();
                    if (parts.Length == 3)
                    {
                        cmdArgs.Add(parts[2]);
                    }
                }
                else
                {
                    cmdArgs.Add(line);
                }
            }
        } while (line != null);
        if (cmdArgs.Count > 0)
        {
            HandleCMD(command, cmdArgs, cur);
        }

        List<long> res = new();
        DFS(root, res);

        Console.WriteLine($"Done: {res.Sum()}"); // 1432936
        long total = 0;
        foreach (long num in res)
        {
            total += num;
        }
        // Total Space: 70000000
        // Space Needed for Updat: 30000000
        // Root Dir: 40268565
        // Free Space: 29731435
        // NEED: 268565 
        // - if file is larger than this consider it for deletion
        // - we want the smallest file >= to this

        Console.WriteLine($"TOTAL: {total}");
        Console.WriteLine($"Min File to Remvoe: {Min}");


    }
    public static long Min = long.MaxValue;
    public static int RemoveFileSize = 268565;

    private static long DFS(INode? node, List<long> res)
    {
        if (node == null)
        {
            return 0;
        }
        if (!node.IsDir)
        {
            return node.Size;
        }

        long size = 0;
        foreach (INode cur in node.Children.Values)
        {
            size += DFS(cur, res);
        }

        if (size <= 100000)
        {
            res.Add(size);
        }
        else
        {
            Console.WriteLine($"TOO BIG {node.Name} {size}");

        }

        if (size >= RemoveFileSize)
        {
            Min = Math.Min(size, Min);
        }


        return size;
    }

    private static INode HandleCMD(string command, List<string> cmdArgs, INode cur)
    {
        switch (command)
        {
            case "ls":
                foreach (string arg in cmdArgs)
                {
                    string[] parts = arg.Split(" ");
                    INode node;
                    if (arg.StartsWith("dir"))
                    {
                        node = new(parts[1], cur, 0L, true);
                    }
                    else
                    {
                        node = new(parts[1], cur, long.Parse(parts[0]), false);
                    }
                    cur.Children[node.Name] = node;
                }
                return cur;
            case "cd":
                if (cmdArgs[0] == "..")
                {
                    if (cur.Parent == null) throw new ArgumentException("Parents should not be null");
                    return cur.Parent;
                }
                else if (cmdArgs[0] == "/")
                {
                    while (cur.Name != "/" && cur.Parent != null)
                    {
                        if (cur == null || cur.Parent == null) throw new ArgumentException("We should be able to reach parent from any dir");
                        cur = cur.Parent;
                    }
                    return cur;
                }
                return cur.Children[cmdArgs[0]];
            default:
                throw new ArgumentException($"Bad command {command}");
        }
    }
}
