using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages._2021;

[Route("/2021/Day7/Part1")]
public class Day7Part1 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 7 - Part 1", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        var data = await Console.ReadLine();

        Console.WriteLine("Running...");

        await Task.Delay(1);

        var horizonalPositions = data.Split(',').Select(n => int.Parse(n));

        var max = horizonalPositions.Max();

        var min = Enumerable
            .Range(0, max)
            .Select(n => (Position: n, Sum: horizonalPositions.Sum(m => Math.Abs(n - m))))
            .OrderBy(n => n.Sum).First();

        Console.WriteLine($"Result Position: {min.Position}, Total: {min.Sum}");

    }
}
