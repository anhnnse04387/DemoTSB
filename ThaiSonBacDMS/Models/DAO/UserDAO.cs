using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public User getByID(int? user_id)
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


        //public int getRoleIdByCurrentAcc(string userId)
        //{
        //    return Convert.ToInt32(context.Users.SingleOrDefault(x => x.User_ID.Equals(userId)).Role_ID);
        //}
        public int getRoleQuanTri()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("quản trị")).Role_ID;
        }
        public int getRoleQuanLy(string userId)
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("quản lý")).Role_ID;
        }
        public string getRoleQuanLy()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("quản lý")).Role_ID.ToString();
        }
        public string getRolePhanPhoi()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("phân phối")).Role_ID.ToString();
        }
        public string getRoleHangHoa()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("hàng hóa")).Role_ID.ToString();
        }
        public string getRoleKeToan()
        {
            return context.Role_detail.SingleOrDefault(x => x.Role_name.ToLower().Equals("kế toán")).Role_ID.ToString();
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanTri()
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanTri(string userName)
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1 && userName.Equals(userName)
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanTri(string nameSearch, string roleSearch, string fromDate, string toDate)
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(x => x.user.User_name.Equals(nameSearch));
            }
            if (!string.IsNullOrEmpty(roleSearch))
            {
                query = query.Where(x => x.user.Role_ID.ToString().Equals(roleSearch));
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue);
            }
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created <= toDateValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue && x.user.Date_created <= toDateValue);
            }
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanLy()
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi))
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanLy(string valueSearch)
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi)) && user.User_name.Contains(valueSearch)
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersActiveByQuanLy(string nameSearch, string roleSearch, string fromDate, string toDate)
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 1 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi))
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(x => x.user.User_name.Equals(nameSearch));
            }
            if (!string.IsNullOrEmpty(roleSearch))
            {
                query = query.Where(x => x.user.Role_ID.ToString().Equals(roleSearch));
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue);
            }
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created <= toDateValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue && x.user.Date_created <= toDateValue);
            }
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanTri()
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanTri(string userName)
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0 && userName.Equals(userName)
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanTri(string nameSearch, string roleSearch, string fromDate, string toDate)
        {
            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(x => x.user.User_name.Equals(nameSearch));
            }
            if (!string.IsNullOrEmpty(roleSearch))
            {
                query = query.Where(x => x.user.Role_ID.ToString().Equals(roleSearch));
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue);
            }
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created <= toDateValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue && x.user.Date_created <= toDateValue);
            }
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanLy()
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi))
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanLy(string valueSearch)
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi)) && user.User_name.Contains(valueSearch)
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public List<DanhSachNguoiDung> getAllUsersDeActiveByQuanLy(string nameSearch, string roleSearch, string fromDate, string toDate)
        {
            string roleQuanly = getRoleQuanLy();
            string roleHangHoa = getRoleHangHoa();
            string roleKeToan = getRoleKeToan();
            string rolePhanPhoi = getRolePhanPhoi();

            List<DanhSachNguoiDung> lst = new List<DanhSachNguoiDung>();

            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID.ToString() equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        where user.Status == 0 && (user.Role_ID.ToString().Equals(roleQuanly)
                        || user.Role_ID.ToString().Equals(roleHangHoa)
                        || user.Role_ID.ToString().Equals(roleKeToan)
                        || user.Role_ID.ToString().Equals(rolePhanPhoi))
                        orderby user.Date_created ascending
                        select new
                        {
                            user,
                            media.Location,
                            role.Role_name

                        };
            if (!string.IsNullOrEmpty(nameSearch))
            {
                query = query.Where(x => x.user.User_name.Equals(nameSearch));
            }
            if (!string.IsNullOrEmpty(roleSearch))
            {
                query = query.Where(x => x.user.Role_ID.ToString().Equals(roleSearch));
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue);
            }
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created <= toDateValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                DateTime toDateValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.user.Date_created >= fromDateValue && x.user.Date_created <= toDateValue);
            }
            if (query != null)
            {
                foreach (var item in query)
                {
                    DanhSachNguoiDung ds = new DanhSachNguoiDung();


                    ds.tenNguoiDung = item.user.User_name;
                    ds.anhDaiDien = item.Location;
                    ds.ngayTao = Convert.ToDateTime(item.user.Date_created);
                    ds.phanHe = item.Role_name;
                    ds.soDienThoai = item.user.Phone;
                    ds.diaChi = item.user.User_Address;
                    //ds.trangThai = item.user.Status == 1 ? "Đang hoạt động" : "";

                    lst.Add(ds);
                }
            }

            return lst;
        }
        public string insertNewUser(User item, string mediaId)
        {
            try
            {
                User user = new User();

                user.User_name = item.User_name;
                user.Office_ID = item.Office_ID;
                user.Date_of_birth = item.Date_of_birth;
                user.User_Address = item.User_Address;
                user.Phone = item.Phone;
                user.Mail = item.Mail;
                user.Role_ID = item.Role_ID;
                user.Date_created = DateTime.Now;
                user.Avatar_ID = mediaId;
                user.Insurance_Code = item.Insurance_Code;
                user.Status = 1;

                context.Users.Add(user);
                context.SaveChanges();
                return user.User_ID;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return ("-1");
            }
        }
        public DanhSachNguoiDung getDetailUser(string userId)
        {
            DanhSachNguoiDung item = new DanhSachNguoiDung();
            var query = from user in context.Users
                        join media in context.Media on user.Avatar_ID equals media.Media_ID.ToString()
                        join role in context.Role_detail on user.Role_ID equals role.Role_ID
                        join office in context.Offices on user.Office_ID equals office.Office_ID
                        where user.User_ID == userId
                        select new
                        {
                            user,
                            image = media.Location,
                            roleName = role.Role_name,
                            office = office.Office_name
                        };
            if (query != null)
            {
              foreach(var user in query)
                {
                    item.tenNguoiDung = user.user.User_name;
                    item.anhDaiDien = user.image;
                    item.ngayTao = Convert.ToDateTime(user.user.Date_created);
                    item.phanHe = user.roleName;
                    item.diaChi = user.user.User_Address;
                    item.soDienThoai = user.user.Phone;
                    item.trangThai = user.user.Status == 1 ? "Active" : "Deactive";
                    item.chucVu = user.office;
                    item.ngaySinh = Convert.ToDateTime(user.user.Date_of_birth).ToString();
                    item.BHYT = user.user.Insurance_Code;
                    item.email = user.user.Mail;

                }

            }
            return item;
        }
    }
}
