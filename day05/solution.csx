#load "../utils/utils.csx"

public class Day5
{
    bool logToConsole = false;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        ParseLines(lines);
    }

    void ParseLines(string[] lines)
    {
        // seeds: 79 14 55 13
        var seeds = lines[0]
                        .Split(':')[1]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        ?.Select(long.Parse)?.ToList();

        Utils.Log($"Seeds: {String.Join(',', seeds)}", logToConsole, logToFile);
        
        var categoryMapLines = GetMaps(lines.Skip(1).ToArray());
        // Utils.Log($"{categoryMapLines.Count}", logToConsole, logToFile);

        List<CategoryMap> categoryMaps = new();
        foreach(var categoryMap in categoryMapLines)
        {
            var map = ParseMap(categoryMap);
            Utils.Log($"{map}", logToConsole, logToFile);
            Utils.Log($"", logToConsole, logToFile);
            categoryMaps.Add(map);
        }

        var locations = GetLocations(seeds, categoryMaps);
        var minLocation = locations.Min();
        Utils.Answer($"{minLocation}");
    }

    public List<long> GetLocations(List<long> seeds, List<CategoryMap> categoryMaps)
    {
        var locations = new List<long>();

        // TODO: Remove
        foreach(var seed in seeds)
        {
            // var seed = //79;
            long soil;
            var soilExists = categoryMaps.FirstOrDefault(c => c.SourceName == "seed").Mapping.TryGetValue(seed, out soil);
            if (!soilExists) soil = seed;
            // Utils.Info($"Seed: {seed} -> Soil: {soil}");

            long fertilizer;
            var fertilizerExists = categoryMaps.FirstOrDefault(c => c.SourceName == "soil").Mapping.TryGetValue(soil, out fertilizer);
            if (!fertilizerExists) fertilizer = soil;
            // Utils.Info($"Soil: {soil} -> Fertilizer: {fertilizer}");

            long water;
            var waterExists = categoryMaps.FirstOrDefault(c => c.SourceName == "fertilizer").Mapping.TryGetValue(fertilizer, out water);
            if (!waterExists) water = fertilizer;
            // Utils.Info($"Fertilizer: {fertilizer} -> Water: {water}");

            long light;
            var lightExists = categoryMaps.FirstOrDefault(c => c.SourceName == "water").Mapping.TryGetValue(water, out light);
            if (!lightExists) light = water;
            // Utils.Info($"Water: {water} -> Light: {light}");

            long temperature;
            var temperatureExists = categoryMaps.FirstOrDefault(c => c.SourceName == "light").Mapping.TryGetValue(light, out temperature);
            if (!temperatureExists) temperature = light;
            // Utils.Info($"Light: {light} -> Temperature: {temperature}");

            long humidity;
            var humidityExists = categoryMaps.FirstOrDefault(c => c.SourceName == "temperature").Mapping.TryGetValue(temperature, out humidity);
            if (!humidityExists) humidity = temperature;
            // Utils.Info($"Temperature: {temperature} -> Humidity {humidity}");

            long location;
            var locationExists = categoryMaps.FirstOrDefault(c => c.SourceName == "humidity").Mapping.TryGetValue(humidity, out location);
            if (!locationExists) location = humidity;
            // Utils.Info($"Humidity {humidity} -> Location: {location}");

            locations.Add(location);
        }

        return locations;
    }

    public CategoryMap ParseMap(List<string> categoryMapLines)
    {
        // seed-to-soil map:
        // 50 98 2
        // 52 50 48
        // D  S
        var categoryMap = new CategoryMap();

        // Get Name
        var firstLine = categoryMapLines[0];
        var categoryName = firstLine.Split(' ')[0];
        categoryMap.CategoryName = categoryName;
        (string sourceName, string destinationName) names = categoryName.Split('-') switch { var n => ((n[0]), n[^1]) };
        categoryMap.SourceName = names.sourceName;
        categoryMap.DestinationName = names.destinationName;

        var mapping = new Dictionary<long, long>();
        for (var i = 1; i < categoryMapLines.Count; i++)
        {
            var line = categoryMapLines[i];

            var items = line.Split(' ')?.Select(long.Parse)?.ToList();
            var destinationRangeStart = items[0];
            var sourceRangeStart = items[1];
            var rangeLength = items[2];
            // categoryMap.DestinationRangeStart = destinationRangeStart;
            // categoryMap.SourceRangeStart = sourceRangeStart;
            categoryMap.RangeLength += rangeLength;

            // Destination
            var destinationLower = destinationRangeStart;
            // var destinationUpper = destinationRangeStart + rangeLength - 1;
            var destinationRange = Utils.CreateList(destinationLower, rangeLength);
            // categoryMap.DestinationRange = categoryMap.DestinationRange.AddRange(destinationRange);

            // Source
            var sourceLower = sourceRangeStart;
            // var sourceUpper = sourceRangeStart + rangeLength - 1;
            var sourceRange = Utils.CreateList(sourceLower, rangeLength);
            // categoryMap.SourceRange = categoryMap.SourceRange.AddRange(sourceRange);

            // categoryMap.Max = Math.Max(destinationUpper, sourceUpper);

            if (sourceRange.Count == 0 || destinationRange.Count == 0) continue;
            for (var m = 0; m < rangeLength; m++)
            {
                mapping[sourceRange[m]] = destinationRange[m];
            }
        }

        categoryMap.Mapping = mapping;
        Utils.PrintDictionary<long, long>(mapping, logToConsole, logToFile);

        // Fill in the missing?
        // Assume if not there then return same number?
        // var minimum = Math.Min();

        return categoryMap;
    }

    public List<List<string>> GetMaps(string[] lines)
    {
        List<List<string>> categoryMaps = new List<List<string>>();

        List<string> curSet = new List<string>();    
        foreach (var line in lines)
        {
            if (line.Trim().Length == 0)
            {
                if (curSet.Count > 0)
                {
                    categoryMaps.Add(curSet);
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
            categoryMaps.Add(curSet);
        }

        return categoryMaps;
    }

    // public void Part2(string[] lines)
    // {
    // }
}

public class CategoryMap
{
    public string CategoryName = "";

    public string SourceName = "";
    // public long SourceRangeStart = 0;
    // public List<long> SourceRange = new();

    public string DestinationName = "";
    
    // public long DestinationRangeStart = 0;
    // public List<long> DestinationRange = new();

    public long RangeLength = 0;
    // public long Max = 0;

    public Dictionary<long, long> Mapping = new();

    public override string ToString()
    {
        return $"{CategoryName} | SourceName: {SourceName} | DestinationName: {DestinationName}" +
            $" | Range : {RangeLength}" +
            $" | Mapping # : {Mapping.Count}"; // +
            // $" | Max : {Max}" +
            // $" | Source Range: {String.Join(',', SourceRange)} | Destination Range {String.Join(',', DestinationRange)} ";
    }
}

Utils.Log("-- Day 5 --", true, true);
Utils.Log("-----------", true, true);

var day5 = new Day5();

// string fileName = @"input-sample-1.txt";
// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day5.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day5.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();