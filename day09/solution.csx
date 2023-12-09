#load "../utils/utils.csx"

public class Day9
{
    bool logToConsole = false;
    bool logToFile = false;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var total = 0L;
        foreach (var line in lines)
        {
            var nextValue = CalculateNextValue(line);
            total += nextValue;
        }
        Utils.Answer($"{total}", true, logToFile);
    }

    public long CalculateNextValue(string line)
    {
        var lineCalcs = CalculateLines(line);

        var c = lineCalcs.Count;
        var nextValue = 0L;
        for (var i = c-1; i > 0; i--)
        {
            // Get last element of the last line.
            // var lastLine = lineCalcs[c];
            // var lastItemLastLine = lastLine.Last();
            var lastItemLastLine = nextValue;
            Utils.Info($"{lastItemLastLine}", logToConsole, logToFile);

            var secondLastLine = lineCalcs[i-1];
            var lastItemSecondLastLine = secondLastLine.Last();
            Utils.Info($"{lastItemSecondLastLine}", logToConsole, logToFile);
            
            nextValue = lastItemLastLine + lastItemSecondLastLine;
            Utils.Info($"{nextValue} ({lastItemLastLine} + {lastItemSecondLastLine})", logToConsole, logToFile);

            lastItemLastLine = nextValue;
        }
        
        return nextValue;
    }

    public List<List<long>> CalculateLines(string line)
    {
        Utils.Log($"{line}", logToConsole, logToFile);
        var items = line.Split(' ')?.Select(long.Parse)?.ToList();
        Utils.Log($"Items: {string.Join(',', items)}", logToConsole, logToFile);

        bool isAllEqual = items.Count > 0 && !items.Any(x => x != 0);
        
        var lineCalcs = new List<List<long>>();
        lineCalcs.Add(items);

        while (!isAllEqual)
        {
            var diffList = new List<long>();
            for (var i = 0; i < items.Count; i++)
            {
                var item1 = items.ElementAt(i);
                if (i == items.Count - 1) break;
                var item2 = items.ElementAt(i+1);
                Utils.Log($"{item1} {item2}", logToConsole, logToFile);
                var diff = item2 - item1;
                Utils.Log($"{diff}", logToConsole, logToFile);

                diffList.Add(diff);
            }
            isAllEqual = diffList.Count > 0 && !diffList.Any(x => x != 0);
            items = diffList;
            lineCalcs.Add(diffList);
        }
        
        // Print Line Calcs
        foreach (var lineCalc in lineCalcs)
        {
            Utils.Log($"{string.Join(' ', lineCalc)}", logToConsole, logToFile);
        }

        return lineCalcs;
    }

    public void Part2(string[] lines)
    {
        var total = 0L;
        foreach (var line in lines)
        {
            var nextValue = CalculateNextValue2(line);
            total += nextValue;
        }
        Utils.Answer($"{total}", true, logToFile);
    }

    public long CalculateNextValue2(string line)
    {
        var lineCalcs = CalculateLines(line);

        var c = lineCalcs.Count;
        var nextValue = 0L;
        for (var i = c-1; i > 0; i--)
        {
            // Get last element of the last line.
            // var lastLine = lineCalcs[c];
            // var lastItemLastLine = lastLine.Last();
            var lastItemLastLine = nextValue;
            Utils.Info($"{lastItemLastLine}", logToConsole, logToFile);

            var secondLastLine = lineCalcs[i-1];
            var lastItemSecondLastLine = secondLastLine.First();
            Utils.Info($"{lastItemSecondLastLine}", logToConsole, logToFile);
            
            nextValue = lastItemSecondLastLine - lastItemLastLine;
            Utils.Info($"{nextValue} ({lastItemLastLine} + {lastItemSecondLastLine})", logToConsole, logToFile);

            lastItemLastLine = nextValue;
        }
        
        return nextValue;
    }

}

Utils.Log("-- Day 9 --", true, true);
Utils.Log("-----------", true, true);

var day9 = new Day9();

// string fileName = @"input-sample.txt";
// string fileName = @"input-sample-2.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day9.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day9.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();