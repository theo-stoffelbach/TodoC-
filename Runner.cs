using UltimateProject.Controller;
using UltimateProject.test_unit;

internal class Runner
{
    private static void Main(string[] args)
    {
        //TestTodo.TestProtocol();

        Menu menu = Menu.GetInstance();
        menu.UseMenu();
    }
}