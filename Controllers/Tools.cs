using Microsoft.Extensions.Logging;

namespace hiBuddy.Controllers;

public class Tools
{
 

  
    public static void Logger(String message,String category)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger(category);
        logger.LogInformation(message);
    }
}