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
        public string user_id;
=======
        public string account_name;
        public int user_id;
>>>>>>> 980fba25e97eb4c727e65e174c82033fdded907e
        public string user_name;
        public string role_name;
        public int roleSelectedID;
        public string avatar_str;
    }
}