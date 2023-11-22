using UltimateProject.Controller;
using UltimateProject.Model;
using UltimateProject.View;

internal class Runner
{
    private static void Main(string[] args)
    {
        Menu menu = Menu.GetInstance();
        menu.UseMenu();
    }
}