using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

[Route("/2021/Day3/Part2")]
public partial class Day3Part2Of2021 : ConsoleComponentBase
{
    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 3 - Part 2", ConsoleColor.DarkMagenta);
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

        var o2candidates = lines.ToList();
        var co2candidates = lines.ToList();

        for (int digit = 0; digit < lines[0].Length; digit++)
        {            
            if (co2candidates.Count > 1)
            {
                var counts = co2candidates.Select(n => n[digit]).GroupBy(m => m).OrderBy(n => n.Count()).ThenBy(n => n.Key == '1').ToList();
                co2candidates.RemoveAll(n => n[digit] != counts.First().Key);
            }

            if (o2candidates.Count > 1)
            {
                var counts = o2candidates.Select(n => n[digit]).GroupBy(m => m).OrderByDescending(n => n.Count()).ThenBy(n => n.Key == '0').ToList();
                o2candidates.RemoveAll(n => n[digit] != counts.First().Key);
            }
        }

        var o2 = Convert.ToInt32(o2candidates.First(), 2);
        var co2 = Convert.ToInt32(co2candidates.First(), 2);
        Console.WriteLine($"Result: {(o2 * co2)} -- O2: {o2}, CO2: {co2}");
    }

}
