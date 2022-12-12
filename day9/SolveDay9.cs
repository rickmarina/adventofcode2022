using System.Text.RegularExpressions;

namespace solutions
{
    public class SolveDay9
    {

        public void solverPart2()
        {
            string filename = Environment.CurrentDirectory + @".\day9\input.txt";
            var lines = File.ReadAllLines(filename);
            List<Point> tail = new List<Point>(Enumerable.Range(1, 10).Select(x => new Point(0, 0)));

            Dictionary<string, int> pos = new();

            pos.Add("0;0", 1);

            foreach (var ins in lines)
            {
                var move = ins.Split(" ");
                int units = int.Parse(move[1]);

                Console.WriteLine(ins);

                if (move[0] == "R")
                {
                    for (int x = 0; x < units; x++)
                    {
                        tail[0].x++;

                        for (int item = 1; item < 10; item++)
                        {
                            if (spaced(tail[item - 1], tail[item]))
                            {
                                tail[item] = moveNext(tail[item], tail[item - 1]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        string k = $"{tail[9].x};{tail[9].y}";
                        if (!pos.ContainsKey(k))
                        {
                            pos.Add(k, 1);
                        }

                    }
                }

                else if (move[0] == "U")
                {
                    for (int x = 0; x < units; x++)
                    {
                        tail[0].y++;
                        for (int item = 1; item < 10; item++)
                        {
                            if (spaced(tail[item - 1], tail[item]))
                            {
                                tail[item] = moveNext(tail[item], tail[item - 1]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        string k = $"{tail[9].x};{tail[9].y}";
                        if (!pos.ContainsKey(k))
                        {
                            pos.Add(k, 1);
                        }

                    }
                }

                else if (move[0] == "L")
                {
                    for (int x = 0; x < units; x++)
                    {
                        tail[0].x--;

                        for (int item = 1; item < 10; item++)
                        {

                            if (spaced(tail[item - 1], tail[item]))
                            {
                                tail[item] = moveNext(tail[item], tail[item - 1]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        string k = $"{tail[9].x};{tail[9].y}";
                        if (!pos.ContainsKey(k))
                        {
                            pos.Add(k, 1);
                        }

                    }
                }


                else if (move[0] == "D")
                {
                    for (int x = 0; x < units; x++)
                    {
                        tail[0].y--;

                        for (int item = 1; item < 10; item++)
                        {

                            if (spaced(tail[item - 1], tail[item]))
                            {
                                tail[item] = moveNext(tail[item], tail[item - 1]);
                            }
                            else
                            {
                                break;
                            }
                        }

                        string k = $"{tail[9].x};{tail[9].y}";
                        if (!pos.ContainsKey(k))
                        {
                            pos.Add(k, 1);
                        }

                    }
                }

                Console.WriteLine($"Total tail moves: " + totalTail(pos));

                //Console.WriteLine($"H {head.x},{head.y} - T {tail.x},{tail.y}");
                //Console.ReadLine();
            }
        }
        static int totalTail(Dictionary<string, int> tail)
        {
            return tail.Keys.Count;
        }
        public void solvePart1()
        {

            string filename = Environment.CurrentDirectory + @".\day9\input.txt";
            var lines = File.ReadAllLines(filename);

            Point tail = new Point(0, 0);
            Point head = new Point(0, 0);

            Dictionary<string, int> pos = new();

            pos.Add("0;0", 1);

            foreach (var ins in lines)
            {
                var move = ins.Split(" ");
                int units = int.Parse(move[1]);

                Console.WriteLine(ins);

                if (move[0] == "R")
                {
                    for (int x = 0; x < units; x++)
                    {
                        head.x++;
                        if (spaced(head, tail))
                        {
                            if (head.y == tail.y)
                            {
                                tail.x++;
                            }
                            else
                            {
                                tail.y = head.y;
                                tail.x++;
                            }
                            string k = $"{tail.x};{tail.y}";
                            if (!pos.ContainsKey(k))
                            {
                                pos.Add(k, 1);
                            }
                        }
                    }
                }
                else if (move[0] == "U")
                {
                    for (int x = 0; x < units; x++)
                    {
                        head.y++;
                        if (spaced(head, tail))
                        {
                            if (head.x == tail.x)
                            {
                                tail.y++;
                            }
                            else
                            {
                                tail.x = head.x;
                                tail.y++;
                            }
                            string k = $"{tail.x};{tail.y}";
                            if (!pos.ContainsKey(k))
                            {
                                pos.Add(k, 1);
                            }
                        }
                    }
                }

                else if (move[0] == "L")
                {
                    for (int x = 0; x < units; x++)
                    {
                        head.x--;
                        if (spaced(head, tail))
                        {
                            if (head.y == tail.y)
                            {
                                tail.x--;
                            }
                            else
                            {
                                tail.y = head.y;
                                tail.x--;
                            }
                            string k = $"{tail.x};{tail.y}";
                            if (!pos.ContainsKey(k))
                            {
                                pos.Add(k, 1);
                            }
                        }
                    }
                }


                else if (move[0] == "D")
                {
                    for (int x = 0; x < units; x++)
                    {
                        head.y--;
                        if (spaced(head, tail))
                        {
                            if (head.x == tail.x)
                            {
                                tail.y--;
                            }
                            else
                            {
                                tail.x = head.x;
                                tail.y--;
                            }

                            string k = $"{tail.x};{tail.y}";
                            if (!pos.ContainsKey(k))
                            {
                                pos.Add(k, 1);
                            }
                        }

                    }
                }

                //Console.WriteLine($"H {head.x},{head.y} - T {tail.x},{tail.y}");
                //Console.ReadLine();
            }


            int total = pos.Keys.Count;

            Console.WriteLine(total);

        }
        static bool spaced(Point A, Point B)
        {
            return (Math.Abs(A.x - B.x) > 1 || Math.Abs(A.y - B.y) > 1);
        }
        static Point moveNext(Point p1, Point p2)
        {
            Point r = new Point(p1.x, p1.y);

            if (p2.x == p1.x)
            {
            }
            else if (p2.x > p1.x)
            {
                r.x++;
            }
            else
            {
                r.x--;
            }

            if (p2.y == p1.y)
            {
            }
            else if (p2.y > p1.y)
            {
                r.y++;
            }
            else
            {
                r.y--;
            }
            return r;
        }
        static void printTail(List<Point> tail)
        {
            for (int y = 10; y > -10; y--)
            {
                for (int x = -10; x < 10; x++)
                {
                    var item = tail.FindIndex(0, 10, e => e.x == x && e.y == y);
                    if (item > -1)
                    {
                        Console.Write(item);
                    }
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }

        }

    }

    class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"{x},{y}";
        }
    }

}