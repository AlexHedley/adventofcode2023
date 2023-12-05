#load "../utils/utils.csx"

public class Day6
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {

    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 6 --", true, true);
Utils.Log("-----------", true, true);

var day6 = new Day6();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day6.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day6.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();