#load "../utils/utils.csx"

public class Day15
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var total = 0;

        foreach (var line in lines)
        {
            var items = line.Split(",", StringSplitOptions.TrimEntries).ToList();
            
            foreach (var item in items)
            {
                var calculation = CalculateItem(item);
                total += calculation;
            }
        }

        Utils.Answer($"{total}", logToConsole, logToFile);
        
    }

    public int CalculateItem(string item)
    {
        var total = 0;

        Utils.Log($"{item}", logToConsole, logToFile);

        foreach (var character in item.ToCharArray())
        {
            // Determine the ASCII code for the current character of the string.
            // Increase the current value by the ASCII code you just determined.
            var ascii = (int)character;
            // Utils.Log($"ascii: {ascii}", logToConsole, logToFile);
            total += ascii;
            // Utils.Log($"total: {total}", logToConsole, logToFile);

            // Set the current value to itself multiplied by 17.
            total *= 17;
            // Utils.Log($"total: {total} (*17)", logToConsole, logToFile);

            // Set the current value to the remainder of dividing itself by 256.
            total = total % 256;
            // Utils.Log($"total: {total} (%256)", logToConsole, logToFile);

            // Utils.Log($"{total}", logToConsole, logToFile);
        }

        return total;
    }

    public void Part2(string[] lines)
    {
        foreach (var line in lines)
        {
            var items = line.Split(",", StringSplitOptions.TrimEntries).ToList();
            
            var boxes = CreateBoxes();

            var boxNumber = 0;

            foreach (var item in items)
            {
                Utils.Log($"Item: {item}", logToConsole, logToFile);

                if (item.Contains("="))
                {
                    Utils.Log($"+ Item: {item} to box {boxNumber}", logToConsole, logToFile);
                    // var info = item.Split("=") switch { var n => ( n[0], long.Parse(n[1]) ) };
                    var lens = item.Replace("=", "");
                    boxes[boxNumber].Add(lens);
                }
                else
                {
                    // Contains "-"?

                    var valueToCheck = item.Replace("-", "");

                    var boxesToCheck = boxes.Where(b => b.Value.Count != 0).ToList();
                    Utils.Log($"boxesToCheck: {boxesToCheck.Count}", logToConsole, logToFile);

                    foreach (var boxToCheck in boxesToCheck)
                    {
                        if (boxToCheck.Value.Contains(valueToCheck))
                        {
                            Utils.Log($"Found: {valueToCheck} in {boxToCheck.Key}", logToConsole, logToFile);
                            // TODO: Remove
                            boxes[boxToCheck.Key].Remove(valueToCheck);
                        }
                    }
                }
                
                // boxNumber++;
            }
        }
    }

    public Dictionary<int, List<string>> CreateBoxes()
    {
        var boxes = new Dictionary<int, List<string>>();
        
        for (var i = 0; i < 256; i++)
        {
            boxes[i] = new List<string>();
        }

        return boxes;
    }
}

Utils.Log("-- Day 15 --", true, true);
Utils.Log("-----------", true, true);

var day15 = new Day15();

string fileName = @"input-sample.txt";
// string fileName = @"input-sample-1.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day15.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day15.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();