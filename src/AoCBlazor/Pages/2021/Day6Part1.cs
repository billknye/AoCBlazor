using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages._2021;

[Route("/2021/Day6/Part1")]
public class Day6Part1 : ConsoleComponentBase
{
    long[,] ageDayLookup = new long[9, 257];

    protected override async Task Main()
    {
        Console.WriteLine("2021 - Day 6 - Part 1", ConsoleColor.DarkMagenta);
        Console.WriteLine("Paste or drag input data");

        var data = await Console.ReadLine();

        Console.WriteLine("Running...");

        await Task.Delay(1);

        var countsByAge = data.Split(',').Select(n => int.Parse(n)).GroupBy(n => n);

        long total = 0;

        // Build cache
        /*for (int days = 0; days < 160; days++)
        { 
            for (int age = 0; age < 9; age++)
            {            
                SimulateFish(age, days);
            }
        }*/

        foreach (var counts in countsByAge)
        {
            Console.WriteLine($"Days: {counts.Key}, Count: {counts.Count()}");
            await Task.Delay(1);


            var count = SimulateFish(counts.Key, 80);
            Console.WriteLine($"\tTotal after 80 days: {count}");
            await Task.Delay(1);

            total += count * counts.Count();
        }

        Console.WriteLine($"Total: {total}");
    }

    public long SimulateFish(int age, int days)
    {
        var cache = ageDayLookup[age, days];
        if (cache != 0)
            return cache;        

        long totalFish = 1;

        int a = age;
        for (int d = days; d > 0; d--)
        {
            a--;
            if (a < 0)
            {
                totalFish += SimulateFish(8, d - 1);
                a = 6;
            }
        }

        ageDayLookup[age, days] = totalFish;
        return totalFish;
    }
}
