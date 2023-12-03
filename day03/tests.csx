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
    // [Arguments('1', false)]
    // [Arguments('2', false)]
    // [Arguments('3', false)]
    // [Arguments('4', false)]
    // [Arguments('5', false)]
    // [Arguments('6', false)]
    // [Arguments('7', false)]
    // [Arguments('8', false)]
    // [Arguments('9', false)]
    // [Arguments('0', false)]
    // [Arguments(default(char), false)]
    public void IsSymbol(char c, bool result)
    {
        var actual = day3.IsSymbol(c);
        actual.Should().Be(result);
    }

    // public void CheckAdjecents()
    // {

    // }

    [Arguments("..35..633.", 2, 35)]
    [Arguments("..35..633.", 3, 35)]
    [Arguments("3.35..633.", 3, 35)]
    [Arguments("..35..633.", 7, 633)]
    [Arguments("..35..633.", 6, 633)]
    [Arguments("..35..633.", 8, 633)]
    [Arguments("...*....*....222..*.........*795..........%...+...#.....54.310.....................622....916.......=......./.........../.......493......956", 137, 956)]
    // [Arguments("...", 1, 0)]
    public void FindNumber(string line, int position, long result)
    {
        var actual = day3.FindNumber(line, position);
        actual.Should().Be(result);
    }
}