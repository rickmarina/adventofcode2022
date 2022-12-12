
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

string filename = Environment.CurrentDirectory + @".\day11\input.txt";
var lines = File.ReadAllLines(filename);

//parse de input 
Regex patMonkey = new Regex(@"^Monkey (\d+):");
Regex patItems = new Regex(@"Starting items: (.*)");
Regex patOp = new Regex(@"Operation: new = old ([\*|\+]) (\d+|old)");
Regex patDiv = new Regex(@"Test: divisible by (\d+)");
Regex patTrue = new Regex(@"If true: throw to monkey (\d+)");
Regex patFalse = new Regex(@"If false: throw to monkey (\d+)");

List<Monkey> monkeys = new(); 
int id = 0; 
List<long> items = new(); 
string op = ""; 
string opNum = "";
int divisible = 0; 
int ifTrue = 0; 
int ifFalse = 0; 
foreach (string l in lines) {

    if (patMonkey.IsMatch(l)) {
        id = Convert.ToInt32(patMonkey.Matches(l)[0].Groups[1].Value);
    }
    else if (patItems.IsMatch(l)) {
        items.AddRange(patItems.Matches(l)[0].Groups[1].Value.ToString().Split(",").Select(long.Parse));
    }
    else if (patOp.IsMatch(l)) {
        var mop = patOp.Matches(l);
        op = mop[0].Groups[1].Value.ToString();
        opNum = mop[0].Groups[2].Value.ToString();
    }
    else if (patDiv.IsMatch(l)) {
        divisible = Convert.ToInt32(patDiv.Matches(l)[0].Groups[1].Value.ToString());
    }
    else if (patTrue.IsMatch(l)) {
        ifTrue = Convert.ToInt32(patTrue.Matches(l)[0].Groups[1].Value.ToString());
    }
    else if (patFalse.IsMatch(l)) {
        ifFalse = Convert.ToInt32(patFalse.Matches(l)[0].Groups[1].Value.ToString());

        //make monkey 
        Monkey m = new Monkey(); 
        m.id = id;
        m.items.AddRange(items); 
        m.operation = op; 
        m.operationNumber = opNum;
        m.divisible = divisible;
        m.ifTrue = ifTrue; 
        m.ifFalse = ifFalse;

        monkeys.Add(m);

        items.Clear();
    } 
    
        
}

Console.WriteLine("Monkeys created: "+monkeys.Count);


int round = 0; 
while (round < 10000) {
    Console.WriteLine("Round # "+round);
    
    for (int i = 0; i < monkeys.Count; i++)
    {
        Console.WriteLine("Monkey "+monkeys[i].id);
        int divisor = monkeys.Select(x=> x.divisible).Aggregate((x,y) => x*y);
        for (int item = 0; item < monkeys[i].items.Count; item++)
        {
            
            //Console.WriteLine(" inspect item "+monkeys[i].items[item]);
            monkeys[i].GetWorryLevel(item);
            //Console.WriteLine(" worry level is "+ monkeys[i].GetOperationName()+ " by "+monkeys[i].operationNumber +" = "+monkeys[i].worrylevel);
            
            //Part1: 
            //monkeys[i].BoredNum();

            //Part2:
            monkeys[i].worrylevel = monkeys[i].worrylevel % divisor;
            //Console.WriteLine(" bored divided by 3 to "+monkeys[i].worrylevel);
            if (monkeys[i].worrylevel % monkeys[i].divisible == 0) {
                monkeys[monkeys[i].ifTrue].items.Add(monkeys[i].worrylevel);
            } else {
                monkeys[monkeys[i].ifFalse].items.Add(monkeys[i].worrylevel);
            }
        }
        monkeys[i].items.Clear();
        /*
        printMonkeys(monkeys);
        Console.ReadKey();
        */
        
    }

    printMonkeys(monkeys);

    


    round++;
}

var result = monkeys.Select(x=> x.totalInspected).OrderByDescending(x=> x).Take(2).ToArray();

Console.WriteLine(result[0]*result[1]);
    

static void printMonkeys(List<Monkey> monkeys) {
    for (int i = 0; i < monkeys.Count; i++)
    {
        Console.WriteLine($"Monkey {monkeys[i].id}: {string.Join(",",monkeys[i].items)} inspected items # {monkeys[i].totalInspected}");
    }
}

class Monkey { 

    public int id {get;set;}

    public List<long> items {get;set;} = new(); 

    public string operation {get;set;} = "";
    public string operationNumber {get;set;}
    public int divisible {get;set;}

    public int ifTrue {get;set;}
    public int ifFalse {get;set;}
    
    public List<long> incoming {get;set;} = new();

    public long worrylevel {get;set;}

    public long totalInspected {get;set;} = 0;

    public string GetOperationName() {
        return this.operation=="*" ? "multiplied" : "increases";
    }
    public void GetWorryLevel(int idx) {
        totalInspected++;
        long number = operationNumber == "old" ? items[idx] : Convert.ToInt32(operationNumber);
        if (operation.Equals("*"))
            this.worrylevel = items[idx] * number;
        else 
            this.worrylevel = items[idx] + number;
    }

    public void BoredNum() {
        this.worrylevel = this.worrylevel /3;
    }
}
