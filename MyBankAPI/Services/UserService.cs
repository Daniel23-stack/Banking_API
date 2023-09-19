using MyBankAPI.Data;
using MyBankAPI.Models;
using MyBankAPI.Repository;

namespace MyBankAPI.Services;

public class UserService:IUsers
{
    private readonly ApplicationDbContext _dbcontext;

    public UserService(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public IEnumerable<Users> GetAll()
    {
        return _dbcontext.Users;
    }
}