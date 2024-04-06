namespace IAM.Infrastructure.CodeGenerator;

public class RandomCodeGenerator : ICodeGenerator
{
    public string Generator()
    {
        Random random = new Random();
        int rand = random.Next(10000000,99999999);
        return rand.ToString();
    }
}