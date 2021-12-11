using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day1/Part2")]
public partial class Day1Part2Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 1 - Part 2", ConsoleColor.DarkMagenta);
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

        var increases2 = depths.Where((n, i) => i > 0 && i < depths.Count - 2 && GroupedDepth(depths, i) > GroupedDepth(depths, i - 1)).Count();

        Console.WriteLine($"Result: {increases2}");
    }


    static int GroupedDepth(List<int> depths, int start) => depths.Skip(start).Take(3).Sum();
}
