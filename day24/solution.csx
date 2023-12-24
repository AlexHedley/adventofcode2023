#load "../utils/utils.csx"

public class Day24
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var data = ParseInput(lines);

        var intersections = CalculateIntersections(data);
    }

    public List<(float x, float y)> CalculateIntersections(List<Data> data)
    {
        List<(float x, float y)> intersections = new();

        for (var i = 0; i < data.Count; i++)
        {
            var newPosition = CalculateNewPosition(data[i].Position, data[i].Velocity);
            
            // var A1 = data[i].Position.x;
            // var A2 = data[i].Position.y;
            // var B1 = data[i+1].Position.x;
            // var B2 = data[i+1].Position.x;

            // var intersection = CalculateIntersection(A1, A2, B1, B2);
            // intersections.Add(intersection);
        }

        return intersections;
    }

    // // https://stackoverflow.com/a/4543530/2895831
    // // http://www.topcoder.com/tc?module=Static&d1=tutorials&d2=geometry2
    // public (float x, float y) CalculateIntersection(long A1, long A2, long B1, long B2, long C1, long C2)
    // {
    //     float delta = A1 * B2 - A2 * B1;

    //     if (delta == 0) 
    //         throw new ArgumentException("Lines are parallel");

    //     float x = (B2 * C1 - B1 * C2) / delta;
    //     float y = (A1 * C2 - A2 * C1) / delta;

    //     return (x, y);
    // }

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