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
        public int insertNewAccount(string accName, string password, int userId, string roleId, string isActive)
        {
            Account acc = new Account();

            acc.Account_name = accName;
            acc.Password = password;
            acc.User_ID = userId;
            acc.Role_ID = Convert.ToByte(roleId);
            acc.Account_Status = isActive == "0" ? "Deactive" : "Active";
            acc.Date_Created = DateTime.Now;

            context.Accounts.Add(acc);
            return context.SaveChanges();
        }
        public int checkExistAcc(string accSearch)
        {
            var query = from acc in context.Accounts
                        where acc.Account_name.ToLower().Equals(accSearch.ToLower())
                        select acc;
            if (query != null)
            {
                return 1;
            }
            return 0;
        }
        public int getMaxId(string account)
        {
           
            int result = 0;
            var query = from acc in context.Accounts
                        where acc.Account_name.Contains(account)
                        select acc.Account_ID;
            List<int> lstId = new List<int>();
            if (query != null)
            {
                foreach(int id in query)
                {
                    lstId.Add(id);
                }
            }
            if (lstId.Count() != 0)
            {
                result = lstId.Max();
            }
            return result;
        }
        public string getExistingAcc(int accountId)
        {
            return context.Accounts.SingleOrDefault(x => x.Account_ID == accountId).Account_name;
        }

        public int updateAccount(string userId,string accountName,string roleId,string isActive)
        {
            int result = 0;
            var account = context.Accounts.Single(x => x.User_ID.ToString().Equals(userId));
            if (account != null)
            {
                account.Account_name = accountName;
                account.Role_ID = Convert.ToByte(roleId);
                account.Account_Status = isActive.Equals("true") ? "Active" : "Deactive";

                result = context.SaveChanges();
            }
            return result;
        }

        public int resetPassword(string userId,string newPassword)
        {
            int result = 0;
            var account = context.Accounts.SingleOrDefault(x => x.User_ID.Equals(userId));
            if (account != null)
            {
                account.Password = newPassword;
                result = context.SaveChanges();
            }
            return result;
        }
    }
}
