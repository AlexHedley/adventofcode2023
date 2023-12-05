#load "../utils/utils.csx"

public class Day5
{
    bool logToConsole = true;
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
                        ?.Select(int.Parse)?.ToList();

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

    public List<int> GetLocations(List<int> seeds, List<CategoryMap> categoryMaps)
    {
        var locations = new List<int>();

        // TODO: Remove
        foreach(var seed in seeds.Take(1))
        {
            // var seed = //79;
            int soil;
            var soilExists = categoryMaps.FirstOrDefault(c => c.SourceName == "seed").Mapping.TryGetValue(seed, out soil);
            if (!soilExists) soil = seed;
            Utils.Info($"Seed: {seed} -> Soil: {soil}");

            int fertilizer;
            var fertilizerExists = categoryMaps.FirstOrDefault(c => c.SourceName == "soil").Mapping.TryGetValue(soil, out fertilizer);
            if (!fertilizerExists) fertilizer = soil;
            Utils.Info($"Soil: {soil} -> Fertilizer: {fertilizer}");

            int water;
            var waterExists = categoryMaps.FirstOrDefault(c => c.SourceName == "fertilizer").Mapping.TryGetValue(fertilizer, out water);
            if (!waterExists) water = fertilizer;
            Utils.Info($"Fertilizer: {fertilizer} -> Water: {water}");

            int light;
            var lightExists = categoryMaps.FirstOrDefault(c => c.SourceName == "water").Mapping.TryGetValue(water, out light);
            if (!lightExists) light = water;
            Utils.Info($"Water: {water} -> Light: {light}");

            int temperature;
            var temperatureExists = categoryMaps.FirstOrDefault(c => c.SourceName == "light").Mapping.TryGetValue(light, out temperature);
            if (!temperatureExists) temperature = light;
            Utils.Info($"Light: {light} -> Temperature: {temperature}");

            int humidity;
            var humidityExists = categoryMaps.FirstOrDefault(c => c.SourceName == "temperature").Mapping.TryGetValue(temperature, out humidity);
            if (!temperatureExists) humidity = temperature;
            Utils.Info($"Temperature: {temperature} -> Humidity {humidity}");

            int location;
            var locationExists = categoryMaps.FirstOrDefault(c => c.SourceName == "humidity").Mapping.TryGetValue(humidity, out location);
            if (!locationExists) location = humidity;
            Utils.Info($"Humidity {humidity} -> Location: {location}");

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
        
        for (var i = 0; i < categoryMapLines.Count; i++)
        {
            var line = categoryMapLines[i];

            // Get Name
            if (i == 0)
            {
                var categoryName = line.Split(' ')[0];
                categoryMap.CategoryName = categoryName;
                (string sourceName, string destinationName) names = categoryName.Split('-') switch { var n => ((n[0]), n[^1]) };
                categoryMap.SourceName = names.sourceName;
                categoryMap.DestinationName = names.destinationName;
            }

            // Source Category
            if (i == 1)
            {
                var items = line.Split(' ')?.Select(int.Parse)?.ToList();
                categoryMap.DestinationRangeStart1 = items[0];
                categoryMap.SourceRangeStart1 = items[1];
                categoryMap.RangeLength1 = items[2];

                // Destination
                var destinationLower = items[0];
                var destinationUpper = items[0] + items[2] - 1;
                var destinationRange = Utils.BoundsRange(destinationLower, destinationUpper);
                categoryMap.DestinationRange1 = destinationRange;

                // Source
                var sourceLower = items[1];
                var sourceUpper = items[1] + items[2] - 1;
                var sourceRange = Utils.BoundsRange(sourceLower, sourceUpper);
                categoryMap.SourceRange1 = sourceRange;

                categoryMap.Max1 = Math.Max(destinationUpper, sourceUpper);
            }

            // DestinationCategory
            if (i == 2)
            {
                var items2 = line.Split(' ')?.Select(int.Parse)?.ToList();
                categoryMap.DestinationRangeStart2 = items2[0];
                categoryMap.SourceRangeStart2 = items2[1];
                categoryMap.RangeLength2 = items2[2];

                // Destination
                var destinationLower2 = items2[0];
                var destinationUpper2 = items2[0] + items2[2] - 1;
                var destinationRange2 = Utils.BoundsRange(destinationLower2, destinationUpper2);
                categoryMap.DestinationRange2 = destinationRange2;

                // Source
                var sourceLower2 = items2[1];
                var sourceUpper2 = items2[1] + items2[2] - 1;
                var sourceRange2 = Utils.BoundsRange(sourceLower2, sourceUpper2);
                categoryMap.SourceRange2 = sourceRange2;
            }
        }

        // Create a mapping
        var mapping = new Dictionary<int, int>();
        for (var m = 0; m < categoryMap.RangeLength1; m++)
        {
            var sourceRangeValue = categoryMap.SourceRange1[m];
            var destinationRangeValue = categoryMap.DestinationRange1[m];
            mapping[sourceRangeValue] = destinationRangeValue;
        }

        for (var m = 0; m < categoryMap.RangeLength2; m++)
        {
            var sourceRangeValue2 = categoryMap.SourceRange2[m];
            var destinationRangeValue2 = categoryMap.DestinationRange2[m];
            mapping[sourceRangeValue2] = destinationRangeValue2;
        }

        categoryMap.Mapping = mapping;
        // Utils.PrintDictionary<int, int>(mapping);

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
    public int SourceRangeStart1 = 0;
    public List<int> SourceRange1 = new();
    public int SourceRangeStart2 = 0;
    public List<int> SourceRange2 = new();

    public string DestinationName = "";
    
    public int DestinationRangeStart1 = 0;
    public List<int> DestinationRange1 = new();

    public int DestinationRangeStart2 = 0;
    public List<int> DestinationRange2 = new();

    public int RangeLength1 = 0;
    public int RangeLength2 = 0;
    public int Max1 = 0;
    public int Max2 = 0;

    public Dictionary<int, int> Mapping = new();

    public override string ToString()
    {
        return $"{CategoryName} | SourceName: {SourceName} | DestinationName: {DestinationName} | " +
            $"Range1 : {RangeLength1} | Max : {Max1} | " +
            $"Source Range: {String.Join(',', SourceRange1)} | Destination Range {String.Join(',', DestinationRange1)} | " +
            $"Range2 : {RangeLength2} | Max2 : {Max2} | " +
            $"Source Range2: {String.Join(',', SourceRange2)} | Destination Range2 {String.Join(',', DestinationRange2)}";
    }
}

Utils.Log("-- Day 5 --", true, true);
Utils.Log("-----------", true, true);

var day5 = new Day5();

// string fileName = @"input-sample-1.txt";
string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day5.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day5.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();