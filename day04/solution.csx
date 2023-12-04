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
        var game = BuildGame(line);
        var result = Total(game);
        var calc = CalculateTotal(result);
        Utils.Info($"{calc}");

        return calc;
    }

    long Total((string gameNumber, List<string> winningNumbers, List<string> chosenNumbers) game)
    {
        var result = game.winningNumbers.Intersect(game.chosenNumbers).ToList();
        // Utils.Log($"Intersect", logToConsole, logToFile);
        // result.ForEach(Console.WriteLine);
        var resultCount = result.Count();

        return resultCount;
    }

    (string gameNumber, List<string> winningNumbers, List<string> chosenNumbers) BuildGame(string line)
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

        // Utils.Log($"Winning", logToConsole, logToFile);
        // winningNumbers.ForEach(Console.WriteLine);
        // Utils.Log($"Chosen", logToConsole, logToFile);
        // chosenNumbers.ForEach(Console.WriteLine);

        (string gameNumber, List<string> winningNumbers, List<string> chosenNumbers) game = (cardNumber, winningNumbers, chosenNumbers);
        
        return game;
    }

    public long CalculateTotal(long number)
    {
        // A1 = 2, B2 = 3
        // =A1*2^B2
        return Convert.ToInt64(Math.Pow(2, number - 1));
    }

    public void Part2(string[] lines)
    {
        var total = 0L;

        var cardsCount = lines.Length;
        Utils.Log($"Card #: {cardsCount}", logToConsole, logToFile);

        // Create a dict to store all card counts.
        Dictionary<int, int> gameTotals = new Dictionary<int, int>(cardsCount);
        for (var i = 1; i < cardsCount + 1; i++)
        {
            gameTotals[i] = 1;
        }
        PrintDict(gameTotals);

        foreach (var line in lines)
        {
            Utils.Log(line, logToConsole, logToFile);
            var game = BuildGame(line);
            var gameNum = int.Parse(game.gameNumber);
            
            var result = Total(game);
            Utils.Log($"Game #: {gameNum} with {result} matching", logToConsole, logToFile);
            if (result < 1) continue;

            var gameNumber = int.Parse(game.gameNumber);
            
            var cards = Cards(gameNumber, (int)result);
            foreach(var card in cards)
            {
                if (card > cardsCount) break;
                gameTotals[card] += 1;
            }

            var toCheck = gameTotals[gameNum] - 1;
            
            while (toCheck > 0)
            {
                foreach (var card in cards)
                {
                    if (card > cardsCount) break;
                    gameTotals[card] += 1;
                }
                toCheck--;
            }
        }

        total = gameTotals.Sum(t => t.Value);
        Utils.Answer($"{total}");
    }

    void PrintDict(Dictionary<int, int> dict)
    {
        foreach (var item in dict)
        {
            Utils.Log($"Key {item.Key}: Value {item.Value}", logToConsole, logToFile);
        }
    }

    public List<int> Cards(int cardNumber, int numberOfCards)
    {
        var lower = cardNumber + 1;
        var upper = lower + numberOfCards - 1;

        Utils.Log($"Lower {lower} | Upper {upper}", logToConsole, logToFile);
        var cards = Utils.BoundsRange(lower, upper);
        // cards.ForEach(Console.WriteLine);
        
        return cards;
    }
}

Utils.Log("-- Day 4 --", true, true);
Utils.Log("-----------", true, true);

var day4 = new Day4();

// string fileName = @"input-sample-1.txt";
// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day4.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day4.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();