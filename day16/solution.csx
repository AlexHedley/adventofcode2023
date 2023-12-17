#load "../utils/utils.csx"

public class Day16
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;
        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 16 --", true, true);
Utils.Log("-----------", true, true);

var day16 = new Day16();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day16.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day16.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();