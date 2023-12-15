#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day15Tests>().Execute();

public class Day15Tests : IDisposable
{
    public Day15 day15;

    public Day15Tests()
    {
        day15 = new Day15();
    }

    public void Dispose() { }

    public void CalculateItem()
    {
        var input = "HASH";
        var expected = 52;
        var actual = day15.CalculateItem(input);

        actual.Should().Be(expected);
    }

    // public void Success()
    // {
    //     "Ok".Should().Be("Ok");
    // }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}