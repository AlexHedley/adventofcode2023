#load "../utils/utils.csx"

#load "GFG.csx"

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
        // Utils.PrintMatrix(matrix, logToConsole, logToFile);

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
        var permutations = GetPermutations(1, 9);
        Utils.Log($"Permutations #({permutations.Count}): {string.Join(" ", permutations)}", logToConsole, logToFile);

        var minPaths = new List<(string galaxyX, string galaxyY, int min)>();

        foreach (var permutation in permutations)
        {
            Utils.Log($"{permutation.Item1} - {permutation.Item2}", logToConsole, logToFile);
            var minPath = CalculateMinPath(matrix, $"{permutation.Item1}", $"{permutation.Item2}");
            // Utils.Warning($"{minPath}", logToConsole, logToFile);
            minPaths.Add(minPath);
        }

        // Utils.Log($"{string.Join(Environment.NewLine, minPaths)}", logToConsole, logToFile);
        var total = Total(minPaths);
        Utils.Log($"Total: {total}", logToConsole, logToFile);
        Utils.Answer($"{total}", true, logToFile);
    }

    public int Total(List<(string galaxyX, string galaxyY, int min)> minPaths)
    {
        var total = 0;
        foreach (var minPath in minPaths)
        {
            total += minPath.Item3;
        }
        return total;
    }

    public (string galaxyX, string galaxyY, int min) CalculateMinPath(string[,] matrix, string galaxyX, string galaxyY)
    {
        var galaxyXCoords = Utils.CoordinatesOf(matrix, galaxyX);
        var galaxyYCoords = Utils.CoordinatesOf(matrix, galaxyY);
        Utils.Log($"{galaxyX}:{galaxyXCoords} - {galaxyY}:{galaxyYCoords}", logToConsole, logToFile);

        // Dijkstra's algorithm?
        // A* Path Finding?
        // BFS - Breadth First Search?
        Utils.UpdatePosition(matrix, galaxyXCoords.Item1, galaxyXCoords.Item2, "s");
        Utils.UpdatePosition(matrix, galaxyYCoords.Item1, galaxyYCoords.Item2, "d");
        var minDistance = GFG.minDistance(matrix);
        Utils.Log($"minDistance: {minDistance}", logToConsole, logToFile);

        // Reset
        Utils.UpdatePosition(matrix, galaxyXCoords.Item1, galaxyXCoords.Item2, galaxyX);
        Utils.UpdatePosition(matrix, galaxyYCoords.Item1, galaxyYCoords.Item2, galaxyY);

        return (galaxyX, galaxyY, minDistance);
    }

    public HashSet<(int, int)> GetPermutations(int first, int last)
    {
        var permutations = new HashSet<(int, int)>();
        
        for (var i = 1; i <= last; i++)
        {
            for (var j = 1; j < last; j++)
            {
                if (i == j+1) continue;
                if (permutations.Contains((j+1, i))) continue;
                permutations.Add((i, j+1));
            }
            
        }
        
        return permutations;
    }

    public void UpdateGalaxyNumbers(ref string[,] matrix)
    {
        var g = 1;
        var allFound = false;
        while (!allFound)
        {
            var galaxy = Utils.CoordinatesOf(matrix, "#");

            if (galaxy.Item1 == -1 && galaxy.Item2 == -1)
            {
                allFound = true;
                break;
            }

            Utils.Log($"Galaxy: ({g}) {galaxy}", logToConsole, logToFile);
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
            Utils.Log($"{l}", logToConsole, logToFile);
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

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day11.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day11.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();