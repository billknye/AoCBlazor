using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day2/Part2")]
public partial class Day2Part2Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 2 - Part 2", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        string[] lines = null;

        while (lines == null)
        {
            var data = await Console.ReadLine();
            lines = data.Replace("\r", "").Split('\n');
        }

        if (lines.Length <= 1)
            return;

        var total = new Point();
        int aim = 0;
        Console.WriteLine("Running...");

        foreach (var a in lines)
        {
            var parsed = Parse(a);
            switch (parsed.direction)
            {
                case Movement.down:
                    aim += parsed.scale;
                    break;
                case Movement.up:
                    aim -= parsed.scale;
                    break;
                case Movement.forward:
                    total += new Point(parsed.scale, parsed.scale * aim);
                    break;
            }    

        }

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
    }
}
