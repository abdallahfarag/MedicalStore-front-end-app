using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalStore.Helpers
{
    public class AuthorizationHelper
    {
        public static UserInfoViewModel GetUserInfo()
        {
            var user = HttpContext.Current.Session["userinfo"];
            if(user is null)
            {
                return null;
            }
            return user as UserInfoViewModel;
        }
    }
}