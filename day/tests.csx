#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests>().Execute();

public class DayTests : IDisposable
{
    public Day day;

    public DayTests()
    {
        day = new Day();
    }

    public void Dispose() { }

    // public void Success()
    // {
    //     "Ok".Should().Be("Ok");
    // }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}