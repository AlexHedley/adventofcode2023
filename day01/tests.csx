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

    public void GetNumbersFromString2()
    {
        var input = "two1nine";
        var numbersFromString = day1.GetNumbersFromString2(input);
        
        var expectedNumbers = new List<string>() { "two", "1", "nine" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "eightwothree";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "eight", "three" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "abcone2threexyz";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "one", "2", "three" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "xtwone3four";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "two", "3", "four" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "4nineeightseven2";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "4", "nine", "eight", "seven", "2" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "zoneight234";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "one", "2", "3", "4" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "7pqrstsixteen";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "7", "six" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");

        //

        input = "twotwo98three71four";
        numbersFromString = day1.GetNumbersFromString2(input);

        expectedNumbers = new List<string>() { "two", "two", "9", "8", "three", "7", "1", "four" };
        numbersFromString.Should().BeEquivalentTo(expectedNumbers, "");
    }

    // [ArgumentsSource(nameof(TestData))]
    // public void GetNumbersFromString2(string input, List<int> expected)
    // {
    //     var numbersFromString = day1.GetNumbersFromString2(input);
    //     numbersFromString.Should().BeEquivalentTo(expected, "");
    // }

    // public IEnumerable<object[]> TestData
    // {
    //     var items = new IEnumerable<object[]>();
    //     var item1 = new object[] { "two1nine", new List<int>() { "two", "1", "nine" } };
    //     items.Add(item1);

    //     return items;  
    // }

    public void StringsToNumbers()
    {
        var input = new List<string>() { "two", "1", "nine" };
        var expectedNumbers = new List<string>() { "2", "1", "9" };

        var numbers = day1.StringsToNumbers(input);
        numbers.Should().BeEquivalentTo(expectedNumbers, "");
    }
}