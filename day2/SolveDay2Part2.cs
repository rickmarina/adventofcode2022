public class SolveDay2Part2
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

            if (round[1] == "Y")
            {
                //draw
                round[1] = round[0];
                total += 3;
            }
            else if (round[1] == "X")
            {
                //lose
                if (round[0] == "A")
                    round[1] = "C";
                else if (round[0] == "B")
                    round[1] = "A";
                else if (round[0] == "C")
                    round[1] = "B";
            }
            else if (round[1] == "Z")
            {
                //win 
                if (round[0] == "A")
                    round[1] = "B";
                else if (round[0] == "B")
                    round[1] = "C";
                else if (round[0] == "C")
                    round[1] = "A";

                total += 6;
            }

            total += scores[round[1]];
        }

        Console.WriteLine(total);
    }

}