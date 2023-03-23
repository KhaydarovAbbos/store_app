using StoreApp.Domain.Entities.Users;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> UserLogin(UserLoginViewModel userLoginViewModel);

        Task<User> UserSignUp(UserSignUpViewModel userSignUpViewModel);

        Task<User> CheckUser(string login);

    }
}
