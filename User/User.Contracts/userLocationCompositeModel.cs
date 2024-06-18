using User.Domain;

namespace User.Contracts;

public class userLocationCompositeModel
{
    public Domain.User User { get; set; }
    public locations? Locations { get; set; }
}