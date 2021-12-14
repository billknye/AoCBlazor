using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages._2021;

[Route("/2021/Day5/Part1")]
public partial class Day5Part1Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 5 - Part 1", ConsoleColor.DarkMagenta);
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

        int[,] map = new int[1024, 1024];
        var linesSegments = lines.Select(n => Line.Parse(n)).ToList();

        foreach (var line in linesSegments)
        {
            if (line.IsStraight)
            {
                line.Plot(p =>
                {
                    map[p.X, p.Y]++;
                    });
            }
        }

        var moreThanOne = 0;
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] > 1)
                {
                    moreThanOne++;
                }
            }

        }

        Console.WriteLine($"Result: {moreThanOne}");

    }



    public struct Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public static Line Parse(string input)
        {
            var points = input.Split(" -> ");
            return new Line(Point.Parse(points[0]), Point.Parse(points[1]));
        }

        public bool IsStraight => Start.X == End.X || Start.Y == End.Y;

        public int Length => Math.Max(Math.Abs(End.X - Start.X), Math.Abs(End.Y - Start.Y));

        public void Plot(Action<Point> plotter)
        {
            var xs = Math.Sign(End.X - Start.X);
            var ys = Math.Sign(End.Y - Start.Y);

            int dx = Start.X;
            int dy = Start.Y;
            for (int d = 0; d <= Length; d++)
            {
                plotter(new Point(dx, dy));

                dx += xs;
                dy += ys;
            }
        }

        public override string ToString()
        {
            return $"{Start} -> {End}";
        }
    }

    public struct Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x; Y = y;
        }

        public static Point Parse(string commaSeparatedCoords)
        {
            var pts = commaSeparatedCoords.Split(',');
            return new Point(int.Parse(pts[0]), int.Parse(pts[1]));
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

}
