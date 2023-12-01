#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day1Tests>().Execute();

public class Day1Tests : IDisposable
{
    public Day1 day1;

    public Day1Tests()
    {
        day1 = new Day1();
    }

    public void Dispose() { }

    // Part 1

    [Arguments("1abc2", "12")]
    [Arguments("pqr3stu8vwx", "38")]
    [Arguments("a1b2c3d4e5f", "12345")]
    [Arguments("treb7uchet", "7")]
    public void GetNumbersFromString(string input, string result)
    {
        var numbersFromString = day1.GetNumbersFromString(input);
        var countOfNumbers = numbersFromString.Count;
        var expectedNumbers = result.Length;
        expectedNumbers.Should().Be(countOfNumbers, "");
    }

    public void JoinFirstAndLast()
    {
        var numbers = new List<string>() { "1", "2" };
        var result = 12;

        var sum = day1.JoinFirstAndLast(numbers);
        
        sum.Should().Be(result);
    }

    public void SumAll()
    {
        var numbers = new List<List<string>>();
        var number1 = new List<string>() { "1", "2" };
        numbers.Add(number1);
        var number2 = new List<string>() { "3", "8" };
        numbers.Add(number2);
        
        var result = 50;

        var sum = day1.SumAll(numbers);
        sum.Should().Be(result);
    }

    // Part 2

    public void StringsToNumbers()
    {
        var input = new List<string>() { "two", "1", "nine" };
        var expectedNumbers = new List<string>() { "2", "1", "9" };

        var numbers = day1.StringsToNumbers(input);
        numbers.Should().BeEquivalentTo(expectedNumbers, "");
    }

    [Arguments("1abc2", "oneabctwo")]
    public void NumbersToStrings(string input, string expected)
    {
        var output = day1.NumbersToStrings(input);
        output.Should().Be(expected);
    }

    [Arguments("oneabctwo", "one")]
    public void FindFirstNumber(string input, string expected)
    {
        string pattern = @"one|two|three|four|five|six|seven|eight|nine";
        var output = day1.FindFirstNumber(input, pattern);
        output.Should().Be(expected);
    }

    [Arguments("oneabctwo", "two")]
    public void FindLastNumber(string input, string expected)
    {
        string pattern = @"one|two|three|four|five|six|seven|eight|nine";
        var output = day1.FindLastNumber(input, pattern);
        output.Should().Be(expected);
    }
}