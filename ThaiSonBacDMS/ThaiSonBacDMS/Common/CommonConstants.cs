using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Common
{
    public static class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string PHONE_REGEX = @"^(01[2689]|09)[0-9]{8}$";
        public static string MST_REGEX = @"^([0-9]{2})[0-9]{8}$";
    }
}