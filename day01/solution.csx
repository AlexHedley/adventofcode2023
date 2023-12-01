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
    
    public void Part2(string[] lines)
    {
        var total = 0;

        foreach(var line in lines)
        {
            var initialNumbers = GetNumbersFromString2(line);
            
            initialNumbers.ForEach(n => Utils.Log(n, logToConsole, logToFile));
            Utils.Log("--- ---", logToConsole, logToFile);

            var numbers = StringsToNumbers(initialNumbers);
            numbers.ForEach(n => Utils.Log(n, logToConsole, logToFile));

            var sum = JoinFirstAndLast(numbers);
            Utils.Log($"sum:'{sum}'", logToConsole, logToFile);
            Utils.Log($"", logToConsole, logToFile);

            total += sum;
        }

        Utils.Log($"{total}", logToConsole, logToFile);
        Utils.Answer($"{total}");
    }

    public List<string> GetNumbersFromString2(string input)
    {
        Utils.Log($"{input}", logToConsole, logToFile);

        var i = -1;
        var skipTo = 0;

        var numbers = new List<string>();
        
        foreach (char c in input)
        {
            i++;
            Utils.Log($"c:{c} | i:{i} | skipTo:{skipTo}", logToConsole, logToFile);

            if (i != skipTo) continue;            
            
            if (Char.IsDigit(c))
            {
                Utils.Log("Digit Found", logToConsole, logToFile);
                numbers.Add(Char.ToString(c));
                skipTo = skipTo+1;
            }
            else
            {
                Utils.Log($"Checking letter: '{c}'", logToConsole, logToFile);
                
                var matches = numbersAsWords.Where(number => number[0] == c);
                
                Utils.Log($"input:'{input}' {skipTo} {input.Length - 1}", logToConsole, logToFile);
                var remainingInput = input.Substring(skipTo);

                if (matches.Any())
                {
                    var matched = false;
                    foreach (var match in matches)
                    {
                        if (matched) continue;

                        Utils.Log($"Checking match:'{match}' against remainingInput:'{remainingInput}' (input:'{input}')", logToConsole, logToFile);
                        
                        if (remainingInput.Contains(match))
                        {
                            Utils.Log($"'{remainingInput}' Matches '{match}' ('{input}')", logToConsole, logToFile);

                            var length = 0;
                            foreach(var number in numbers)
                            {
                                length += number.Length;
                            }

                            Utils.Log($"i:'{i}' | match.Length:'{match.Length}'", logToConsole, logToFile);
                            var index = i + match.Length;
                            Utils.Log($"Index: {index}", logToConsole, logToFile);

                            numbers.Add(match);
                            skipTo = index;

                            Utils.Log($"i:{i} | skipTo:{skipTo}", logToConsole, logToFile);
                            matched = true;
                            continue;
                        }
                        else 
                        {
                            Utils.Log($"input doesn't contain match", logToConsole, logToFile);
                            // continue;
                        }
                    }
                    if (!matched) skipTo = skipTo+1;
                }
                else
                {
                    Utils.Log($"No matches for {c}", logToConsole, logToFile);
                    skipTo = skipTo+1;
                }
            }
        }

        return numbers;
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
    private static Dictionary<string,long> numberTable=
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