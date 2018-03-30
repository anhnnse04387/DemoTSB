using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class RoleDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public RoleDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public Role_detail getByID(byte id)
        {
            return context.Role_detail.SingleOrDefault(s => s.Role_ID == id);
        }
        public List<Role_detail> getRoleByAccount(int account_id)
        {
            var x = from a in context.Account_role
                    join r in context.Role_detail on a.Role_ID equals (r.Role_ID)
                    where a.Account_ID == account_id
                    select new
                    {
                        accID = a.Account_ID,
                        roleName = r.Role_name,
                        roleID = r.Role_ID
                    };
            List<Role_detail> result = new List<Role_detail>();
            foreach(var i in x)
            {
                result.Add(new RoleDAO().getByID(i.roleID));
            }
            return result;
        }
        public int roleCount()
        {
            return context.Role_detail.Count();
        }
    }
}
