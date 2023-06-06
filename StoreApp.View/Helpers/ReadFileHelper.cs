using StoreApp.Domain.Entities.Users;
using System.Collections.Generic;
using System.IO;

namespace StoreApp.View.Helpers
{
    public static class ReadFileHelper
    {
        public static IList<UserSignIn> GetUsers()
        {

            IList<UserSignIn> users = new List<UserSignIn>();

            //var logFile = File.ReadAllLines(App.FilePath);
            //var logList = new List<string>(logFile);

            //foreach (var item in logList)
            //{
            //    if (item != "")
            //    {
            //        string[] data = item.Split();

            //        UserSignIn user = new UserSignIn()
            //        {
            //            Login = data[0],
            //            Password = data[1]
            //        };

            //        users.Add(user);
            //    }
            //}

            return users;
        }
    }
}
