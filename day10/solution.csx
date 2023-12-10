#load "../utils/utils.csx"

public class Day10
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    // Day 12 - 2022?

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);

        //Utils.UpdatePosition(matrix, 1, 1, "S");
        // error CS1503: Argument 2: cannot convert from 'System.Tuple<int, int>' to '(int, int)'
        var startingPosition = Utils.CoordinatesOf(matrix, "S").ToValueTuple(); //.ToTuple();
        Utils.Log($"SP{startingPosition} '{matrix[startingPosition.Item1, startingPosition.Item2]}'", logToConsole, logToFile);

        // var seven = matrix[1, 3]; // matrix[row, col]
        // Utils.Log($"matrix[1, 3] : '{seven}' (7)", logToConsole, logToFile);

        // GetAdjacents -> Day 3  - 2023
        // CheckNearest -> Day 12 - 2022?
        // var adjacents = GetAdjacents(matrix, startingPosition.Item1, startingPosition.Item2);
        // Utils.Log($"{string.Join(" ", adjacents)}", logToConsole, logToFile);
        var adjacentPositions = GetAdjacentPositions(matrix, startingPosition.Item1, startingPosition.Item2);
        Utils.Log($"APs{string.Join(" ", adjacentPositions)}", logToConsole, logToFile);

        var currentPos = startingPosition;
        // For each adjacent, check if it is an allowed move.
        foreach (var adjacentPosition in adjacentPositions)
        {
            CheckAdjacentPosition(matrix, startingPosition, currentPos, adjacentPosition);
        }
    }

    public void CheckAdjacentPosition(string[,] matrix, (int, int) startingPosition, (int, int) currentPos, (int, int) adjacentPosition)
    {
        var adjacentPositions = new List<(int, int)>();

        Utils.Log($"CP{currentPos} '{matrix[currentPos.Item1, currentPos.Item2]}' | AP{adjacentPosition} '{matrix[adjacentPosition.Item1, adjacentPosition.Item2]}'", logToConsole, logToFile);
        var validMove = ValidMove(matrix, currentPos, adjacentPosition);
        if (validMove.Item1)
        {
            Utils.Log($"AP{adjacentPosition} '{matrix[adjacentPosition.Item1, adjacentPosition.Item2]}'", logToConsole, logToFile);
            adjacentPositions = GetAdjacentPositions(matrix, adjacentPosition.Item1, adjacentPosition.Item2);
            // Remove previous
            adjacentPositions.Remove(currentPos);
            Utils.Log($"APs{string.Join(" ", adjacentPositions)}", logToConsole, logToFile);
            
            // Get the next position based on direction.
            var position = GetNextPosition(matrix, validMove.Item2, adjacentPosition);
            Utils.Log($"Next Position: {position} '{matrix[position.Item1, position.Item2]}'", logToConsole, logToFile);

            // Check if back at start. 
            if (adjacentPosition == startingPosition)
            {
                Utils.Log($"We are back at the start.", logToConsole, logToFile);
            }

            Utils.Log($"RE LOOP", logToConsole, logToFile);
            CheckAdjacentPosition(matrix, startingPosition, adjacentPosition, position);
        }
        else
        {
            Utils.Log($"{adjacentPosition} is not a valid move.", logToConsole, logToFile);
            adjacentPositions.Remove(currentPos);
            Utils.Log($"APs #:{adjacentPositions.Count}", logToConsole, logToFile);
            adjacentPosition = adjacentPositions.First();
        }
    }

    public (int, int) GetNextPosition(string[,] matrix, Direction direction, (int, int) curPosition)
    {
        var updatedPos = curPosition;
        switch (direction)
        {
            case Direction.North:
                updatedPos.Item1 -= 1;
                break;
            case Direction.East:
                updatedPos.Item2 += 1;
                break;
            case Direction.South:
                updatedPos.Item1 += 1;
                break;
            case Direction.West:
                updatedPos.Item2 -= 1;
                break;
            default:
                Utils.Log($"direction:{direction}...", logToConsole, logToFile);
                break;
        }

        if (matrix[updatedPos.Item1, updatedPos.Item2] == ".")
            return curPosition;

        return updatedPos;
    }

    public (bool, Direction) ValidMove(string[,] matrix, (int, int) currentPos, (int, int) nextPos)
    {
        var currentPosValue = matrix[currentPos.Item1, currentPos.Item2];
        var nextPosValue = matrix[nextPos.Item1, nextPos.Item2];
        Utils.Log($"VM - CP '{currentPosValue}' | NP '{nextPosValue}'", logToConsole, logToFile);

        //   N     NW   NE      y 
        // W ✚ E     ✚         |
        //   S     SW   SE       --> x
        // var direction = GetDirection(currentPos, nextPos);
        // Utils.Log($"{direction}", logToConsole, logToFile);
        var direction = GetDirection(nextPos, currentPos);
        Utils.Log($"Direction: '{direction}'", logToConsole, logToFile);

        // var allowedDirectionsCP = pipeMappings[currentPosValue];
        // Utils.Log($"Allowed Directions Current: ({currentPosValue}) '{string.Join(",", allowedDirectionsCP)}'", logToConsole, logToFile);
        var allowedDirectionsNP = pipeMappings[nextPosValue];
        Utils.Log($"Allowed Directions Next: ({nextPosValue}) '{string.Join(",", allowedDirectionsNP)}'", logToConsole, logToFile);

        if (allowedDirectionsNP.Contains(direction))
        {
            Utils.Log($"Allowed", logToConsole, logToFile);
            return (true, direction);
        }

        return (false, Direction.Undefined);
    }

    // https://stackoverflow.com/a/35106009/2895831
    public Direction GetDirection((int, int) currentPos, (int, int) nextPos)
    {
        //   N     NW   NE
        // W ✚ E     ✚
        //   S     SW   SE

        // Point p1, Point p2
        // double rad = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        double rad = Math.Atan2(nextPos.Item1 - currentPos.Item1, nextPos.Item2 - currentPos.Item2);

        // Adjust result to be between 0 to 2*Pi
        if (rad < 0)
            rad = rad + (2 * Math.PI);

        var deg = rad * (180 / Math.PI);

        if (deg == 0)
            return Direction.East;
        else if (deg == 45)
            return Direction.Northeast;
        else if (deg == 90)
            return Direction.North;
        else if (deg == 135)
            return Direction.Northwest;
        else if (deg == 180)
            return Direction.West;
        else if (deg == 225)
            return Direction.Southwest;
        else if (deg == 270)
            return Direction.South;
        else if (deg == 315)
            return Direction.Southeast;
        else
            return Direction.Undefined;
    }

    // public void Part2(string[] lines)
    // {
    // }

    public List<string> GetAdjacents(string[,] matrix, int i, int j)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var adjecents = new List<string>();
        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        // U D L R
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

        var topLeft = "";
        var topRight = "";
        var bottomLeft = "";
        var bottomRight = "";

        // Diagonals
        if (i-1 > -1 && j-1 > -1)
        {
            topLeft = matrix[i-1, j-1];
            adjecents.Add(topLeft);
        }
        if (i+1 < rowLength && j-1 > -1)
        {
            bottomLeft = matrix[i+1, j-1];
            adjecents.Add(bottomLeft);
        }
        if (i-1 > -1 && j+1 < colLength)
        {
            topRight = matrix[i-1, j+1];
            adjecents.Add(topRight); 
        }
        if (i+1 < rowLength && j+1 < colLength)
        {
            bottomRight = matrix[i+1, j+1];
            adjecents.Add(bottomRight);
        }

        Utils.Log($"TR:{topRight} | TL:{topLeft} | BR:{bottomRight} | BL:{bottomLeft}", logToConsole, logToFile);

        return adjecents;
    }

    public List<(int, int)> GetAdjacentPositions(string[,] matrix, int i, int j)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var adjecents = new List<(int, int)>();
        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        // U D L R
        if (i-1 > -1)
        {
            top = matrix[i-1, j];
            var isntDot = top != ".";
            if (isntDot)
                adjecents.Add((i-1, j));
        }
        if (j-1 > -1)
        {
            left = matrix[i, j-1];
            var isntDot = left != ".";
            if (isntDot)
                adjecents.Add((i, j-1));
        }
        if (i+1 < rowLength)
        {
            bottom = matrix[i+1, j];
            var isntDot = bottom != ".";
            if (isntDot)
                adjecents.Add((i+1, j)); 
        }
        if (j+1 < colLength)
        {
            right = matrix[i, j+1];
            var isntDot = right != ".";
            if (isntDot)
                adjecents.Add((i, j+1));
        }

        Utils.Log($"T:{top} | L:{left} | B:{bottom} | R:{right}", logToConsole, logToFile);

        var topLeft = "";
        var topRight = "";
        var bottomLeft = "";
        var bottomRight = "";

        // Diagonals
        if (i-1 > -1 && j-1 > -1)
        {
            topLeft = matrix[i-1, j-1];
            var isntDot = topLeft != ".";
            if (isntDot)
                adjecents.Add((i-1, j-1));
        }
        if (i+1 < rowLength && j-1 > -1)
        {
            bottomLeft = matrix[i+1, j-1];
            var isntDot = bottomLeft != ".";
            if (isntDot)
                adjecents.Add((i+1, j-1));
        }
        if (i-1 > -1 && j+1 < colLength)
        {
            topRight = matrix[i-1, j+1];
            var isntDot = topRight != ".";
            if (isntDot)
                adjecents.Add((i-1, j+1)); 
        }
        if (i+1 < rowLength && j+1 < colLength)
        {
            bottomRight = matrix[i+1, j+1];
            var isntDot = bottomRight != ".";
            if (isntDot)
                adjecents.Add((i+1, j+1));
        }

        Utils.Log($"TR:{topRight} | TL:{topLeft} | BR:{bottomRight} | BL:{bottomLeft}", logToConsole, logToFile);

        return adjecents;
    }

    private static Dictionary<string, List<Direction>> pipeMappings = new Dictionary<string, List<Direction>> { 
        { "|", new List<Direction>() { Direction.North, Direction.South } },
        { "-", new List<Direction>() { Direction.East, Direction.West } },
        { "L", new List<Direction>() { Direction.South, Direction.West } }, //Direction.North, Direction.East
        { "J", new List<Direction>() { Direction.North, Direction.West } },
        { "7", new List<Direction>() { Direction.South, Direction.West } },
        { "F", new List<Direction>() { Direction.South, Direction.East } },
        { ".", new List<Direction>() {} },
        { "S", new List<Direction>() {} }
    };

    public enum Direction
    {
        North,
        South,
        East,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest,
        Undefined
    }
}

Utils.Log("-- Day 10 --", true, true);
Utils.Log("-----------", true, true);

var day10 = new Day10();

// string fileName = @"input-sample.txt";
string fileName = @"input-sample-1.0.txt";
// string fileName = @"input-sample-1.1.txt";
// string fileName = @"input-sample-1.2.txt";
// string fileName = @"input-sample-1.3.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day10.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day10.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();