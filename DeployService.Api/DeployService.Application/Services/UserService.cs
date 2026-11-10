using DeployService.Application.Interfaces;

namespace DeployService.Application.Services;

public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    
    
    
    
}