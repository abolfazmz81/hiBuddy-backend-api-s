namespace IAM.Application.AuthenticationService;

public record LoginDetails(
    String email,
    String pass);