#load "../utils/utils.csx"

public class Day6
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var data = new List<List<long>>();
        data = ParseLines(lines);
        Utils.Log($"List #: {data.Count}", logToConsole, logToFile);
        
        var races = new List<Race>();
        races = CreateRaces(data);
        // races.ForEach(Console.WriteLine);

        var counts = new List<long>();
        counts = CalculateTimes(races);

        var total = counts.Aggregate((a, x) => a * x);
        Utils.Answer($"{total}");
    }

    public List<long> CalculateTimes(List<Race> races)
    {
        var counts = new List<long>();

        foreach(var race in races)
        {
            var raceTimes = new List<long>();

            var time = race.Time;
            for(var j = 0; j <= time; j++)
            {
                var raceTime = j * (time - j);
                Utils.Log($"[{j} * ({time} - {j})] {raceTime}", logToConsole, logToFile);
                raceTimes.Add(raceTime);
            }
            var count = raceTimes.Where(rc => rc > race.Distance).Count();
            Utils.Log($"{count} {race.Distance}", logToConsole, logToFile);
            Utils.Log($"", logToConsole, logToFile);

            counts.Add(count);
        }

        return counts;
    }

    public List<Race> CreateRaces(List<List<long>> dataSets)
    {
        var races = new List<Race>();
        
        var times = dataSets[0];
        var distances = dataSets[1];
        
        for(var i = 0; i < times.Count; i++)
        {
            var race = new Race();
            
            race.Time = times[i];
            race.Distance = distances[i];
            
            races.Add(race);
        }

        return races;
    }

    public List<List<long>> ParseLines(string[] lines)
    {
        var data = new List<List<long>>();

        foreach(var line in lines)
        {
            var numbers = line.Split(':', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)?.Select(long.Parse)?.ToList();
            data.Add(numbers);
        }

        return data;
    }

    // public void Part2(string[] lines)
    // {
    // }
}

public class Race
{
    public long Time = 0;
    public long Distance = 0;

    public override string ToString()
    {
        return $"{Time}: {Distance}";
    }
}

Utils.Log("-- Day 6 --", true, true);
Utils.Log("-----------", true, true);

var day6 = new Day6();

// string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
string fileName = @"input-2.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day6.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day6.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();