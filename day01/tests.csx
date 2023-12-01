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

    // [Arguments(new List<int>() { 1, 2 }, 3)]
    // [Arguments(new List<int>() { 3, 8 }, 11)]
    // [Arguments(new List<int>() { 1, 2, 3, 4, 5 }, 6)]
    // [Arguments(new List<int>() { 7 }, 14)]
    // public void SumFirstAndLast(List<int> numbers, int result)
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

}