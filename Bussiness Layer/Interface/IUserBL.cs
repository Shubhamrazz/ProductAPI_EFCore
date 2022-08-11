using Database_Layer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Interface
{
    public interface IUserBL
    {
        public void Register(UserPostModel userPostModel);
        public string Login(UserLoginModel userLoginModel);
    }
}
