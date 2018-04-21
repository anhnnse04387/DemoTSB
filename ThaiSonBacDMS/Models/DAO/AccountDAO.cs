using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.DAO
{
    public class AccountDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public AccountDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public Account GetByName(string account_name)
        {
            return context.Accounts.SingleOrDefault(s => s.Account_name == account_name);
        }
        
        public int Login(string accountName, string password)
        {
            var data = context.Accounts.SingleOrDefault(s => s.Account_name == accountName);
            if (data == null)
            {
                return 0;
            }
            else
            {
                if (data.Account_Status == "Deactive")
                {
                    return -1;
                }
                else
                {
                    if (data.Password.Trim().ToLower() == password.Trim().ToLower())
                        return 1;
                    else
                        return -2;
                }

            }
        }

        public int accountCount()
        {
            return context.Accounts.Count();
        }
        //thuongtx
        public int getRoleIdByCurrentAcc(string accountId)
        {
            return Convert.ToInt32(context.Accounts.SingleOrDefault(x => x.Account_ID.ToString().Equals(accountId)).Role_ID);
        }
    }
}
