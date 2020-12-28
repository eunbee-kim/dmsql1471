using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;

namespace Test1.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(string id);
        User AddUser(User user);
        User UpdateUser(User user);
        void RemoveUser(string id);
    }
}
