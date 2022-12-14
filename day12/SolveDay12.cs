public class SolveDay12
{


    public void solve()
    {

        string filename = Environment.CurrentDirectory + @".\day12\input.txt";
        var lines = File.ReadAllLines(filename);


        Console.WriteLine("day12");

        Location start = new Location(0, 0);
        Location end = new Location(0, 0);

        var map = lines.Select((line, y) =>
        {
            return line.Select((val, x) =>
            {
                if (val == 'S')
                {
                    start = new Location(y, x);
                    return 0;
                }
                else if (val == 'E')
                {
                    end = new Location(y, x);
                    return 25;
                }
                else
                    return (int)val - (int)'a';
            }).ToArray();
        }).ToArray();

        Console.WriteLine("start: " + start.ToString());
        Console.WriteLine("end: " + end.ToString());

        printMap(map);

        var result = dijkstra(map, start, end);

        //Part1 
        double distance = result[end.ToKey()];
        Console.WriteLine("Distance: " + distance);

        //Part2
        var result2 = dijkstra2(map, end);
        Console.WriteLine("Distance: " + result2);
    }


    static Dictionary<string, double> dijkstra(int[][] map, Location start, Location end)
    {
        Dictionary<string, double> dist = new();
        Dictionary<string, Location> prev = new();

        Queue<string> queue = new();

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                Location loc = new Location(y, x);
                dist.Add(loc.ToKey(), double.PositiveInfinity);
                queue.Enqueue(loc.ToKey());
            }
        }

        dist[start.ToKey()] = 0;

        while (queue.Count > 0)
        {
            Location u = null;
            foreach (var current in queue)
            {
                if (u == null || dist[current] < dist[u.ToKey()])
                {

                    u = new Location(Convert.ToInt32(current.Split(",")[0]), Convert.ToInt32(current.Split(",")[1]));
                }
            }

            if (u.ToString() == end.ToString())
            {
                Console.WriteLine("end reached");
                break;
            }

            queue = new Queue<string>(queue.Where(x => x.ToString() != u.ToKey()));

            foreach (var v in getNeighbors(u.y, u.x, map))
            {
                if (queue.Contains(v.ToKey()))
                {
                    double alt = dist[u.ToKey()] + 1;
                    if (alt < dist[v.ToKey()])
                    {
                        dist[v.ToKey()] = alt;
                        prev[v.ToKey()] = u;
                    }
                }
            }
        }

        return dist;


    }



    static double dijkstra2(int[][] map, Location start)
    {
        Dictionary<string, double> dist = new();
        Dictionary<string, Location> prev = new();

        Queue<string> queue = new();

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                Location loc = new Location(y, x);
                dist.Add(loc.ToKey(), double.PositiveInfinity);
                queue.Enqueue(loc.ToKey());
            }
        }

        dist[start.ToKey()] = 0;

        while (queue.Count > 0)
        {
            Location u = null;
            foreach (var current in queue)
            {
                if (u == null || dist[current] < dist[u.ToKey()])
                {

                    u = new Location(Convert.ToInt32(current.Split(",")[0]), Convert.ToInt32(current.Split(",")[1]));
                }
            }

            if (map[u.y][u.x] == 0)
            {
                Console.WriteLine("end reached");
                return dist[u.ToKey()];
            }

            queue = new Queue<string>(queue.Where(x => x.ToString() != u.ToKey()));

            foreach (var v in getNeighbors2(u.y, u.x, map))
            {
                if (queue.Contains(v.ToKey()))
                {
                    double alt = dist[u.ToKey()] + 1;
                    if (alt < dist[v.ToKey()])
                    {
                        dist[v.ToKey()] = alt;
                        prev[v.ToKey()] = u;
                    }
                }
            }
        }

        return 0;

    }

    static IEnumerable<Location> getNeighbors(int y, int x, int[][] map)
    {
        List<Location> dirs = new List<Location>() { new Location(1, 0), new Location(0, -1), new Location(-1, 0), new Location(0, 1) };
        foreach (var dir in dirs)
        {
            Location n = new Location(y + dir.y, x + dir.x);
            if (InMap(n, map) && map[n.y][n.x] <= map[y][x] + 1)
            {
                yield return n;
            }
        }
    }

    static IEnumerable<Location> getNeighbors2(int y, int x, int[][] map)
    {
        List<Location> dirs = new List<Location>() { new Location(1, 0), new Location(0, -1), new Location(-1, 0), new Location(0, 1) };
        foreach (var dir in dirs)
        {
            Location n = new Location(y + dir.y, x + dir.x);
            if (InMap(n, map) && map[n.y][n.x] >= map[y][x] - 1)
            {
                yield return n;
            }
        }
    }

    static bool InMap(Location p, int[][] map)
    {
        int bx = map[0].GetUpperBound(0);
        int by = map.GetUpperBound(0);

        //Console.WriteLine($"checking {p.ToString()} by {by} bx {bx}");

        if (p.y >= 0 && p.y <= by && p.x >= 0 && p.x <= bx)
            return true;
        else
            return false;
    }

    static void printMap(int[][] map)
    {
        Console.WriteLine("------- map: ");

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                Console.Write(string.Format("{0, 3}", map[i][j]));
            }
            Console.WriteLine();
        }

    }

}

class Location
{
    public int x;
    public int y;

    public Location(int y, int x)
    {
        this.y = y;
        this.x = x;
    }

    public void Move(int offsetY, int offsetX)
    {
        this.y += offsetY;
        this.x += offsetX;
    }

    public override string ToString()
    {
        return $"[y,x] = {y},{x}";
    }

    public string ToKey()
    {
        return $"{y},{x}";
        //return (this.y*1000)+ this.x;
    }

}