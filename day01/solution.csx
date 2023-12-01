#load "../utils/utils.csx"

public class Day1
{
    public void Part1(string[] lines)
    {
        var lineNumbers = LineNumbers(lines);
        var sum = SumAll(lineNumbers);
        Console.WriteLine(sum);
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
        var firstNumber = numbers.First();
        var lastNumber = numbers.Last();

        return int.Parse(firstNumber + lastNumber);
    }
}

Console.WriteLine("-- Day 1 --");

var day1 = new Day1();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day1.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");
// day1.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();