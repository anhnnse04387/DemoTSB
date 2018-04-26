using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class Customer_transactionDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public Customer_transactionDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<ChiTietNoKhachHang> getDetailTransaction(int customerId)
        {
            //var sumQuery = from oi in db.Order_items
            //               group oi by oi.Order_ID into g
            //               select new
            //               {
            //                   quantity = g.Sum(x => x.Quantity),
            //                   orderId = g.Key
            //               };
            var query = from ct in db.Customer_transaction
                            //join cus in db.Customers on ct.Customer_ID equals cus.Customer_ID
                            //join od in db.Order_total on ct.Order_ID equals od.Order_ID
                            //join sum in sumQuery on ct.Order_ID equals sum.orderId
                        where ct.Customer_ID == customerId
                        select ct;
            //select new
            //{
            //    ct.Date_Created,
            //    ngay = ct.Date_Created,
            //    //soHopDong = od.
            //    loSo = ct.Order_ID,
            //    soLuong = sum.quantity,
            //    dienGiai = ct.Description,
            //    tienHang = ct.Sub_total,
            //    vat = ct.VAT,
            //    tongCong = od.Total_price,
            //    thanhToan = ct.Pay,
            //    duNo = ct.Debt,
            //    ghiChu = ct.Note
            //};            
            List<ChiTietNoKhachHang> lstDetail = new List<ChiTietNoKhachHang>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    ChiTietNoKhachHang ctnkh = new ChiTietNoKhachHang();

                    ctnkh.ngay = item.Date_Created?.ToString("dd/MM/yyyy");
                    ctnkh.loSo = item.Order_ID;
                    // ctnkh.soLuong = Convert.ToInt32(item.soLuong);
                    ctnkh.dienGiai = item.Description;
                    ctnkh.tienHang = item.Sub_total;
                    ctnkh.VAT = item.VAT;
                    ctnkh.tongCong = item.Total;
                    ctnkh.thanhToan = item.Pay;
                    ctnkh.duNo = item.Debt;
                    ctnkh.ghiChu = item.Note;


                    lstDetail.Add(ctnkh);
                }
            }

            return lstDetail;
        }

        public List<ChiTietNoKhachHang> getSearchData(int customerId, string fromDate, string toDate)
        {
            //var sumQuery = from oi in db.Order_items
            //               group oi by oi.Order_ID into g
            //               select new
            //               {
            //                   quantity = g.Sum(x => x.Quantity),
            //                   orderId = g.Key
            //               };
            var query = from ct in db.Customer_transaction
                            //join cus in db.Customers on ct.Customer_ID equals cus.Customer_ID
                            //join od in db.Order_total on ct.Order_ID equals od.Order_ID
                            //join sum in sumQuery on ct.Order_ID equals sum.orderId
                        where ct.Customer_ID == customerId
                        select ct;
            if (!String.IsNullOrEmpty(fromDate) && String.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.Parse(fromDate);
                query = query.Where(x => x.Date_Created >= fromDateValue);
            }
            if (String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
            {
                DateTime toDateValue = DateTime.Parse(toDate);
                query = query.Where(x => x.Date_Created <= toDateValue);
            }
            if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate))
            {
                DateTime fromDateValue = DateTime.Parse(fromDate);
                DateTime toDateValue = DateTime.Parse(toDate);
                query = query.Where(x => x.Date_Created >= fromDateValue && x.Date_Created <= toDateValue);
            }

            List<ChiTietNoKhachHang> lstDetail = new List<ChiTietNoKhachHang>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    ChiTietNoKhachHang ctnkh = new ChiTietNoKhachHang();


                    ctnkh.dateString = item.Date_Created?.ToString("dd/MM/yyyy");
                    ctnkh.loSo = item.Order_ID;
                    // ctnkh.soLuong = Convert.ToInt32(item.soLuong);
                    ctnkh.dienGiai = item.Description;
                    ctnkh.tienHang = item.Sub_total;
                    ctnkh.VAT = item.VAT;
                    ctnkh.tongCong = item.Total;
                    ctnkh.thanhToan = item.Pay;
                    ctnkh.duNo = item.Debt;
                    ctnkh.ghiChu = item.Note;

                    lstDetail.Add(ctnkh);
                }
            }

            return lstDetail;
        }

        public decimal? getLastestDebt(int customerId)
        {
            decimal? debt = null;
            var queryLastestDate = from ct in db.Customer_transaction
                                   group ct by ct.Customer_ID into date
                                   select new
                                   {
                                       customerId = date.Key,
                                       lastDate = date.Max(x => x.Date_Created)
                                   };
            var query = from ct in db.Customer_transaction
                        join lastestData in queryLastestDate on ct.Customer_ID equals lastestData.customerId
                        where ct.Date_Created == lastestData.lastDate && ct.Customer_ID == customerId
                        select ct;
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    debt = item.Debt;
                }
            }
            return debt;
        }
        public int insertNewCustomerDebt(int customerId, string tienHang, string vat, string thanhToan, string duNo, string dienGiai, int userId, string tongCong, string noCu)
        {
            Customer_transaction ct = new Customer_transaction();
            ct.Customer_ID = customerId;
            ct.Date_Created = DateTime.Now;
            ct.Description = dienGiai;
            ct.Sub_total = Convert.ToDecimal(tienHang);
            ct.Pay = Convert.ToDecimal(thanhToan);
            ct.VAT = string.IsNullOrEmpty(vat) ? 0 : Convert.ToDecimal(vat);
            ct.Debt = Convert.ToDecimal(duNo);
            ct.User_ID = userId;
            ct.Old_debt = Convert.ToDecimal(noCu);
            ct.Total = Convert.ToDecimal(tongCong);
            db.Customer_transaction.Add(ct);
            int rowInserted = db.SaveChanges();
            return rowInserted;
        }
        public List<DanhSachNoKhachHang> danhSachKhachHang()
        {
            List<DanhSachNoKhachHang> lst = new List<DanhSachNoKhachHang>();

            var query1 = from transaction in db.Customer_transaction
                         group transaction by transaction.Customer_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from tst in db.Customer_transaction
                         join maxTst in query1 on tst.Transaction_ID equals maxTst.transactionId
                         join cus in db.Customers on tst.Customer_ID equals cus.Customer_ID
                         select new
                         {
                             tst,
                             tenKhachHang = cus.Customer_name
                         };
            if (query2 != null)
            {
                foreach (var item in query2)
                {
                    DanhSachNoKhachHang ds = new DanhSachNoKhachHang();

                    ds.tenKhachHang = item.tenKhachHang;
                    ds.noCu = item.tst.Old_debt == null ? 0 : item.tst.Old_debt;
                    ds.tienHang = item.tst.Sub_total;
                    ds.vat = item.tst.VAT;
                    ds.thanhToan = item.tst.Pay;
                    ds.tongCong = item.tst.Total;
                    ds.conNo = item.tst.Debt;
                    ds.dienGiai = item.tst.Description;
                    ds.customerId = item.tst.Customer_ID;

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<Autocomplete> getListAuto(string valueSearch)
        {
            var query1 = from transaction in db.Customer_transaction
                         group transaction by transaction.Customer_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from tst in db.Customer_transaction
                         join maxTst in query1 on tst.Transaction_ID equals maxTst.transactionId
                         join cus in db.Customers on tst.Customer_ID equals cus.Customer_ID

                         select new
                         {
                             id = tst.Customer_ID,
                             tenKhachHang = cus.Customer_name
                         };
            List<Autocomplete> lst = new List<Autocomplete>();
            query2 = query2.Where(x => x.tenKhachHang.ToLower().Contains(valueSearch.ToLower()));
            if (query2.Count() != 0)
            {
                foreach (var item in query2)
                {
                    Autocomplete obj = new Autocomplete();

                    obj.key = item.tenKhachHang;
                    obj.value = Convert.ToInt32(item.id);

                    lst.Add(obj);
                }
            }
            return lst;
        }
        public List<DanhSachNoKhachHang> getListSearchCustomer(string customerName, string noTu, string noDen)
        {
            List<DanhSachNoKhachHang> lst = new List<DanhSachNoKhachHang>();


            var query1 = from transaction in db.Customer_transaction
                         group transaction by transaction.Customer_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from tst in db.Customer_transaction
                         join maxTst in query1 on tst.Transaction_ID equals maxTst.transactionId
                         join cus in db.Customers on tst.Customer_ID equals cus.Customer_ID
                         select new
                         {
                             tst,
                             tenKhachHang = cus.Customer_name
                         };

            if (!string.IsNullOrEmpty(customerName))
            {
                query2 = query2.Where(x => x.tenKhachHang.Equals(customerName));
            }
            if (!string.IsNullOrEmpty(noTu) && string.IsNullOrEmpty(noDen))
            {
                Decimal value = Convert.ToDecimal(noTu);
                query2 = query2.Where(x => x.tst.Debt >= value);
            }
            if (!string.IsNullOrEmpty(noDen) && string.IsNullOrEmpty(noTu))
            {
                Decimal value = Convert.ToDecimal(noDen);
                query2 = query2.Where(x => x.tst.Debt <= value);
            }
            if (!string.IsNullOrEmpty(noTu) && !string.IsNullOrEmpty(noDen))
            {
                Decimal fromValue = Convert.ToDecimal(noTu);
                Decimal toValue = Convert.ToDecimal(noDen);
                query2 = query2.Where(x => x.tst.Debt >= fromValue && x.tst.Debt <= toValue);
            }
            if (query2.Count() != 0)
            {
                foreach (var item in query2)
                {
                    DanhSachNoKhachHang ds = new DanhSachNoKhachHang();

                    ds.customerId = item.tst.Customer_ID;
                    ds.tenKhachHang = item.tenKhachHang;
                    ds.noCu = item.tst.Old_debt;
                    //ds.soLuong = item.quantity;
                    ds.tienHang = item.tst.Sub_total;
                    ds.vat = item.tst.VAT;
                    ds.tongCong = item.tst.Total;
                    ds.thanhToan = item.tst.Pay;
                    ds.conNo = item.tst.Debt;
                    ds.customerId = item.tst.Customer_ID;
                    ds.dienGiai = item.tst.Description;

                    lst.Add(ds);
                }
            }
            return lst;
        }

        public int insertNewDetailCutomerDebt(string noCu, string nhapTrongKy, string thanhToan, string vat, string conNo, string dienGiai, string customerId, int userId)
        {
            Customer_transaction cus = new Customer_transaction();

            cus.Date_Created = DateTime.Now;
            cus.Customer_ID = Convert.ToInt32(customerId);
            cus.VAT = Convert.ToByte(vat);
            cus.Sub_total = Convert.ToDecimal(nhapTrongKy);
            cus.Description = dienGiai;
            cus.Pay = Convert.ToDecimal(thanhToan);
            cus.Old_debt = Convert.ToDecimal(noCu);
            cus.Debt = Convert.ToDecimal(conNo);
            cus.User_ID = userId;

            db.Customer_transaction.Add(cus);
            return db.SaveChanges();
        }
    }
}
