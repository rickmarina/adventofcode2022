public class SolveDay4Part1
{

    public void solve()
    {

        string path = Environment.CurrentDirectory + @".\day4\input.txt";
        var lines = File.ReadAllLines(path);


        int total = 0;
        foreach (var l in lines)
        {

            var ranges = l.Split(",");

            var range1 = ranges[0].Split("-").Select(int.Parse).ToArray();
            var range2 = ranges[1].Split("-").Select(int.Parse).ToArray();

            if ((range1[0] >= range2[0] && range1[1] <= range2[1]) || (range2[0] >= range1[0] && range2[1] <= range1[1]))
                total++;

        }

        Console.WriteLine(total);
    }

}