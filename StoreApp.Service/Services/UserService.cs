using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Users;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Services
{
    public class UserService : IUserService
    {
        internal IUserRepository UserRepository;

        public UserService()
        {
            UserRepository = new UserRepository();
        }

        public Task<User> CheckUser(string login)
        {
            return UserRepository.GetAsync(x => x.Login == login);
        }

        public Task<User> UserLogin(UserLoginViewModel  userLoginViewModel)
        {
            return UserRepository.GetAsync(x => x.Login == userLoginViewModel.Login && x.Password == userLoginViewModel.Password);
        }

        public Task<User> UserSignUp(UserSignUpViewModel userSignUpViewModel)
        {
            User user = new User
            {
                Login = userSignUpViewModel.Login,
                Password = userSignUpViewModel.Password,
            };

            return UserRepository.CreatAsync(user);
        }

        public Task<IEnumerable<User>> GetAllusers()
        {
            return UserRepository.GetAllAsync();
        }
    }
}
