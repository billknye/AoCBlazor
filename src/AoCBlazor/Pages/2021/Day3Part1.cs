using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day3/Part1")]
public partial class Day3Part1Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 3 - Part 1", ConsoleColor.DarkMagenta);
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

        var gamma = string.Empty;
        var epsilon = string.Empty;

        for (int digit = 0; digit < lines[0].Length; digit++)
        {
            var counts = lines.Select(n => n[digit]).GroupBy(m => m).OrderBy(n => n.Count());

            epsilon += (counts.First().Key);
            gamma += (counts.Skip(1).First().Key);
        }

        var total = Convert.ToInt32(epsilon, 2) * Convert.ToInt32(gamma, 2);
        Console.WriteLine($"Result: {total}");
    }

}
