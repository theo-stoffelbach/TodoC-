using UltimateProject.Controller;
using UltimateProject.test_unit;

internal class Runner
{
    /// <summary>
    /// The main function of the program
    /// 
    /// you have 
    /// - TestRunner to test the program
    /// - and Menu to use the program
    /// 
    /// </summary>
    /// <param name="args"> arg when we lunch the program </param>
    private static void Main(string[] args)
    {
        //TestRunner.TestProtocol();

        Menu menu = Menu.GetInstance();
        menu.UseMenu();
    }
}