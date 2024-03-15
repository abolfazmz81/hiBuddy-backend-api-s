namespace hiBuddy.Controllers;

public class Tools
{
    public static void Logger(String message)
    {
        DateTime dateTime = DateTime.Now;
        Console.WriteLine(dateTime + "- " + message);
    }
}