namespace IAM.Contracts.Authentication;

public record SignupAllDetails
(
String username,
long phone_number,
String name,     
String password,
String email     
    );