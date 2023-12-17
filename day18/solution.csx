#load "../utils/utils.csx"

public class Day18
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {

    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 18 --", true, true);
Utils.Log("-----------", true, true);

var day18 = new Day18();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day18.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day18.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();