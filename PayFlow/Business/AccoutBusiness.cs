using Domain.Auth;
using Infrastructure.Repository.Interfaces;
using PayFlow.Services;
using Microsoft.AspNetCore.Mvc;

namespace PayFlow.Business
{
    public class AccoutBusiness : Controller
    {
        private readonly IAccoutRepository _accoutRepository;

        public AccoutBusiness(IAccoutRepository accoutRepository)
        {
            _accoutRepository = accoutRepository;
        }

        public async Task<UserAuthenticate?> Authentication(string user, string pass)
        {
            try
            {
                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                {
                    return await _accoutRepository.ViewAuthenticate(user, pass);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while trying to authenticate.");
            }

        }

        public async Task<TokenBody> GenerateToken(TokenService tokenService, UserAuthenticate user)
        {
            return await tokenService.GenerateToken(user);
        }
    }
}

