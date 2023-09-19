using MyBankAPI.Models;

namespace MyBankAPI.Repository;

public interface IUsers
{
    IEnumerable<Users> GetAll();
}