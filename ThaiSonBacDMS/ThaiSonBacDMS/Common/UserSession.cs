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
        public string account_name;
        public string user_id;
        public string user_name;
        public string role_name;
    }
}