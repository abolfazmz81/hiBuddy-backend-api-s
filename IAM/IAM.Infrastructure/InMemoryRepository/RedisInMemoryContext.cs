using StackExchange.Redis;

namespace IAM.Infrastructure.InMemoryRepository;

public class RedisInMemoryContext : IInmemoryContext
{
    static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
    static IDatabase db = redis.GetDatabase();
    public async Task Set(string key, string value)
    {
        TimeSpan timeSpan = TimeSpan.FromMinutes(1);
        db.StringSet(key, value,timeSpan);
    }

    public async Task<String?> Get(string key)
    {
        String? check = db.StringGet(key);
        return check;
    }
}