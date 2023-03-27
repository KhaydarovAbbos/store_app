using StoreApp.Domain.Entities.Users;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> UserLogin(UserLoginViewModel userLoginViewModel);

        Task<User> UserSignUp(UserSignUpViewModel userSignUpViewModel);

        Task<User> CheckUser(string login);

    }
}
