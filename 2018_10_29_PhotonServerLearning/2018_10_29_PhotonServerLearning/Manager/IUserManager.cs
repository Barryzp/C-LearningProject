using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;

namespace MyGameServer.Manager
{
    interface IUserManager
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        bool VerifyUserAccount(string username, string password);
        User GetUserByID(int id);
        User GetUserByName(string name);
        ICollection<User> GetAllUsers();
    }
}
