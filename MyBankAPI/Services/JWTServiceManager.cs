using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyBankAPI.Data;
using MyBankAPI.Models;
using MyBankAPI.Repository;


namespace MyBankAPI.Services;

public class JWTServiceManager : IJWTTTokenService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbcontext;

    public JWTServiceManager(IConfiguration configuration, ApplicationDbContext dbcontext)
    {
        _configuration = configuration;
        _dbcontext = dbcontext;
    }
    public JWTTokens Authenticate(Users users)
    {
             
        if (!_dbcontext.Users.Any(e => e.UserName == users.UserName && e.Password == users.Password))
        {
            return null;            
        }
 
        var tokenhandler = new JwtSecurityTokenHandler();
        var tkey = Encoding.UTF8.GetBytes(_configuration["JWTToken:key"]);
        var ToeknDescp = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, users.UserName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
        };
        var toekn = tokenhandler.CreateToken(ToeknDescp);
 
        return new JWTTokens { Token = tokenhandler.WriteToken(toekn) };
 
    }
}
