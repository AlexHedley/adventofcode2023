#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day2
{
    bool logToConsole = false;
    bool logToFile = false;

    public class Game
    {
        public long GameNumber = 0;

        public List<Options> Options = new();

        public override string ToString()
        {
            return $"Game {GameNumber}: {String.Join(" | ", Options)}";
        }

        public bool Equals(Game other)
        {
            return other != null &&
                GameNumber == other.GameNumber &&
                other.Options.Equals(Options);
            // other.Options.SequenceEqual(Options);
            // Options == other.Options;
        }

        public override int GetHashCode()
        {
            return this.GameNumber.GetHashCode();
        }
    }

    public class Options
    {
        public long Red = 0;
        public long Blue = 0;
        public long Green = 0;

        public override string ToString()
        {
            return $"R{Red} G{Green} B{Blue}";
        }

        public bool Equals(Options other)
        {
            return other != null &&
                Red == other.Red &&
                Blue == other.Blue &&
                Green == other.Green;
        }

        public override int GetHashCode()
        {
            // return HashCode.Combine(Red, Blue, Green);
            return (Red, Blue, Green).GetHashCode();
        }
    }

    public Options Max = new Options() { Red = 12, Green = 13, Blue = 14 };

    public void Part1(string[] lines)
    {
        var sum = 0L;
        foreach(var line in lines)
        {
            var game = GetGame(line);
            // Utils.Log(game, logToConsole, logToFile);
            // Console.WriteLine(game);

            var validGame = CheckGame(game);
            Utils.Log($"{game.GameNumber} is valid: '{validGame}'", logToConsole, logToFile);
            if (validGame) sum += game.GameNumber;
        }
        Utils.Log($"{sum}", logToConsole, logToFile);
        Utils.Answer($"{sum}");
    }

    public Game GetGame(string line)
    {
        var game = new Game();

        Utils.Log(line, logToConsole, logToFile);
        // Get "Game #: " and rest
        var pattern = @"(\w* \d+: )(.*)";
        var match = Regex.Match(line, pattern);

        var gameGrouping = match.Groups[1].Value;
        var optionsGrouping = match.Groups[2].Value;

        var gameNumber = Regex.Match(gameGrouping, @"\d+").Value;
        Utils.Log($"Game: '{gameNumber}'", logToConsole, logToFile);
        game.GameNumber = long.Parse(gameNumber);
        
        // Split options by ";"
        var combinations = optionsGrouping.Split(";", StringSplitOptions.TrimEntries);
        foreach(var combination in combinations)
        {
            var option = new Options();

            Utils.Log($"Combination: '{combination}'", logToConsole, logToFile);
            // 3 blue; 2 green, 3 red; 1 red, 2 green, 6 blue

            var coloursGroup = combination.Split(",", StringSplitOptions.TrimEntries);
            foreach(var colourGroup in coloursGroup)
            {
                pattern = @"(\d+) (\w*)";
                match = Regex.Match(colourGroup, pattern);

                var count = match.Groups[1].Value;
                var colour = match.Groups[2].Value;

                Utils.Log($"{colour}: '{count}'", logToConsole, logToFile);
                var number = long.Parse(count);

                var result = colour switch
                {
                    "red" => option.Red = number,
                    "green" => option.Green = number,
                    "blue" => option.Blue = number,
                };
            }
            game.Options.Add(option);
        }

        return game;
    }

    public bool CheckGame(Game game)
    {
        var valid = true;
        foreach(var option in game.Options)
        {
            if (option.Red > Max.Red) valid = false;
            if (option.Green > Max.Green) valid = false;
            if (option.Blue > Max.Blue) valid = false;
        }
        return valid;
    }
}

Utils.Log("-- Day 2 --", true, true);
Utils.Log("-----------", true, true);

var day2 = new Day2();

// string fileName = @"input-sample.txt";
// string fileName = @"input-sample-1.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day2.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day2.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();