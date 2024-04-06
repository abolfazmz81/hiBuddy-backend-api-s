namespace IAM.Contracts.Authentication;

public record SignupAllDetails
(
String username,
long phone_number,
String? name,     
 String? password,
 String? job,
String? education,      
String? pic,     
String? email,      
String? hobbies,
String? favorites,       
String? Date,
String? gender
    );