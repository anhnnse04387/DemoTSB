using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Common
{
    [Serializable]
    public class UserSession
    {
        public int accountID;
        public User user_info;
        public bool roleSelectFlag;
<<<<<<< HEAD
        public int user_id;
        public string account_name;
=======
        public string account_name;
        public int user_id;
>>>>>>> 846a234cd9a4953a42cfd468ee61ea90f71d9a0f
        public string user_name;
        public string role_name;
        public int roleSelectedID;
        public string avatar_str;
    }
}