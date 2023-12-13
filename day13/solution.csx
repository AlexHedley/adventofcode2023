#load "../utils/utils.csx"

public class Day13
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var patterns = GetPatterns(lines);
        var total = GetPatternPairs(patterns);
        Utils.Answer($"{total}", logToConsole, logToFile);
    }

    public int GetPatternPairs(List<List<string>> patterns)
    {
        var sum = 0;

        var len = patterns.Count;
        for (var p = 0; p < len; p++)
        {
            Utils.Log($"Checking {p} against {p+1}", logToConsole, logToFile);
            var pattern1 = patterns[p];
            var pattern2 = patterns[p+1];
            var total = CheckPatternPairs(pattern1, pattern2);
            sum += total;

            p = p+2;
        }

        return sum;
    }

    public int CheckPatternPairs(List<string> patternOne, List<string> patternTwo)
    {
        var pattern1 = patternOne.ToArray(); 
        var rows = pattern1.Length;
        var cols = pattern1[0].Length;
        var matrix1 = Utils.GenerateMatrix<string>(pattern1, rows, cols);
        Utils.PrintMatrix(matrix1, logToConsole, logToFile);
        var leftNumber = CheckForMatchingColumns(matrix1);

        var pattern2 = patternTwo.ToArray(); 
        rows = pattern2.Length;
        cols = pattern2[0].Length;
        var matrix2 = Utils.GenerateMatrix<string>(pattern2, rows, cols);
        Utils.PrintMatrix(matrix2, logToConsole, logToFile);
        var aboveNumber = CheckForMatchingRows(matrix2);

        var total = (aboveNumber * 100) + leftNumber;
        Utils.Info($"{total}", logToConsole, logToFile);
        return total;
    }

    public int CheckForMatchingColumns(string[,] matrix)
    {
        int colLength = matrix.GetLength(1);

        var leftNumber = 0;
        // Compare Columns to find matching
        for (var i = 0; i < colLength; i++)
        {
            if (i+1 == colLength-1) break;

            var col1 = Utils.GetColumn(matrix, i);
            Utils.Log($"{matrix[0, i]} - {string.Join(" ", col1)}", logToConsole, logToFile);
            var col2 = Utils.GetColumn(matrix, i+1);
            Utils.Log($"{matrix[0, i+1]} - {string.Join(" ", col2)}", logToConsole, logToFile);

            if (col1.SequenceEqual(col2))
            {
                Utils.Warning($"MATCHING ({i}=={i+1}) [{i+1}, {i+2}]", logToConsole, logToFile);
                leftNumber = i+1;
            }
            Utils.Log($"", logToConsole, logToFile);
        }

        return leftNumber;
    }

    public int CheckForMatchingRows(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        
        var aboveNumber = 0;
        // Compare Rows to find matching
        for (var i = 0; i < rowLength; i++)
        {
            if (i+1 == rowLength-1) break;

            var row1 = Utils.GetRow(matrix, i);
            Utils.Log($"{matrix[0, i]} - {string.Join(" ", row1)}", logToConsole, logToFile);
            var row2 = Utils.GetRow(matrix, i+1);
            Utils.Log($"{matrix[0, i+1]} - {string.Join(" ", row2)}", logToConsole, logToFile);

            if (row1.SequenceEqual(row2))
            {
                Utils.Warning($"MATCHING ({i}=={i+1}) [{i+1}, {i+2}]", logToConsole, logToFile);
                aboveNumber = i+1;
            }
            Utils.Log($"", logToConsole, logToFile);
        }

        return aboveNumber;
    }

    public List<List<string>> GetPatterns(string[] lines)
    {
        List<List<string>> patternMaps = new List<List<string>>();

        List<string> curSet = new List<string>();    
        foreach (var line in lines)
        {
            if (line.Trim().Length == 0)
            {
                if (curSet.Count > 0)
                {
                    patternMaps.Add(curSet);
                    curSet = new List<string>();
                }               
            }
            else
            {
                curSet.Add(line);
            }
        }

        if (curSet.Count > 0)
        {
            patternMaps.Add(curSet);
        }

        return patternMaps;
    }

    // public void Part2(string[] lines)
    // {
    // }

    // Valley of Mirrors
    // KEY
    // ash (.)
    // rocks (#)
}

Utils.Log("-- Day 13 --", true, true);
Utils.Log("-----------", true, true);

var day13 = new Day13();

// string fileName = @"input-sample.txt";
// string fileName = @"input-sample-1.0.txt";
// string fileName = @"input-sample-1.1.txt";
// string fileName = @"input.txt";
string fileName = @"input-1.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day13.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day13.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();