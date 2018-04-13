using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        private ThaiSonBacDMSDbContext context = null;
        public UserDAO()
        {
            context = new ThaiSonBacDMSDbContext();
        }
        public User getByID(string user_id)
        {
            return context.Users.SingleOrDefault(s => s.User_ID == user_id);
        }
        public List<User> getLstShipper()
        {
            return context.Users.Where(s => s.Role_ID == 4 && s.Office_ID == 4).ToList();
        }
        public List<User> getLstDriver()
        {
            return context.Users.Where(s => s.Role_ID == 4 && s.Office_ID == 12).ToList();
        }
        public User getByAccountID(int account_id)
        {
            var x = from a in context.Accounts
                    join u in context.Users on a.User_ID equals (u.User_ID)
                    where a.Account_ID == account_id
                    select new
                    {
                        accID = a.Account_ID,
                        userID = u.User_ID
                    };
            User user = new User();
            if (x != null)
            {
                user = new UserDAO().getByID(x.ToList()[0].userID);
            }
            return user;
        }
        public string getOffice(int officeID)
        {
            return context.Offices.SingleOrDefault(x => x.Office_ID == officeID).Office_name;
        }
    }
}
