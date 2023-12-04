#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day4Tests>().Execute();

public class Day4Tests : IDisposable
{
    public Day4 day4;

    public Day4Tests()
    {
        day4 = new Day4();
    }

    public void Dispose() { }

    [Arguments(4, 8)]
    [Arguments(2, 2)]
    [Arguments(1, 1)]
    [Arguments(0, 0)]
    public void CalculateTotal(long input, long result)
    {
        var actualResult = day4.CalculateTotal(input);
        actualResult.Should().Be(result);
    }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}