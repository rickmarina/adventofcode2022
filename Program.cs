// See https://aka.ms/new-console-template for more information

string filename = Environment.CurrentDirectory + @".\day8\input.txt";
var lines = File.ReadAllLines(filename);


int n = lines[0].Length;
int m = lines.Count();
int[,] map = new int[m, n];
int[,] mapVisible = new int[m, n];


for (int j = 0; j < m; j++)
{
    for (int i = 0; i < n; i++)
    {
        map[j, i] = int.Parse(lines[j][i].ToString());
        mapVisible[j, i] = 0;
    }
}

printMap(map);

//from top 
for (int i = 0; i < n; i++)
{
    int maxCol = -1;
    for (int j = 0; j < m; j++)
    {
        //col i row j        
        if (map[j, i] > maxCol) {
            maxCol = map[j,i];
            mapVisible[j,i] = 1;
        }
    }
}

//from down
for (int i = 0; i < n; i++)
{
    int maxCol = -1;
    for (int j = m-1; j >= 0; j--)
    {
        //col i row j        
        if (map[j, i] > maxCol) {
            maxCol = map[j,i];
            mapVisible[j,i] = 1;
        }
    }
}

//from left 
for (int j = 0; j < m; j++)
{
    int maxRow = -1; 
    for (int i = 0; i < n; i++)
    {
        if (map[j, i] > maxRow) { 
            maxRow = map[j,i];
            mapVisible[j,i] = 1;
        }
    }
}

//from right
for (int j = 0; j < m; j++)
{
    int maxRow = -1; 
    for (int i = n-1; i >= 0; i--)
    {
        if (map[j, i] > maxRow) { 
            maxRow = map[j,i];
            mapVisible[j,i] = 1;
        }
    }
}

printMap(mapVisible);
int result = totalVisibles(mapVisible);

Console.WriteLine("Result: "+result);



static void printMap(int[,] map)
{
    for (int j = 0; j <= map.GetUpperBound(0); j++)
    {
        for (int i = 0; i <= map.GetUpperBound(1); i++)
        {
            Console.Write(map[j,i]);
        }
        Console.WriteLine();
    }
    Console.WriteLine("----------------");
    
}

static int totalVisibles(int[,] map) { 
    int total = 0;
    for (int j = 0; j <= map.GetUpperBound(0); j++)
    {
        for (int i = 0; i <= map.GetUpperBound(1); i++)
        {
            if (map[j,i] == 1) {
                total++;
            }
        }
    }
    return total;
}



