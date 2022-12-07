using System.Text.RegularExpressions;

public class SolveDay6Part1
{

    public void solve()
    {
        string path = Environment.CurrentDirectory + @".\day6\input.txt";
        var lines = File.ReadAllLines(path);

        var r = FirstMaker(lines[0], 4);

        Console.WriteLine(r);
    }

    int FirstMaker(string input, int len)
    {

        int result = 0;
        for (int i = 0; i < input.Length - len; i++)
        {
            if (input.Substring(i, len).Distinct().Count() == len)
            {
                result = i + len;
                break;
            }
        }
        return result;
    }

}