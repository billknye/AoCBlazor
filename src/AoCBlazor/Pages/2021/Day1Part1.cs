using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day1/Part1")]
public partial class Day1Part1Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 1 - Part 1", ConsoleColor.DarkMagenta);
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

        var depths = lines.Select(n => int.Parse(n)).ToList();

        var increases = depths.Where((n, i) => i > 0 && n > depths[i - 1]).Count();

        Console.WriteLine($"Result: {increases}");
    }
}
