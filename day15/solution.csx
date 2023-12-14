#load "../utils/utils.csx"

public class Day15
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

Utils.Log("-- Day 15 --", true, true);
Utils.Log("-----------", true, true);

var day15 = new Day15();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day15.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day15.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();