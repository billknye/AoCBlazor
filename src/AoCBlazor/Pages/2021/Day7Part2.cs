using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages._2021;

[Route("/2021/Day7/Part2")]
public class Day7Part2 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 7 - Part 2", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        var data = await Console.ReadLine();

        Console.WriteLine("Running...");

        await Task.Delay(1);

        var horizonalPositions = data.Split(',').Select(n => int.Parse(n));

        var max = horizonalPositions.Max();

        var min = Enumerable
            .Range(0, max)
            .Select(n => (Position: n, Sum: horizonalPositions.Sum(m => GetFuelCost(n, m))))
            .OrderBy(n => n.Sum).First();

        Console.WriteLine($"Result Position: {min.Position}, Total: {min.Sum}");

    }

    public static int GetFuelCost(int start, int end)
    {
        var total = 0;
        for (int i = 0; i < Math.Abs(end - start); i++)
        {
            total += (i + 1);
        }

        return total;
    }
}
