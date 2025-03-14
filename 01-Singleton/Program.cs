namespace SingletonDemo;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Singleton Pattern Demo\n");

        var config1 = ConfigurationManager.Instance;
        config1.SetValue("DatabaseConnection", "Server=myServerAddress;Database=myDataBase;");

        var config2 = ConfigurationManager.Instance; //Should get same instance

        Console.WriteLine($"Database Connection: {config2.GetValue("DatabaseConnection")}");
        Console.WriteLine($"Same instance: {ReferenceEquals(config1, config2)}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
