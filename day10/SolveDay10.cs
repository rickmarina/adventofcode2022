

public class SolveDay10
{

    public void solvePart2()
    {

        string filename = Environment.CurrentDirectory + @".\day10\input.txt";
        var lines = File.ReadAllLines(filename);

        int cycles = 0;
        int valX = 1;
        List<int> map = new();

        foreach (string command in lines)
        {

            if (command.Equals("noop"))
            {
                cycles++;
                map.Add(valX);
            }
            else if (command.StartsWith("addx"))
            {
                int x = Convert.ToInt32(command.Split(" ")[1].ToString());
                map.Add(valX);
                map.Add(valX);
                valX += x;
            }
        }

        List<int> freqs = new() { 20, 60, 100, 140, 180, 220 };
        int total = 0;
        foreach (var f in freqs)
        {
            Console.WriteLine($"{f}th {map[f - 1]} valx");

            total += map[f - 1] * f;
        }

        Console.WriteLine(total);

        List<string> crt = new();
        List<int> freqsCRT = new() { 40, 80, 120, 160, 200, 240 };


        string currentCRTRow = "";
        for (int i = 0; i < map.Count(); i++)
        {

            int newCycle = (i % 40) + 1;

            Console.WriteLine("Sprite position: " + map[i]);
            Console.WriteLine("Start cycle " + (i + 1));
            Console.WriteLine("During cycle CRT draws pixel in position: " + newCycle);


            if (freqsCRT.Any(x => x == i))
            {
                crt.Add(currentCRTRow);
                currentCRTRow = "";
            }

            if (newCycle >= map[i] && newCycle < map[i] + 3)
                currentCRTRow += "#";
            else
                currentCRTRow += ".";


            Console.WriteLine("Current CRT Row: " + currentCRTRow);

            //Console.ReadKey(); 

        }
        crt.Add(currentCRTRow);

        printCRT(crt);
    }
    public void solvePart1()
    {

        string filename = Environment.CurrentDirectory + @".\day10\input.txt";
        var lines = File.ReadAllLines(filename);

        int cycles = 0;
        int valX = 1;
        List<int> map = new();

        foreach (string command in lines)
        {

            if (command.Equals("noop"))
            {
                cycles++;
                map.Add(valX);
            }
            else if (command.StartsWith("addx"))
            {
                int x = Convert.ToInt32(command.Split(" ")[1].ToString());
                map.Add(valX);
                map.Add(valX);
                valX += x;
            }
        }

        List<int> freqs = new() { 20, 60, 100, 140, 180, 220 };
        int total = 0;
        foreach (var f in freqs)
        {
            Console.WriteLine($"{f}th {map[f - 1]} valx");

            total += map[f - 1] * f;
        }

        Console.WriteLine(total);
    }

    static void printCRT(List<string> crt)
    {
        Console.WriteLine("Crt:");
        foreach (var c in crt)
        {
            Console.WriteLine(c);
        }
    }

}