using System.Text.RegularExpressions;

public class SolveDay5Part1
{

    public void solve()
    {


        string path = Environment.CurrentDirectory + @".\day5\input.txt";
        var lines = File.ReadAllLines(path);

        List<Stack<char>> stacks = new List<Stack<char>>();


        stacks.Add(new Stack<char>("BVSNTCHQ"));
        stacks.Add(new Stack<char>("WDBG"));
        stacks.Add(new Stack<char>("FWRTSQB"));
        stacks.Add(new Stack<char>("LGWSZJDN"));
        stacks.Add(new Stack<char>("MPDVF"));
        stacks.Add(new Stack<char>("FWJ"));
        stacks.Add(new Stack<char>("LNQBJV"));
        stacks.Add(new Stack<char>("GTRCJQSN"));
        stacks.Add(new Stack<char>("JSQCWDM"));


        /*
        stacks.Add(new Stack<char>("ZN"));
        stacks.Add(new Stack<char>("MCD"));
        stacks.Add(new Stack<char>("P"));
        */


        Regex pat = new Regex(@"move (\d*) from (\d) to (\d)");
        int order = 0;
        foreach (var l in lines)
        {
            var m = pat.Matches(l);

            if (m.Count > 0)
            {
                order++;
                Console.WriteLine("order #" + order + " " + m[0].Value);

                int quantity = Convert.ToInt32(m[0].Groups[1].Value);
                int from = Convert.ToInt32(m[0].Groups[2].Value);
                int to = Convert.ToInt32(m[0].Groups[3].Value);

                for (int i = 0; i < quantity; i++)
                {
                    char item = stacks[from - 1].Pop();
                    stacks[to - 1].Push(item);

                }

            }
        }

        string result = "";
        foreach (var s in stacks)
        {
            if (s.Count > 0)
                result += s.Peek();
        }
        Console.WriteLine(result);
    }

}