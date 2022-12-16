using System.Text.Json;

public class SolveDay13
{

    public void solve()
    {


        string filename = Environment.CurrentDirectory + @".\day13\input.txt";
        var lines = File.ReadAllLines(filename);

        List<Group> grupos = new();

        for (int i = 0; i < lines.Length; i += 3)
        {
            grupos.Add(new Group(lines[i], lines[i + 1]));
        }


        //Part 1 : 6478
        Console.WriteLine("groups #: " + grupos.Count);

        var result = grupos.Select((x, index) =>
        {
            int r = compareOrder(x.left, x.right, 0);
            Console.WriteLine(r);
            return r == 1 ? index + 1 : 0;

        }).Sum();

        Console.WriteLine("result: " + result);

        //Part 2 
        List<string> packets = new List<string>();
        packets.AddRange(lines.Where(x => !string.IsNullOrEmpty(x)).Select(x => x));
        packets.Add("[[2]]");
        packets.Add("[[6]]");

        packets.Sort((a, b) =>
        {
            return compareOrder(JsonSerializer.Deserialize<List<object>>(a), JsonSerializer.Deserialize<List<object>>(b), 0);
        });
        packets.Reverse();

        Console.WriteLine("print sorted:");
        foreach (var item in packets)
        {
            Console.WriteLine(item);
        }

        int resultpart2 = (packets.IndexOf("[[2]]") + 1) * (packets.IndexOf("[[6]]") + 1);
        Console.WriteLine(resultpart2);

    }


    //1: right order
    //-1: wrong order
    //0: no order
    static int compareOrder(List<object> left, List<object> right, int index)
    {

        Console.WriteLine($"Compare {string.Join(",", left)} vs {string.Join(",", right)} index {index}");

        //Console.ReadKey();

        if (index > left.Count - 1 && index <= right.Count - 1)
        {
            Console.WriteLine("los elementos de la izquierda se acabaron antes");
            return 1; //los elementos de la izquierda se acabaron antes 
        }
        if (index > right.Count - 1 && index <= left.Count - 1)
        {
            Console.WriteLine("los elementos de la derecha se acabaron antes");
            return -1; //los elementos de la derecha se acabaron antes 
        }
        if (index > left.Count - 1 && index > right.Count - 1)
        {
            Console.WriteLine("los elementos se acabaron por igual");
            return 0; // se acabaron por igual
        }

        if (left[index].ToString()[0] != '[' && right[index].ToString()[0] != '[')
        { //son números
            if (int.Parse(left[index].ToString()) < int.Parse(right[index].ToString()))
            {
                return 1;
            }
            else if (int.Parse(left[index].ToString()) > int.Parse(right[index].ToString()))
            {
                return -1;
            }
        }

        if (left[index].ToString()[0] == '[' && right[index].ToString()[0] == '[')
        { //L y R son listas
            Console.WriteLine($"L: {left[index]} - R: {right[index]} are lists");
            var listLeft = JsonSerializer.Deserialize<List<object>>(left[index].ToString());
            var listRight = JsonSerializer.Deserialize<List<object>>(right[index].ToString());
            int c = compareOrder(listLeft, listRight, 0);
            if (c == 0)
                return compareOrder(left, right, index + 1);
            else
                return c;
        }
        else if (left[index].ToString()[0] == '[' && right[index].ToString()[0] != '[')
        { // R no es lista
            Console.WriteLine("convertimos R en lista");

            var listLeft = JsonSerializer.Deserialize<List<object>>(left[index].ToString());
            var listRight = JsonSerializer.Deserialize<List<object>>("[" + right[index].ToString() + "]");

            return compareOrder(listLeft, listRight, 0);
        }
        else if (left[index].ToString()[0] != '[' && right[index].ToString()[0] == '[')
        { // L no es lista
            Console.WriteLine("convertimos L en lista");
            var listLeft = JsonSerializer.Deserialize<List<object>>("[" + left[index].ToString() + "]");
            var listRight = JsonSerializer.Deserialize<List<object>>(right[index].ToString());

            return compareOrder(listLeft, listRight, 0);
        }

        return compareOrder(left, right, index + 1);
    }

}

class Group {
    public Group(string left, string right)
    {
        this.leftRaw = left; 
        this.rightRaw = right;
        this.left = JsonSerializer.Deserialize<List<object>>(left);
        this.right = JsonSerializer.Deserialize<List<object>>(right);
    }

    public List<object> left {get;set;}
    public List<object> right {get;set;}
    public string leftRaw {get;set;}
    public string rightRaw {get;set;}
}