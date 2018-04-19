using Models.DAO_Model;
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
        //thuongtx
        public byte getRoleId()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("quản trị")).Role_ID;
        }

        public byte? getRoleIdByCurrentAcc(string userId)
        {
            return context.Accounts.SingleOrDefault(x => x.User_ID.Equals(userId)).Role_ID;
        }
        public List<DanhSachNguoiDung> getAllUsersActive()
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        //where user
                        select new
                        {
                            user,
                            media.Location
                        };
            if (query != null)
            {
                foreach(var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.
                }
            }
            return lst;
        }
    }
}
