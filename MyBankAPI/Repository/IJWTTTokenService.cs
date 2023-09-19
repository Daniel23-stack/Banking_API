using MyBankAPI.Models;

namespace MyBankAPI.Repository;

public interface IJWTTTokenService
{
    
    JWTTokens Authenticate(Users users);
}