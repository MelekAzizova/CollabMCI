using Core.Models;

namespace UserInterface1;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(CompanyService.RegisterUser("f1", "f2", "f3", "f4"));
    }
}