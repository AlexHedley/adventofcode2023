#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day3
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var ROWS = lines.Length;
        var COLS = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, ROWS, COLS);
        Utils.PrintMatrix(matrix);

        CheckAdjecents(matrix);

        // Find all numbers and sum
        // TODO: Remove where adjacent.
        // var numbers = FindNumbersInLines(lines);
        // var sum = numbers.Sum();

        // Utils.Log($"{sum}", logToConsole, logToFile);
        // Utils.Answer($"{sum}");
    }

    public List<long> FindNumbersInLines(string[] lines)
    {
        var numbers = new List<long>();

        foreach (var line in lines)
        {
            numbers.AddRange(FindNumbersInLine(line));
        }
        
        return numbers;
    }

    public List<long> FindNumbersInLine(string line)
    {
        var numbers = new List<long>();

        var pattern = @"\d+";
        Regex reg = new Regex(pattern);
        MatchCollection matchedCommands = reg.Matches(line);
        for (int count = 0; count < matchedCommands.Count; count++)
        {
            Utils.Log($"{matchedCommands[count].Value}", logToConsole, logToFile);
            numbers.Add(long.Parse(matchedCommands[count].Value));
        }

        return numbers;
    }

    public bool IsSymbol(char c)
    {
        if (c == '.') return false;
        if (c == '#') return true;
        if (c == '%') return true;
        if (c == '&') return true;
        if (c == '@') return true;
        if (c == '_') return true;
        if (c == '-') return true;
        if (c == '*') return true;
        if (c == '/') return true;
        
        if (Char.IsSymbol(c)) return true;

        return false;
    }

    public void CheckAdjecents(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            // Get the row and find the numbers in the that row.
            var row = Utils.GetRow(matrix, i);
            var line = string.Join("", row);
            Utils.Log($"{i}: {line}", logToConsole, logToFile);
            var numbers = FindNumbersInLine(line);

            for (int j = 0; j < colLength; j++)
            {
                var currentValue = matrix[i, j];
                Utils.Log($"{currentValue} | {i}:{j}", logToConsole, logToFile);
                // Don't need to check "."
                if (currentValue == ".") continue;

                var adjecents = new List<string>();
                var top = "";
                var left = "";
                var bottom = "";
                var right = "";

                if (i-1 > -1)
                {
                    top = matrix[i-1, j];
                    adjecents.Add(top);
                }
                if (j-1 > -1)
                {
                    left = matrix[i, j-1];
                    adjecents.Add(left);
                }
                if (i+1 < rowLength)
                {
                    bottom = matrix[i+1, j];
                    adjecents.Add(bottom); 
                }
                if (j+1 < colLength)
                {
                    right = matrix[i, j+1];
                    adjecents.Add(right);
                }

                Utils.Log($"T:{top} | L:{left} | B:{bottom} | R:{right}", logToConsole, logToFile);
                
                if (adjecents.Where(a => IsSymbol(a.ToCharArray()[0])).Any())
                {
                    Utils.Log("SYMBOL", logToConsole, logToFile);
                    // var currentString = line.Substring(j+1);
                    // Utils.Log($"{line} : {currentString}", logToConsole, logToFile);
                }
            }
        }
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 3 --", true, true);
Utils.Log("-----------", true, true);

var day3 = new Day3();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day3.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day3.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();