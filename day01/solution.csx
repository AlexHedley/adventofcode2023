#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day1
{
    bool logToConsole = false;
    bool logToFile = false;

    public void Part1(string[] lines)
    {
        var lineNumbers = LineNumbers(lines);
        var sum = SumAll(lineNumbers);

        Utils.Log($"{sum}", logToConsole, logToFile);
        Utils.Answer($"{sum}");
    }

    public List<string> GetNumbersFromString(string input)
    {
        var numbers = new List<string>();
        foreach (char c in input)
        {
            if (Char.IsDigit(c))
            {
                numbers.Add(Char.ToString(c));
            }
        }
        return numbers;
    }

    public List<List<string>> LineNumbers(string[] lines)
    {
        var lineNumbers = new List<List<string>>();
        foreach(var line in lines)
        {
            var numbers = GetNumbersFromString(line);
            lineNumbers.Add(numbers);
        }

        return lineNumbers;
    }

    public int SumAll(List<List<string>> numbers)
    {
        var total = 0;
        foreach(var number in numbers)
        {
            var joined = JoinFirstAndLast(number);
            total += joined;
        }
        
        return total;
    }

    public int JoinFirstAndLast(List<string> numbers)
    {
        if (!numbers.Any()) return 0;

        var firstNumber = numbers.First();
        var lastNumber = numbers.Last();

        return int.Parse(firstNumber + lastNumber);
    }

    string[] numbersAsWords = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    string pattern = @"one|two|three|four|five|six|seven|eight|nine";
    
    public void Part2(string[] lines)
    {
        var total = 0;
        
        var numbersAsStrings = new List<string>();
        foreach(var line in lines)
        {
            Utils.Log($"{line}", logToConsole, logToFile);
            var input = NumbersToStrings(line);
            Utils.Log($"{input}", logToConsole, logToFile);

            var first = FindFirstNumber(input, pattern);
            var last = FindLastNumber(input, pattern);

            Utils.Log($"{first}{last}", logToConsole, logToFile);
            var firstNumber = ToLong(first);
            var lastNumber = ToLong(last);
            Utils.Log($"{firstNumber}{lastNumber}", logToConsole, logToFile);
            numbersAsStrings.Add($"{firstNumber}{lastNumber}");
        }

        foreach(var numberAsString in numbersAsStrings)
        {
            var number = int.Parse(numberAsString);
            total += number;
        }

        Utils.Log($"{total}", logToConsole, logToFile);
        Utils.Answer($"{total}");
    }

    public string FindFirstNumber(string input, string pattern)
    {
        var first = "";
        Regex firstRegex = new Regex(pattern, RegexOptions.IgnoreCase);
        Match firstMatch = firstRegex.Match(input);
        while (firstMatch.Success)
        {
            Utils.Log($"{firstMatch.Value}", logToConsole, logToFile);
            first = firstMatch.Value;
            break;
        }

        return first;
    }

    public string FindLastNumber(string input, string pattern)
    {
        var last = "";
        Regex lastRegex = new Regex(pattern, RegexOptions.RightToLeft);
        Match lastMatch = lastRegex.Match(input);
        while (lastMatch.Success)
        {
            Utils.Log($"{lastMatch.Value}", logToConsole, logToFile);
            last = lastMatch.Value;
            break;
        }

        return last;
    }

    public string NumbersToStrings(string input)
    {
        foreach (char c in input)
        {
            if (Char.IsDigit(c))
            {
                var name = numberTable.FirstOrDefault(n => n.Value == c - '0').Key;
                var index = input.IndexOf(c);
                StringBuilder sb = new StringBuilder(input);
                sb.Remove(index, 1);
                sb.Insert(index, name);
                input = sb.ToString();
            }
        }

        return input;
    }

    public List<string> StringsToNumbers(List<string> strings)
    {
        var numbers = new List<string>();
        
        foreach (var number in strings)
        {
            var isNumeric = long.TryParse(number, out _);
            if (!isNumeric)
            {
                var stringToNumber = ToLong(number);
                numbers.Add(stringToNumber.ToString());
            }
            else
            {
                numbers.Add(number);
            }
        }
        
        return numbers;
    }

    // https://stackoverflow.com/a/11278412
    private static Dictionary<string,long> numberTable =
        new Dictionary<string,long>
            { {"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},
            {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9},
            {"ten",10},{"eleven",11},{"twelve",12},{"thirteen",13},
            {"fourteen",14},{"fifteen",15},{"sixteen",16},
            {"seventeen",17},{"eighteen",18},{"nineteen",19},{"twenty",20},
            {"thirty",30},{"forty",40},{"fifty",50},{"sixty",60},
            {"seventy",70},{"eighty",80},{"ninety",90},{"hundred",100},
            {"thousand",1000},{"million",1000000},{"billion",1000000000},
            {"trillion",1000000000000},{"quadrillion",1000000000000000},
            {"quintillion",1000000000000000000} };
    public static long ToLong(string numberString)
    {
        var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
            .Select(m => m.Value.ToLowerInvariant())
            .Where(v => numberTable.ContainsKey(v))
            .Select(v => numberTable[v]);
        
        long acc = 0,total = 0L;
        
        foreach(var n in numbers)
        {
            if(n >= 1000)
            {
                total += (acc * n);
                acc = 0;
            }
            else if(n >= 100){
                acc *= n;
            }
            else acc += n;          
        }

        return (total + acc)  * ( numberString.StartsWith("minus",
            StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
    }
}

Utils.Log("-- Day 1 --", true, true);
Utils.Log("-----------", true, true);

var day1 = new Day1();

// string fileName = @"input-sample.txt";
// string fileName = @"input2-sample.txt";
// string fileName = @"input3-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day1.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day1.Part2(lines);

Console.WriteLine("");
Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();