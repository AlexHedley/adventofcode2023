#load "../utils/utils.csx"

public class Day12
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        foreach (var line in lines)
        {
            (string springs, List<long> records) condition = line.Split(" ", StringSplitOptions.TrimEntries) switch { 
                    var c => ((c[0]), c[^1].Split(",", StringSplitOptions.TrimEntries)?.Select(long.Parse)?.ToList()) 
                };
            Utils.Log($"{condition.springs} {string.Join(",", condition.records)}", logToConsole, logToFile);
        }
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 12 --", true, true);
Utils.Log("-----------", true, true);

var day12 = new Day12();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day12.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day12.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();