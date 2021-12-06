using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace AoCBlazor.Pages;


[Route("/")]
public class Index : ConsoleComponentBase
{
    protected override async Task Main()
    {
        while (true)
        {
            Console.WriteLine("Advent Of Code -- Blazor", ConsoleColor.DarkMagenta);
            Console.WriteLine("Main Menu");

            var routes = GetRoutesToRender(Assembly.GetExecutingAssembly());

            for (int i = 0; i < routes.Count; i++)
            {
                Console.WriteLine($"{i,3} - Route: {routes[i]}");
            }

            var option = await Console.ReadLine();

            if (int.TryParse(option, out var index))
            {
                if (index >= 0 || index < routes.Count)
                {
                    navigation.NavigateTo(routes[index], false);
                    return;
                }
            }
        }
    }

    public static List<string> GetRoutesToRender(Assembly assembly)
    {
        // Get all the components whose base class is ComponentBase
        var components = assembly
            .ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

        var routes = components
            .Select(component => GetRouteFromComponent(component))
            .Where(config => config is not null && config != "/")
            .OrderBy(n => n)
            .ToList();

        return routes;
    }

    private static string GetRouteFromComponent(Type component)
    {
        var attributes = component.GetCustomAttributes(inherit: true);

        var routeAttribute = attributes.OfType<RouteAttribute>().FirstOrDefault();

        if (routeAttribute is null)
        {
            // Only map routable components
            return null;
        }

        var route = routeAttribute.Template;

        if (string.IsNullOrEmpty(route))
        {
            throw new Exception($"RouteAttribute in component '{component}' has empty route template");
        }

        // Doesn't support tokens yet
        if (route.Contains('{'))
        {
            throw new Exception($"RouteAttribute for component '{component}' contains route values. Route values are invalid for prerendering");
        }

        return route;
    }

    protected override async Task AfterMain()
    {
        // Do nothing
    }

}
