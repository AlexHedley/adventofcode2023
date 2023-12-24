#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.0"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

using System.Drawing;

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

    public void FindIntersection()
    {
        // Hailstone A: 19, 13, 30 @ -2, 1, -2
        // Hailstone B: 18, 19, 22 @ -1, -1, -2
        // Hailstones' paths will cross inside the test area (at x=14.333, y=15.333).
        
        var expected = new PointF(15.4f, 16.4f);

        Func<float, float, PointF> p = (x, y) => new PointF(x, y);
        var actual = day24.FindIntersection(p(19f, 13f), p(1f, 30f), p(18f, 19f), p(2f, 3f));
        actual.Should().Be(expected);
    }

    public void ValidIntersections()
    {
        var p1 = new PointF(14.333333f, 15.333333f); // {X=14.333333, Y=15.333333}
        var p2 = new PointF(float.NaN, float.NaN); // {X=NaN, Y=NaN}
        var p3 = new PointF(-2f, 3f); // {X=-2, Y=3}
        var p4 = new PointF(16f, 39); // {X=16, Y=39}
        var points  = new List<PointF>() { p1, p2, p3, p4 };

        var expected = 1L;
        var actual = day24.ValidIntersections(points);
        actual.Should().Be(expected);
    }
    
}