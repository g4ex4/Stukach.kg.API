using Application.Users.UserCommands.UserAuth;
using Domain.Models;

namespace Application.Managers;

public class AuthManager
{
    private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<AuthResponse> LoginUser(UserLoginRequest request, CancellationToken token)
        {
            var user = await _userManager.Find
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _signInManager.PasswordSignInAsync(request.PhoneNumber,
                request.Password, false, false);
            if (result.Succeeded)
            {
                

                return new Response(200, true, "Операция успешна!");
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("User account locked out");
            }
            else
            {
                throw new Exception("Login Error");
            }
        }
}