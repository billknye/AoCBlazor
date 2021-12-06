using Microsoft.AspNetCore.Components;

namespace AoCBlazor.Pages;

public abstract class ConsoleComponentBase : ComponentBase
{
    [Inject]
    protected ConsoleEx Console { get; set; }

    [Inject]
    protected NavigationManager navigation { get; set; }

    System.Timers.Timer? timer;

    protected override void OnInitialized()
    {
        timer = new System.Timers.Timer(1);
        timer.Elapsed += Timer_Elapsed;
        timer.Enabled = true;
        base.OnInitialized();
    }

    private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        timer!.Enabled = false;
        timer.Elapsed -= Timer_Elapsed;
        timer.Dispose();
        timer = null;

        await Main();
        await AfterMain();
    }

    protected abstract Task Main();

    protected virtual async Task AfterMain()
    {
        Console.WriteLine("[Press Enter To Return to Main Menu]", ConsoleColor.Cyan);
        _ = await Console.ReadLine();

        navigation.NavigateTo("/");
    }
}