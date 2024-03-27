using IAM.Application.common;
using IAM.Infrastructure.CodeGenerator;
using IAM.Infrastructure.Logger;

namespace IAM.Infrastructure.InMemoryRepository;

public class InMemoryRepository : IInMemoryRepository
{
    private readonly IInmemoryContext _db;
    private readonly ICodeGenerator _codeGenerator;
    private readonly IMLogger _logger;

    public InMemoryRepository(IInmemoryContext db, ICodeGenerator codeGenerator, IMLogger logger)
    {
        _db = db;
        _codeGenerator = codeGenerator;
        _logger = logger;
    }

    public void Add(string key)
    {
        String code = _codeGenerator.Generator();
        _db.Set(key,code);
        _logger.Log("new code '" + code + "' generated for sign up","AuthPhoneRegister");
    }

    public String? Get(string key)
    {
        return  _db.Get(key);
    }
}