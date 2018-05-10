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


            if (!roleId.Contains(','))
            {
                Account_role item = new Account_role();
                item.Account_ID = accId;
                item.Role_ID = Convert.ToByte(roleId);
                db.Account_role.Add(item);
            }
            else
            {

                string[] roles = roleId.Split(',');
                foreach (var roleIds in roles)
                {
                    Account_role item = new Account_role();
                    item.Account_ID = accId;
                    item.Role_ID = Convert.ToByte(roleIds);
                    db.Account_role.Add(item);
                }
            }


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
        public List<string> getRoleId(int accId)
        {
            List<string> lst = new List<string>();
            var query = from role in db.Account_role
                        where role.Account_ID == accId
                        select role;
            if(query.Count() > 0)
            {
                foreach(var item in query)
                {
                    lst.Add(item.Role_ID.ToString());
                }
            }
            return lst;
        }
    }
}
