using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue_JS_Template_AspNet_Core_3._1_Web_API111.Models;

namespace Vue_JS_Template_AspNet_Core_3._1_Web_API111.Interface
{
    public interface ILoginUserService
    {
        IEnumerable<LoginUser> GetAllLoginUser();

        LoginUser GetLoginUserById(string loginid);

        LoginUser AddLoginUser(LoginUser loginUser);

        LoginUser UpdateLoginUser(LoginUser loginUser);

        void Remove(string id);

    }
}
