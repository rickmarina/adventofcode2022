public class SolveDay3Part2
{

    public void solve()
    {

        string path = Environment.CurrentDirectory + @".\day3\input.txt";
        var lines = File.ReadAllLines(path);

        int total = 0;
        List<string> group = new();

        foreach (var l in lines)
        {
            group.Add(l);

            if (group.Count == 3)
            {
                HashSet<char> c1 = new HashSet<char>(group[0]);
                HashSet<char> c2 = new HashSet<char>(group[1]);
                HashSet<char> c3 = new HashSet<char>(group[2]);

                var r = c1.Intersect(c2).Intersect(c3);

                char e = r.First();
                if (char.IsUpper(e))
                    total += (int)e - 38;
                else
                    total += (int)e - 96;

                group.Clear();

            }


        }

        Console.WriteLine(total);
    }

}