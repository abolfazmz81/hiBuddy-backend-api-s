using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public record AuthResult(
    User User,
    String Token
    );