#load "../utils/utils.csx"

public class Day11
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

        // var row = Utils.GetRow(matrix, 0); //rowIndex
        // Utils.Log($"{matrix[0, 0]} - {string.Join(" ", row)}", logToConsole, logToFile);
        // var col = Utils.GetColumn(matrix, 0); //colIndex
        // Utils.Log($"{matrix[0, 0]} - {string.Join(" ", col)}", logToConsole, logToFile);

        var emptyRows = FindEmptyRows(matrix);
        Utils.Log($"Empty Rows: {string.Join(" ", emptyRows)}", logToConsole, logToFile);

        var emptyColumns = FindEmptyColumns(matrix);
        Utils.Log($"Empty Columns: {string.Join(" ", emptyColumns)}", logToConsole, logToFile);
        
        var galaxy = UpdateGalaxy(lines, emptyRows, emptyColumns);
        rows = galaxy.Length;
        cols = galaxy[0].Length;
        matrix = Utils.GenerateMatrix<string>(galaxy, rows, cols);

        UpdateGalaxyNumbers(ref matrix);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);

        CalculatePaths(matrix);
    }

    public void CalculatePaths(string[,] matrix)
    {
        Utils.Info($"TODO - CalculatePaths", logToConsole, logToFile);
    }

    public void UpdateGalaxyNumbers(ref string[,] matrix)
    {
        var g = 1;
        var allFound = false;
        while (!allFound)
        {
            var galaxy = Utils.CoordinatesOf(matrix, "#");
            Utils.Log($"Galaxy: {galaxy}", logToConsole, logToFile);
            
            if (galaxy.Item1 == -1 && galaxy.Item2 == -1)
            {
                allFound = true;
                break;
            }
            
            Utils.UpdatePosition(matrix, galaxy.Item1, galaxy.Item2, $"{g}");
            g++;
        }
    }

    public string[] UpdateGalaxy(string[] lines, List<int> emptyRows, List<int> emptyColumns)
    {
        var rowsLength = lines.Length;
        var colsLength = lines[0].Length;

        var r = 0;
        // https://www.c-sharpcorner.com/article/how-to-insert-an-element-into-an-array-in-c-sharp2/
        foreach (var row in emptyRows)
        {
            var lineToAdd = string.Join("", Enumerable.Repeat(".", rowsLength));

            Array.Resize(ref lines, lines.Length + 1); // increase array length by 1
            // Copy (Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length);
            Array.Copy(lines, row + r, lines, row + r + 1, lines.Length - row - r - 1); // shift elements right from the index
            lines[row + r] = lineToAdd; // insert element at the index
            
            r++;
        }

        // Update to new length
        rowsLength = lines.Length;

        var c = 0;
        foreach (var col in emptyColumns)
        {
            for (var l = 0; l < rowsLength; l++)
            {
                lines[l] = lines[l].Insert(col + c, ".");
            }
            c++;
        }
        
        foreach(var l in lines)
        {
            Console.WriteLine(l);
        }

        return lines;
    }

    public List<int> FindEmptyRows(string[,] matrix)
    {
        var emptyRows = new List<int>();

        int rowLength = matrix.GetLength(0);

        for (var i = 0; i < rowLength; i++)
        {
            var row = Utils.GetRow(matrix, i); //rowIndex
            // Utils.Log($"{matrix[i, 0]} - {string.Join(" ", row)}", logToConsole, logToFile);

            if (row.ToList().All(r => r == "."))
            {
                emptyRows.Add(i);
                // Utils.Log($"{i}", logToConsole, logToFile);
            }
        }

        return emptyRows;
    }

    public List<int> FindEmptyColumns(string[,] matrix)
    {
        var emptyColumns = new List<int>();

        int colLength = matrix.GetLength(1);

        for (var i = 0; i < colLength; i++)
        {
            var col = Utils.GetColumn(matrix, i); //colIndex
            // Utils.Log($"{matrix[i, 0]} - {string.Join(" ", row)}", logToConsole, logToFile);

            if (col.ToList().All(c => c == "."))
            {
                emptyColumns.Add(i);
                // Utils.Log($"{i}", logToConsole, logToFile);
            }
        }

        return emptyColumns;
    }



    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 11 --", true, true);
Utils.Log("-----------", true, true);

var day11 = new Day11();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day11.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day11.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();