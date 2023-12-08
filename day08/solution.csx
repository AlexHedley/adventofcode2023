#load "../utils/utils.csx"

public class Day8
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var instructionsLine = lines[0];
        var instructions = instructionsLine.ToCharArray();
        Utils.Log($"{string.Join(',', instructions)}", logToConsole, logToFile);

        var count = lines.Length - 2;
        var networkNodes = lines.Skip(2).Take(count);

        var nodes = BuildData(networkNodes);
        var steps = CalculateSteps(nodes, instructions);
        Utils.Log($"steps:{steps.Item1} {steps.Item2}", logToConsole, logToFile);
        Utils.Log($"DONE", logToConsole, logToFile);
    }

    public (bool, long) CalculateSteps(Dictionary<string, (string, string)> nodes, char[] instructions)
    {
        var steps = 0;
        var i = 1;
        var item = nodes["AAA"];
        Utils.Log($"ITEM: {item}", logToConsole, logToFile);

        while (true)
        {
            var matching = "";
            foreach (var direction in instructions)
            {
                if (direction == 'L')
                {
                    if (matching == item.Item1) return (false, steps);
                    matching = item.Item1;
                    Utils.Log($"L: {matching}", logToConsole, logToFile);
                }
                if (direction == 'R')
                {
                    if (matching == item.Item1) return (false, steps);
                    matching = item.Item2;
                    Utils.Log($"R: {matching}", logToConsole, logToFile);
                }
                
                steps++;
                if (matching == "ZZZ") return (true, steps);
                
                item = nodes[matching];
                Utils.Log($"ITEM: {item}", logToConsole, logToFile);
            }
        }
        
    }

    public Dictionary<string, (string, string)> BuildData(IEnumerable<string> networkNodes)
    {
        var nodes = new Dictionary<string, (string, string)>();

        foreach (var networkNode in networkNodes)
        {
            // Utils.Log($"{networkNode}", logToConsole, logToFile);
            // var node = networkNode.Split(' = ')[0];
            (string node, string lr) data = networkNode.Split(" = ", StringSplitOptions.TrimEntries) switch { var a => (a[0], a[1]) };
            (string l, string r) options = data.lr.Replace("(", "").Replace(")", "").Split(", ") switch { var b => (b[0], b[1]) };
            // Utils.Log($"{options.l} | {options.r}", logToConsole, logToFile);

            nodes.Add(data.node, options);
        }

        return nodes;
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 8 --", true, true);
Utils.Log("-----------", true, true);

var day8 = new Day8();

// string fileName = @"input-sample.txt";
// string fileName = @"input-sample-1.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day8.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day8.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();