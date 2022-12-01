public class SolveDay1
{

    public void solve()
    {
        string path = Environment.CurrentDirectory + @".\day1\input1.txt";
        var lines = File.ReadAllLines(path);

        Console.WriteLine(path);

        List<int> max = new();
        int partial = 0;
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                if (max.Count < 3)
                    max.Add(partial);
                else
                {
                    int idx = max.FindIndex(0, x => x == max.Min());
                    if (partial > max[idx])
                        max[idx] = partial;
                }

                partial = 0;
            }
            else
            {
                partial += int.Parse(l);
            }
        }

        Console.WriteLine(max.Sum());
    }

}