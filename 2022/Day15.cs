using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

class Day15 : IRunnable
{
    public static void Run()
    {
        Console.WriteLine($"=============");
        Console.WriteLine($"Day15");
        Console.WriteLine($"=============");

        // Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        // var reader = new StreamReader("input/day15.txt");
        var reader = new StreamReader("input/day15-simple.txt");
        string? line;
        List<Point> sensors = new();
        List<Point> beacons = new();
        while ((line = reader.ReadLine()) != null)
        {
            var iter = line.GetEnumerator();
            iter.MoveNext();
            var sensor = new Point(NextInt(iter), NextInt(iter));
            var beacon = new Point(NextInt(iter), NextInt(iter));
            sensors.Add(sensor);
            beacons.Add(beacon);
        }
        Console.WriteLine($"SENSORS: {sensors.Count}");
        Console.WriteLine($"BEACONS: {beacons.Count}");

        // scan line by line jumping by sensor dist from each beacon
        var p = new Point(0, 0);
        // int size = 4000000;
        int size = 20;
        while (true)
        {
            if (p.Y > size)
            {
                Console.WriteLine($"WENT TO FAR: {p}");
                break;
            }
            if (p.X > size)
            {
                p.X = 0;
                p.Y += 1;
            }

            bool moved = false;
            for (int i = 0; i < sensors.Count; i++)
            {
                var sensor = sensors[i];
                var beacon = beacons[i];
                // overlap
                if (Dist(sensor, p) <= Dist(sensor, beacon))
                {
                    if (p.Y % 1 == 0) Console.WriteLine($"POINT: {p} sendor={sensor} beacon={beacon}");
                    int newX = Dist(sensor, beacon) - Math.Abs(p.Y - sensor.Y);
                    if (newX == 0) continue;
                    if (sensor.X < p.X)
                    {
                        p.X += newX - (p.X - sensor.X);
                    }
                    else
                    {
                        p.X += newX + (sensor.X - p.X);
                    }
                    if (p.X >= size)
                    {
                        p.Y += 1;
                        p.X = 0;
                    }
                    moved = true;
                }
            }
            if (!moved)
            {
                foreach (var s in sensors)
                {
                    if (s.X == p.X && s.Y == p.Y)
                    {
                        p.X++;
                        moved = true;
                        break;
                    }
                }
                foreach (var b in beacons)
                {
                    if (b.X == p.X && b.Y == p.Y)
                    {
                        p.X++;
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                {
                    long res = 4000000L * p.X + p.Y;
                    Console.WriteLine($"Found: {p} res={res}");
                    // ans 1: 8249228186904 -- (x=2062306, y=4186904)
                    // ans 2: 11756170628223 -- (x=2939042, y=2628223) (too low)
                    return;
                }
            }
        }
    }

    private static int Dist(Point a, Point b)
    {
        int res = 0;
        res += Math.Abs(a.X - b.X);
        res += Math.Abs(a.Y - b.Y);
        return res;
    }

    private static int NextInt(CharEnumerator iter)
    {
        string buff = "";
        while (true)
        {
            if ((iter.Current < '0' || iter.Current > '9') && iter.Current != '-')
            {
                if (buff.Length > 0) break;
            }
            else
            {
                buff += iter.Current;
            }
            if (!iter.MoveNext())
            {
                break;
            }
        }
        return int.Parse(buff);
    }
}

class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"(x={X}, y={Y})";
    }
}