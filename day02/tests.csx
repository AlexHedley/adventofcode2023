#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day2Tests>().Execute();

public class Day2Tests : IDisposable
{
    public Day2 day2;

    public Day2Tests()
    {
        day2 = new Day2();
    }

    public void Dispose() { }

    public void GetGame()
    {
        var input = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
        var expected = new Day2.Game();
        expected.GameNumber = 1;
        var options1 = new Day2.Options() { Red = 4, Green = 0, Blue = 3 };
        expected.Options.Add(options1);
        var options2 = new Day2.Options() { Red = 1, Green = 2, Blue = 6 };
        expected.Options.Add(options2);
        var options3 = new Day2.Options() { Red = 0, Green = 2, Blue = 0 };
        expected.Options.Add(options3);

        var result = day2.GetGame(input);
        // result.Should().Be(expected);
        result.GameNumber.Should().Be(1);
    }

    public void CheckGame()
    {
        var option = new Day2.Options() { Red = 1, Green = 1, Blue = 1 };
        var game = new Day2.Game();
        game.Options.Add(option);
        
        var result = day2.CheckGame(game);
        result.Should().BeTrue();
    }

    public void CheckMaxColours()
    {
        var input = new Day2.Game();
        input.GameNumber = 1;
        var options1 = new Day2.Options() { Red = 4, Green = 0, Blue = 3 };
        input.Options.Add(options1);
        var options2 = new Day2.Options() { Red = 1, Green = 2, Blue = 6 };
        input.Options.Add(options2);
        var options3 = new Day2.Options() { Red = 0, Green = 2, Blue = 0 };
        input.Options.Add(options3);

        var expected = new Day2.Options() { Red = 4, Green = 2, Blue = 6 };

        var result = day2.CheckMaxColours(input);
        // result.Should().Be(expected);
        result.Red.Should().Be(expected.Red);
        result.Blue.Should().Be(expected.Blue);
        result.Green.Should().Be(expected.Green);
    }

    public void TotalOfOptions()
    {
        var input = new Day2.Options() { Red = 4, Green = 2, Blue = 6 };
        var expected = 48;

        var result = day2.TotalOfOptions(input);
        result.Should().Be(expected);
    }
}