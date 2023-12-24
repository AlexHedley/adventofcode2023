#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day24Tests>().Execute();

public class Day24Tests : IDisposable
{
    public Day24 day24;

    public Day24Tests()
    {
        day24 = new Day24();
    }

    public void Dispose() { }

    // public void ParseInput()
    // {
    //     string[] lines = new string[] { "19, 13, 30 @ -2, 1, -2" };
    //     var data1 = new Day24.Data() { Position = (19, 13, 30), Velocity = (-2, 1, -2) };
    //     var expected = new List<Day24.Data>() { data1 };

    //     var actual = day24.ParseInput(lines);
    //     actual.Should().Be(expected);
    // }

    public void CalculateNewPosition()
    {
        (long x, long y, long z) position = (20, 19, 15);
        (long x, long y, long z) velocity = (1, -5, -3);
        var expected = (21, 14, 12);

        var actual = day24.CalculateNewPosition(position, velocity);
        actual.Should().Be(expected);
    }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}