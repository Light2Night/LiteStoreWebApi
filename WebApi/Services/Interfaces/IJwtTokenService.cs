using Data.Entities.Identity;

namespace WebApi.Services.Interfaces;

public interface IJwtTokenService {
	Task<string> CreateTokenAsync(User user);
}