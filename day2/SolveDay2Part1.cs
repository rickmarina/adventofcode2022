public class SolveDay2Part1
{

    public void solve()
    {
        string path = Environment.CurrentDirectory + @".\day2\input1.txt";
        var lines = File.ReadAllLines(path);

        Console.WriteLine(path);

        var scores = new Dictionary<string, int>() {
            {"A", 1},
            {"B", 2},
            {"C", 3}
        };

        int total = 0;
        foreach (var l in lines)
        {
            var round = l.Split(" ");
            round[1] = round[1].Replace("X", "A").Replace("Y", "B").Replace("Z", "C");
            if (round[0] == round[1])
            {
                total += 3;
            }
            else if (round[1] == "A" && round[0] == "C")
            {
                total += 6;
            }
            else if (round[1] == "B" && round[0] == "A")
            {
                total += 6;
            }
            else if (round[1] == "C" && round[0] == "B")
            {
                total += 6;
            }

            total += scores[round[1]];
        }

        Console.WriteLine(total);
    }

}