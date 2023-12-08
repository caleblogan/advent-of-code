using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml.Schema;

class Program
{
    static int Main(string[] args)
    {
        StreamReader sr = new(args[0]);
        string directions = sr.ReadLine();
        sr.ReadLine();
        string? line;
        var nodes = new Dictionary<string, (Node node, string left, string right)>();
        Regex re = new("[A-Z]{3}");
        while ((line = sr.ReadLine()) != null)
        {
            var matches = re.Matches(line);
            var node = new Node(matches[0].Value);
            nodes[matches[0].Value] = (node, matches[1].Value, matches[2].Value);
        }
        var aNodes = new Stack<Node>();
        foreach ((var node, var left, var right) in nodes.Values)
        {
            node.Left = nodes[left].node;
            node.Right = nodes[right].node;
            if (node.Val.EndsWith('A'))
            {
                aNodes.Push(node);
            }
        }

        var pathLengths = new Stack<long>();
        foreach (var node in aNodes)
        {
            pathLengths.Push(FindPathLength(node, directions));
        }

        while (pathLengths.Count > 1)
        {
            var a = pathLengths.Pop();
            var b = pathLengths.Pop();
            pathLengths.Push(lcm(a, b));
        }

        Console.WriteLine($"Steps: {pathLengths.Peek()}");

        return 0;
    }

    private static long FindPathLength(Node node, string directions)
    {
        int i = 0;
        int path = 0;
        Node cur = node;
        while (!cur.Val.EndsWith('Z'))
        {

            if (directions[i % directions.Length] == 'R')
            {
                cur = cur.Right;
            }
            else
            {
                cur = cur.Left;
            }
            i += 1;
            path += 1;
        }
        return path;
    }

    static long gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long lcm(long a, long b)
    {
        return (a / gcf(a, b)) * b;
    }
}

class Node
{
    public string Val { get; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    public Node(string val)
    {
        Val = val;
    }
}