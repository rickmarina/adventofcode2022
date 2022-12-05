public class SolveDay3Part1
{

    public void solve()
    {
        string path = Environment.CurrentDirectory + @".\day3\input.txt";
        var lines = File.ReadAllLines(path);

        int total = 0;

        foreach (var l in lines)
        {
            string c1 = l.Substring(0, l.Length / 2);
            string c2 = l.Substring(l.Length / 2);

            HashSet<char> compartment1 = new HashSet<char>(c1);
            HashSet<char> compartment2 = new HashSet<char>(c2);

            compartment1.IntersectWith(compartment2);

            char r = compartment1.Single();
            if (char.IsUpper(r))
            {
                total += (int)r - 38;
            }
            else
            {
                total += (int)r - 96;
            }

        }

        Console.WriteLine(total);
    }

}