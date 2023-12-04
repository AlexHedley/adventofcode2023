#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day4
{
    bool logToConsole = false;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var total = ParseLines(lines);
        Utils.Answer($"{total}");
    }

    long ParseLines(string[] lines)
    {
        var total = 0L;

        foreach (var line in lines)
        {
            var calc = ParseLine(line);
            total += calc;
        }

        return total;
    }

    long ParseLine(string line)
    {
        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        var cardInfo = line.Split(":", StringSplitOptions.TrimEntries);
        var cardDetails = cardInfo[0];
        var cardNumber = Regex.Match(cardDetails, @"\d+").Value;
        Utils.Log($"Card {cardNumber}", logToConsole, logToFile);

        var cardNumbers = cardInfo[1];
        (string winning, string chosen) numbers = cardNumbers.Split('|', StringSplitOptions.TrimEntries) switch { var a => (a[0], a[1]) };
        var winningNumbers = numbers.winning.Split(' ', StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        var chosenNumbers = numbers.chosen.Split(' ', StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

        Utils.Log($"Winning", logToConsole, logToFile);
        // winningNumbers.ForEach(Console.WriteLine);
        Utils.Log($"Chosen", logToConsole, logToFile);
        // chosenNumbers.ForEach(Console.WriteLine);

        var result = winningNumbers.Intersect(chosenNumbers).ToList();
        Utils.Log($"Intersect", logToConsole, logToFile);
        // result.ForEach(Console.WriteLine);
        var resultCount = result.Count();
        // var calc = Convert.ToInt64(Math.Pow(2, resultCount - 1));
        var calc = CalculateTotal(resultCount);
        Utils.Info($"{calc}");

        return calc;
    }

    public long CalculateTotal(long number)
    {
        // A1 = 2, B2 = 3
        // =A1*2^B2
        return Convert.ToInt64(Math.Pow(2, number - 1));
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 4 --", true, true);
Utils.Log("-----------", true, true);

var day4 = new Day4();

// string fileName = @"input-sample-1.txt";
// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day4.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day4.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();