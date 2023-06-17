using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Mediatr;

namespace Application.Users.UserCommands.UserRegistration
{
    public class UserRegistrHandler : IRequestHandler<UserRegistrRequest, Response>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly SignInManager<User> _signInManager;

        public UserRegistrHandler(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<User>)_userStore;
            _signInManager = signInManager;
        }

        public async Task<Response> Handle(UserRegistrRequest request, CancellationToken token)
        {
            var user = new User()
            {
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
            };
             var result = await _userManager.CreateAsync(user, request.Password);
             if (result.Succeeded)
             {
                 await _signInManager.SignInAsync(user, isPersistent: false);
                 return new Response() { StatusCode = 200,
                     Success = true,
                     Message = "User created a new account with password." };
             }
        }
    }
}
