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
        public int insertData(int customerId, string tienHang, string vat, string thanhToan, string duNo,string dienGiai,int userId,string tongCong)
        {
            Customer_transaction ct = new Customer_transaction();
            ct.Customer_ID = customerId;
            ct.Date_Created = DateTime.Now;           
            ct.Description = dienGiai;
            ct.Sub_total = Convert.ToDecimal(tienHang);
            ct.Pay = Convert.ToDecimal(thanhToan);
            ct.VAT = Convert.ToByte(vat);
            ct.Debt = Convert.ToDecimal(duNo);
            ct.User_ID = userId.ToString();
            ct.Total = Convert.ToDecimal(tongCong);
            db.Customer_transaction.Add(ct);
            int rowInserted = db.SaveChanges();
            return rowInserted;
        }
    }
}
