using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day2/Part1")]
public partial class Day2Part1Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 2 - Part 1", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        string[] lines = null;

        while (lines == null)
        {
            var data = await Console.ReadLine();
            lines = data.Replace("\r", "").Split('\n');
        }

        if (lines.Length <= 1)
            return;

        Console.WriteLine("Running...");

        var total = lines.Select(Parse).Aggregate(new Point(), (a, b) => a += MovementOffset[(int)b.direction] * b.scale);

        Console.WriteLine($"Result: {total.X * total.Y}");

    }

    static (Movement direction, int scale) Parse(string line)
    {
        var pts = line.Split(' ');
        return ((Enum.Parse<Movement>(pts[0]), int.Parse(pts[1])));
    }

    enum Movement
    {
        forward,
        down,
        up
    }

    static Point[] MovementOffset = new Point[]
    {
        new Point(1, 0),
        new Point(0, 1),
        new Point(0, -1)
    };

    struct Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator *(Point left, int scale)
        {
            return new Point(left.X * scale, left.Y * scale);
        }
    }
}
