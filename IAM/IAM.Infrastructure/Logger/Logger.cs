using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Logger;

public class Logger : IMLogger
{
    public void Log(string Message,String category)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger(category);
        logger.LogInformation(Message);
    }
}