using Database_Layer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRL
    {
        public void Register(UserPostModel userPostModel);
        public string Login(UserLoginModel userLoginModel);
    }
}
