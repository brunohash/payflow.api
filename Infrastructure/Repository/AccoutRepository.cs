using Domain.Auth;
using Dapper;
using MySqlConnector;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace Infrastructure.Repository
{
    public class AccoutRepository : IAccoutRepository
    {
        private readonly IDbConnection _mySql;

        public AccoutRepository(IConfiguration config)
        {
            _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
        }

        public async Task<UserAuthenticate> ViewAuthenticate(string user, string pass)
        {
            string passOn = generateMD5(pass);

            return await _mySql.QueryFirstOrDefaultAsync<UserAuthenticate>(@"
                        SELECT Id, User, Role FROM `PayFlow`.`User.Api`
                        WHERE Status = 1 AND `User` = @User AND `Password` = @Pass
                      ",
                new
                {
                    User = user,
                    Pass = passOn
                });
        }

        public Task<int> GetUser(string? email)
        {
            return _mySql.ExecuteScalarAsync<int>(@"
                        SELECT * FROM `PayFlow`.`User.Account`
                        WHERE `email` = @Email
                      ",
                    new
                    {
                        Email = email
                    });
        }

        public string generateMD5(string password)
        {
            var md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}

