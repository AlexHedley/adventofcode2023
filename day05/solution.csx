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
        var seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Utils.Log($"Seeds: {String.Join(',', seeds)}", logToConsole, logToFile);
        
        var categoryMapLines = GetMaps(lines.Skip(1).ToArray());
        // Utils.Log($"{categoryMapLines.Count}", logToConsole, logToFile);

        List<CategoryMap> categoryMaps = new();
        foreach(var categoryMap in categoryMapLines)
        {
            var map = ParseMap(categoryMap);
            Utils.Log($"{map}", logToConsole, logToFile);
            categoryMaps.Add(map);
        }
    }

    public CategoryMap ParseMap(List<string> categoryMapLines)
    {
        // seed-to-soil map:
        // 50 98 2
        // 52 50 48
        var categoryMap = new CategoryMap();
        
        for (var i = 0; i < categoryMapLines.Count; i++)
        {
            var line = categoryMapLines[i];

            // Get Name
            if (i == 0)
            {
                var categoryName = line.Split(' ')[0];
                categoryMap.CategoryName = categoryName;
            }

            // Source Category
            if (i == 1)
            {
                categoryMap.SourceCategories = line.Split(' ')?.Select(Int32.Parse)?.ToList();
            }

            // DestinationCategory
            if (i == 2)
            {
                categoryMap.DestinationCategories = line.Split(' ')?.Select(Int32.Parse)?.ToList();
            }
        }

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
    public List<int> SourceCategories = new();
    public List<int> DestinationCategories = new();

    public override string ToString()
    {
        return $"{CategoryName} | SourceCategories: {String.Join(',', SourceCategories)} | DestinationCategories: {String.Join(',', DestinationCategories)}";
    }
}

Utils.Log("-- Day 5 --", true, true);
Utils.Log("-----------", true, true);

var day5 = new Day5();

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