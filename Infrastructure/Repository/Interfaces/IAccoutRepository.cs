using Domain.Auth;

namespace Infrastructure.Repository.Interfaces
{
	public interface IAccoutRepository
    {
        Task<UserAuthenticate> ViewAuthenticate(string user, string pass);
    }
}

