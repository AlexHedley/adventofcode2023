#load "../utils/utils.csx"

using System.Drawing;

public class Day24
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var data = ParseInput(lines);

        var intersections = CalculateIntersections(data);
        Utils.Log($"Intersections #({intersections.Count}): {string.Join(" ", intersections)}", logToConsole, logToFile);

        var validIntersections = ValidIntersections(intersections);        
        Utils.Answer($"{validIntersections}", logToConsole, logToFile);
    }

    public long ValidIntersections(List<PointF> intersections)
    {
        // Between 7 and 27?
        var valid = intersections.Where(p => (p.X >= 7 && p.X <= 27) && (p.Y >= 7 && p.Y <= 27));
        Utils.Info($"Valid Intersections #({valid.Count()}): {string.Join(" ", valid)}", logToConsole, logToFile);

        return valid.Count();
    }

    public List<PointF> CalculateIntersections(List<Data> data)
    {
        Utils.Log($"Data #: {data.Count}", logToConsole, logToFile);
        // List<(float x, float y)> intersections = new();
        List<PointF> intersections = new();

        var permutations = Utils.GetPermutations(1, data.Count);
        Utils.Log($"Permutations #({permutations.Count}): {string.Join(" ", permutations)}", logToConsole, logToFile);

        // for (var i = 0; i < data.Count; i++)
        // {
        //     if (i+1 == data.Count) break;
        foreach (var permutation in permutations)
        {
            // get first item
            var first = permutation.Item1 - 1;
            var second = permutation.Item2 - 1;
            Utils.Log($"{first} , {second}", logToConsole, logToFile);

            // var p1x1 = data[i].Position.x;
            // var p1y1 = data[i].Position.y;
            var p1x1 = data[first].Position.x;
            var p1y1 = data[first].Position.y;

            // var newPosition = CalculateNewPosition(data[i].Position, data[i].Velocity);
            var newPosition = CalculateNewPosition(data[first].Position, data[first].Velocity);
            
            // Repeat??
            var p1x2 = newPosition.x;
            var p1y2 = newPosition.y;

            // Next Pair
            // var p2x1 = data[i+1].Position.x;
            // var p2y1 = data[i+1].Position.y;
            var p2x1 = data[second].Position.x;
            var p2y1 = data[second].Position.y;

            // newPosition = CalculateNewPosition(data[i+1].Position, data[i+1].Velocity);
            newPosition = CalculateNewPosition(data[second].Position, data[second].Velocity);
            var p2x2 = newPosition.x;
            var p2y2 = newPosition.y;

            // var intersection = CalculateIntersection(A1, A2, B1, B2);
            Func<float, float, PointF> p = (x, y) => new PointF(x, y);
            // var intersection = FindIntersection(p(0f, 0f), p(1f, 1f), p(1f, 2f), p(4f, 5f));
            var intersection = FindIntersection(p(p1x1, p1y1), p(p1x2, p1y2), p(p2x1, p2y1), p(p2x2, p2y2));
            
            Utils.Log($"Intersection: {intersection}", logToConsole, logToFile);
            intersections.Add(intersection);
        }

        return intersections;
    }

    // // https://stackoverflow.com/a/4543530/2895831
    // // http://www.topcoder.com/tc?module=Static&d1=tutorials&d2=geometry2
    // public (float x, float y) CalculateIntersection(long A1, long A2, long B1, long B2, long C1, long C2)
    // {
    //     // A = y2-y1; B = x1-x2; C = Ax1+By1

    //     float delta = A1 * B2 - A2 * B1;

    //     if (delta == 0) 
    //         throw new ArgumentException("Lines are parallel");

    //     float x = (B2 * C1 - B1 * C2) / delta;
    //     float y = (A1 * C2 - A2 * C1) / delta;

    //     return (x, y);
    // }

    // https://rosettacode.org/wiki/Find_the_intersection_of_two_lines#C#
    public PointF FindIntersection(PointF s1, PointF e1, PointF s2, PointF e2)
    {
        Utils.Log($"{s1} {e1} {s2} {e2}", logToConsole, logToFile);

        float a1 = e1.Y - s1.Y;
        float b1 = s1.X - e1.X;
        float c1 = a1 * s1.X + b1 * s1.Y;

        float a2 = e2.Y - s2.Y;
        float b2 = s2.X - e2.X;
        float c2 = a2 * s2.X + b2 * s2.Y;

        float delta = a1 * b2 - a2 * b1;
        //If lines are parallel, the result will be (NaN, NaN).
        return delta == 0 ? new PointF(float.NaN, float.NaN)
            : new PointF((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
    }

    public (long x, long y, long z) CalculateNewPosition((long x, long y, long z) position, (long x, long y, long z) velocity)
    {
        position.x = position.x + velocity.x;
        position.y = position.y + velocity.y;
        position.z = position.z + velocity.z;
        Utils.Log($"Updated: {position.x}, {position.y}, {position.z}", logToConsole, logToFile);

        return position;
    }

    public List<Data> ParseInput(string[] lines)
    {
        var data = new List<Data>();

        foreach (var line in lines)
        {
            var combinations = line.Split("@", StringSplitOptions.TrimEntries);
            (long x, long y, long z) position = combinations[0].Split(',', StringSplitOptions.TrimEntries) switch { var a => ( long.Parse(a[0]),  long.Parse(a[1]),  long.Parse(a[2]) ) };
            (long x, long y, long z) velocity = combinations[1].Split(',', StringSplitOptions.TrimEntries) switch { var a => ( long.Parse(a[0]),  long.Parse(a[1]),  long.Parse(a[2]) ) };
            Utils.Log($"Original: {position.x}, {position.y}, {position.z} @ {velocity.x}, {velocity.y}, {velocity.z}", logToConsole, logToFile);
            
            var d = new Data() { Position = position, Velocity = velocity };
            data.Add(d);
        }

        return data;
    }

    // public void Part2(string[] lines)
    // {
    // }

    public class Data
    {
        public (long x, long y, long z) Position { get; set; }
        public (long x, long y, long z) Velocity { get; set; }
    }
}

Utils.Log("-- Day 24 --", true, true);
Utils.Log("-----------", true, true);

var day24 = new Day24();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day24.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day24.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();