using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class Account_roleDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public Account_roleDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public int insertNewRecord(int accId, string roleId)
        {
            Account_role item = new Account_role();

            item.Account_ID = accId;
            item.Role_ID = Convert.ToByte(roleId);

            db.Account_role.Add(item);
            int result = db.SaveChanges();
            return result;
        }
        public int updateAccRole(int accId, int roleId)
        {
            Account_role item = new Account_role();
            item = db.Account_role.Single(x => x.Account_ID == accId);
            item.Role_ID = Convert.ToByte(roleId);
            int result = db.SaveChanges();
            return result;
        }
    }
}
