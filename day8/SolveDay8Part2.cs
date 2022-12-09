using System.Text.RegularExpressions;

public class SolveDay8Part2
{

    public void solve()
    {

        string filename = Environment.CurrentDirectory + @".\day8\input.txt";
        var lines = File.ReadAllLines(filename);


        int n = lines[0].Length;
        int m = lines.Count();
        int[,] map = new int[m, n];
        int[,] mapVisible = new int[m, n];

        int max = 0;

        for (int j = 0; j < m; j++)
        {
            for (int i = 0; i < n; i++)
            {
                map[j, i] = int.Parse(lines[j][i].ToString());
                mapVisible[j, i] = 0;
            }
        }

        printMap(map);

        //from top 
        for (int i = 1; i < n - 1; i++)
        {
            for (int j = 1; j < m - 1; j++)
            {
                int height = map[j, i];

                int row = j + 1;
                while (row <= m - 1)
                {
                    if (map[row, i] < height)
                    {
                        mapVisible[j, i]++;
                        max = Math.Max(max, mapVisible[j, i]);
                    }
                    else
                    {
                        mapVisible[j, i]++;
                        max = Math.Max(max, mapVisible[j, i]);
                        break;
                    }
                    row++;
                }
            }
        }


        //from down
        for (int i = 1; i < n - 1; i++)
        {
            for (int j = m - 2; j > 0; j--)
            {
                //Console.Write($"{j}:{i} height {map[j, i]} ->");
                int height = map[j, i];
                int row = j - 1;
                int total = 0;
                while (row >= 0)
                {
                    if (map[row, i] < height)
                    {
                        total++;
                    }
                    else
                    {
                        total++;
                        break;
                    }
                    row--;
                }
                //Console.WriteLine(total);
                mapVisible[j, i] *= total;
                max = Math.Max(max, mapVisible[j, i]);
            }
        }


        //from left 
        for (int j = 1; j < m - 1; j++)
        {
            for (int i = 1; i < n - 1; i++)
            {
                int height = map[j, i];
                int col = i + 1;
                int total = 0;
                while (col <= n - 1)
                {
                    if (map[j, col] < height)
                    {
                        total++;
                    }
                    else
                    {
                        total++;
                        break;
                    }
                    col++;
                }
                mapVisible[j, i] *= total;
                max = Math.Max(max, mapVisible[j, i]);
            }
        }

        //from right
        for (int j = 0; j < m; j++)
        {
            for (int i = n - 2; i > 0; i--)
            {
                int height = map[j, i];
                int col = i - 1;
                int total = 0;
                while (col >= 0)
                {
                    if (map[j, col] < height)
                    {
                        total++;
                    }
                    else
                    {
                        total++;
                        break;
                    }
                    col--;
                }
                mapVisible[j, i] *= total;
                max = Math.Max(max, mapVisible[j, i]);
            }
        }
        printMap(mapVisible);


        Console.WriteLine("Result: " + max);

    }

    static void printMap(int[,] map)
    {
        for (int j = 0; j <= map.GetUpperBound(0); j++)
        {
            for (int i = 0; i <= map.GetUpperBound(1); i++)
            {
                Console.Write(map[j, i]);
            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------");

    }



}