#load "../utils/utils.csx"

public class Day14
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;
        var matrix1 = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix1, logToConsole, logToFile);

        // Move North
        MoveNorth(ref matrix1);

        Utils.PrintMatrix(matrix1, logToConsole, logToFile);
    }

    public void MoveNorth(ref string[,] matrix)
    {
        int colLength = matrix.GetLength(1);
        for (var i = 0; i < colLength; i++)
        {
            var col = Utils.GetColumn(matrix, i);
            Utils.Log($"{string.Join(" ", col)}", logToConsole, logToFile);
            // Utils.UpdatePosition(matrix, 0, i, "O");
            // Utils.UpdatePosition(matrix, 0, i, ".");

            var len = col.Length;
            for (var j = 0; j < len; j++)
            {
                if (j+1 == len-1) break;
                
                Utils.Log($"{col[j]}", logToConsole, logToFile);
                
                if (j == 0 && col[j] == "O") continue;
                if (col[j] == ".")
                {
                    if (col[j+1] == "O")
                    {
                        Utils.Info($"BOOM", logToConsole, logToFile);
                        // Swap
                        Utils.Log($"{i}{j} {i}{j+1}", logToConsole, logToFile);
                        Utils.UpdatePosition(matrix, i, j, "O");
                        Utils.UpdatePosition(matrix, i, j+1, ".");
                    }
                }
            }
        }
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 14 --", true, true);
Utils.Log("-----------", true, true);

var day14 = new Day14();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// The rounded rocks (O) will roll when the platform is tilted, 
// while the cube-shaped rocks (#) will stay in place. 
// You note the positions of all of the empty spaces (.) and rocks (your puzzle input).

// Part 1
Utils.Log("Part 1", true, true);
day14.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day14.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();