#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day3Tests>().Execute();

public class Day3Tests : IDisposable
{
    public Day3 day3;

    public Day3Tests()
    {
        day3 = new Day3();
    }

    public void Dispose() { }

    public void FindNumbersInLine()
    {
        var input = "467..114..";
        var expectedNumbers = new List<long>() { 467, 114 };
        var result = day3.FindNumbersInLine(input);
        result.Should().Equal(expectedNumbers);
    }

    public void FindNumbersInLines()
    {
        var input = new string[] { "467..114..", "...*......" };
        var expectedNumbers = new List<long>() { 467, 114 };
        var result = day3.FindNumbersInLines(input);
        result.Should().Equal(expectedNumbers);
    }

    [Arguments('/', true)]
    [Arguments('*', true)]
    [Arguments('-', true)]
    [Arguments('+', true)]
    [Arguments('_', true)]
    [Arguments('@', true)]
    [Arguments('&', true)]
    [Arguments('$', true)]
    [Arguments('#', true)]
    [Arguments('%', true)]
    [Arguments('.', false)]
    public void IsSymbol(char c, bool result)
    {
        var actual = day3.IsSymbol(c);
        actual.Should().Be(result);
    }

    // public void CheckAdjecents()
    // {

    // }
}