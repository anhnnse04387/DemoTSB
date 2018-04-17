using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class Supplier_transactionDAO
    {
        private ThaiSonBacDMSDbContext db;
        public Supplier_transactionDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<ChiTietNoCungCap> getLst(int supplierId)
        {
            var query = from st in db.Supplier_transaction
                        join po in db.POes on st.Supplier_ID equals po.Supplier_ID
                        where st.Supplier_ID == supplierId
                        select new { st, po.PO_no };
            List<ChiTietNoCungCap> lst = new List<ChiTietNoCungCap>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    ChiTietNoCungCap obj = new ChiTietNoCungCap();
                    obj.ngay = item.st.Date_Created?.ToString("dd/MM/yyyy");
                    obj.soPo = item.PO_no;
                    obj.dienGiai = item.st.Description;
                    obj.tienHang = item.st.Sub_total;
                    obj.thanhToan = item.st.Pay;
                    obj.duNo = item.st.Debt;
                    obj.ghiChu = item.st.Note;

                    lst.Add(obj);
                    //obj.tienHang = item.st.
                }
            }

            return lst;
        }
        public List<ChiTietNoCungCap> getLstSearch(int supplierId, string fromDate, string toDate)
        {
            var query = from supplier in db.Supplier_transaction
                        join po in db.POes on supplier.Supplier_ID equals po.Supplier_ID
                        where supplier.Supplier_ID == supplierId
                        select new { supplier, po.PO_no };
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = Convert.ToDateTime(fromDate);
                query = query.Where(x => x.supplier.Date_Created >= fromValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime toValue = Convert.ToDateTime(toDate);
                query = query.Where(x => x.supplier.Date_Created <= toValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = Convert.ToDateTime(fromDate);
                DateTime toValue = Convert.ToDateTime(toDate);
                query = query.Where(x => x.supplier.Date_Created >= fromValue && x.supplier.Date_Created <= toValue);
            }
            List<ChiTietNoCungCap> lst = new List<ChiTietNoCungCap>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    ChiTietNoCungCap obj = new ChiTietNoCungCap();
                    obj.ngay = item.supplier.Date_Created?.ToString("dd/MM/yyyy");
                    obj.soPo = item.PO_no;
                    obj.dienGiai = item.supplier.Description;
                    obj.tienHang = item.supplier.Sub_total;
                    obj.thanhToan = item.supplier.Pay;
                    obj.duNo = item.supplier.Debt;
                    obj.ghiChu = item.supplier.Note;

                    lst.Add(obj);
                }
            }

            return lst;
        }
        public decimal? getLastestDebt(int supplierId)
        {
            decimal? debt = null;
            var queryLastestDate = from ct in db.Supplier_transaction
                                   group ct by ct.Supplier_ID into date
                                   select new
                                   {
                                       supplierId = date.Key,
                                       lastDate = date.Max(x => x.Date_Created)
                                   };
            var query = from ct in db.Supplier_transaction
                        join lastestData in queryLastestDate on ct.Supplier_ID equals lastestData.supplierId
                        where ct.Date_Created == lastestData.lastDate && ct.Supplier_ID == supplierId
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
        public int insertData(string supplierId,string ngay, string dienGiai, string noCu, string tienHang, string thanhToan, string duNo, string ghiChu,int userId)
        {
            Supplier_transaction item = new Supplier_transaction();

            item.Supplier_ID = Convert.ToInt32(supplierId);
            item.Date_Created = Convert.ToDateTime(ngay);
            item.Description = dienGiai;
            item.Sub_total = Convert.ToDecimal(tienHang);
            item.Pay = Convert.ToDecimal(thanhToan);
            item.Debt = Convert.ToDecimal(duNo);
            item.Note = ghiChu;
            item.User_ID = userId.ToString();

            db.Supplier_transaction.Add(item);
            return db.SaveChanges();
        }

    }
}
