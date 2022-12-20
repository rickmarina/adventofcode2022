
using System.Data;
using System.Text.RegularExpressions;
using System.Text.Json;

string filename = Environment.CurrentDirectory + @".\day14\input.txt";
var lines = File.ReadAllLines(filename);

Regex patCoord = new Regex(@"(\d+,\d+)");

Dictionary<string, string> map = new();

int abyssLine =0;

Point sand = new Point(500,0);

foreach (var line in lines) {
    var matches = patCoord.Matches(line);

    Point from = new Point(int.Parse(matches[0].ToString().Split(",")[0]),int.Parse(matches[0].ToString().Split(",")[1]));
    for (int i = 1; i < matches.Count; i++)
    {
        Point to = new Point(int.Parse(matches[i].ToString().Split(",")[0]),int.Parse(matches[i].ToString().Split(",")[1]));
        //from -> to in map 
        AddInMap(from,to,map);
        Console.WriteLine($"{from} -> {to} in map");
        abyssLine = Math.Max(abyssLine , from.y);
        abyssLine = Math.Max(abyssLine , to.y);

        from = to; 
    }
    
    Console.WriteLine("---");
}

Console.WriteLine("abyss: "+abyssLine);

// sand starts 
int totalsand = 0; 

bool abyss = false; 
while (!abyss) {
    Point psand = new Point(sand.x, sand.y);
    totalsand++;

    bool canfall = true; 
    while (canfall) {

        Console.WriteLine(psand.ToString());
        //Console.ReadKey();
        psand.y++;

        if (psand.y > abyssLine) {
            abyss = true;
            totalsand--;
            break;
        }

        if (GetPointMapValue(psand, map) == "#" || GetPointMapValue(psand, map) == "o") {
            // el punto está bloqueado, intentamos a la izquierda 
            psand.x--; 
            if (GetPointMapValue(psand, map) == "#" || GetPointMapValue(psand, map) == "o") {
                psand.x+=2;
                //intentamos a la derecha 
                if (GetPointMapValue(psand, map) == "#" || GetPointMapValue(psand, map) == "o") {
                    canfall=false; 
                } 
            } 
        }
        if (!canfall) {
            psand.y--;
            psand.x--;
            Console.WriteLine("No puede caer más, lo dejamos en "+psand.ToString());
            map[psand.ToString()]= "o";
        }
    }

    //printMap(map, 0, 494, 9, 503);
    //Console.ReadKey();
}

Console.WriteLine("abyss: "+abyssLine);
Console.WriteLine("total sand: "+totalsand);

static string GetPointMapValue(Point p, Dictionary<string, string> map) {
    if (map.ContainsKey(p.ToString())) 
        return map[p.ToString()];
    else {
        map.Add(p.ToString(), ".");
        return ".";
    } 
}

//Rellenar con . el resto de posiciones?
static void AddInMap(Point from, Point to, Dictionary<string, string> map) {

    while (from.ToString() != to.ToString()) {
            if (!map.ContainsKey(from.ToString()))
                map.Add(from.ToString(), "#"); //puede que vengan claves iguales

            if (from.x < to.x) {
                from.x++;
            } else if (from.x > to.x) {
                from.x--;
            } else if (from.y > to.y) {
                from.y--;
            } else if (from.y < to.y) { 
                from.y++;
            }
    }
    if (!map.ContainsKey(to.ToString()))
                map.Add(to.ToString(), "#"); 
}

static void printMap(Dictionary<string, string> map, int y0, int x0, int y1, int x1) {

    for (int y = y0; y <= y1; y++)
    {
        for (int x= x0; x<= x1; x++) { 
            Console.Write(GetPointMapValue(new Point(x,y), map));
        }
        Console.WriteLine();
    }

}

class Point {
    public int x {get;set;}
    public int y {get;set;}

    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return $"{x},{y}";
    }
}