using System.Text.RegularExpressions;

public class SolveDay7
{

    public void solve()
    {
        string filename = Environment.CurrentDirectory + @".\day7\input.txt";
        var lines = File.ReadAllLines(filename);

        Console.WriteLine("------- Start ");

        Dictionary<string, Nodo> tree = new Dictionary<string, Nodo>();

        Regex patCommand = new Regex(@"\$ (\w*)(.*)");
        Regex patDir = new Regex(@"dir (\w*)");
        Regex patFile = new Regex(@"^(\d+) (.*)");

        Stack<string> path = new();

        foreach (var l in lines)
        {
            Console.WriteLine($"{l}");

            if (patCommand.IsMatch(l))
            {
                var matches = patCommand.Matches(l);
                string com = matches[0].Groups[1].Value;
                string dir = "";
                if (com.Equals("cd"))
                {
                    dir = matches[0].Groups[2].Value.Trim();
                    if (dir.Equals("/"))
                    {
                        path.Push("/");
                        tree.Add(dir, new Nodo(""));
                    }
                    else if (dir.Equals(".."))
                    {
                        _ = path.Pop();
                    }
                    else
                    {
                        string parent = getPath(path);
                        path.Push(dir);
                        tree.Add(getPath(path), new Nodo(parent));
                    }
                    Console.WriteLine($"path->{getPath(path)}");

                }
            }
            else if (patFile.IsMatch(l))
            {
                string fname = patFile.Matches(l)[0].Groups[2].Value;
                long size = long.Parse(patFile.Matches(l)[0].Groups[1].Value);

                Fichero fich = new Fichero(fname, size);
                tree[getPath(path)].files.Add(fich);
                string aux = getPath(path);

                while (!string.IsNullOrEmpty(tree[aux].parent))
                {
                    tree[aux].totalsize += size;
                    Console.WriteLine($"incrementamos en {size} el dir {aux}");

                    aux = tree[aux].parent;
                }
                Console.WriteLine($"incrementamos en {size} el dir {aux}");
                tree["/"].totalsize += size;
                Console.WriteLine($"fichero: {fname} size: {size}");
            }

        }

        //Recorremos el arbol-diccionario para obtener el tamaño de los directorios 
        foreach (var key in tree.Keys)
        {
            Console.WriteLine($"dir: {key} size: {tree[key].totalsize}");

        }

        long result = tree.Values.Where(x => x.totalsize <= 100_000).Sum(y => y.totalsize);
        Console.WriteLine(result);


        Console.WriteLine("------- End ");

        // PART 2 INI
        long totalFS = 70000000;
        Console.WriteLine("Total filesystem spcae: " + totalFS);
        Console.WriteLine("Total used: " + tree["/"].totalsize);
        long free = totalFS - (tree["/"].totalsize);
        long needed = 30000000 - free;
        Console.WriteLine("Needed: " + needed);

        long min = long.MaxValue;
        long selected = 0;
        foreach (var dir in tree.Values.Where(x => x.totalsize >= needed))
        {
            if (dir.totalsize - needed < min)
            {
                min = dir.totalsize - needed;
                selected = dir.totalsize;
            }
        }
        // PART 2 FIN

        Console.WriteLine("selected directory: " + selected);
    }


    static string getPath(Stack<string> path) => string.Join(">",path.ToArray().Reverse());

}



// Estructuras de soporte 

class Fichero {
    public string filename;
    public long size; 

    public Fichero(string f, long s) {
        this.filename = f; 
        this.size = s;
    }
}
class Nodo {
    public List<Fichero> files; 
    public string parent;
    public long totalsize;

    public Nodo(string p) {
        files = new();
        parent = p;
        totalsize= 0;
    }
}