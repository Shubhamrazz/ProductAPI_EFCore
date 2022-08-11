using Bussiness_Layer.Interface;
using Database_Layer.UserModel;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void Register(UserPostModel userPostModel)
        {
            try
            {
                this.userRL.Register(userPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return this.userRL.Login(userLoginModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
