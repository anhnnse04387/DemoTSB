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
                    obj.ngay = item.st.Date_Created?.ToString("dd-MM-yyyy");
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
            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.supplier.Date_Created >= fromValue);
            }
            if (string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.supplier.Date_Created <= toValue);
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = DateTime.ParseExact(fromDate, "d-M-yyyy", CultureInfo.InvariantCulture);
                DateTime toValue = DateTime.ParseExact(toDate, "d-M-yyyy", CultureInfo.InvariantCulture); ;
                query = query.Where(x => x.supplier.Date_Created >= fromValue && x.supplier.Date_Created <= toValue);
            }
            List<ChiTietNoCungCap> lst = new List<ChiTietNoCungCap>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    ChiTietNoCungCap obj = new ChiTietNoCungCap();
                    obj.ngay = item.supplier.Date_Created?.ToString("dd-MM-yyyy");
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
        public int insertData(string supplierId, string ngay, string dienGiai, string noCu, string tienHang, string thanhToan, string duNo, string ghiChu, int userId)
        {
            Supplier_transaction item = new Supplier_transaction();

            item.Supplier_ID = Convert.ToInt32(supplierId);
            item.Date_Created = Convert.ToDateTime(ngay);
            item.Description = dienGiai;
            item.Sub_total = Convert.ToDecimal(tienHang);
            item.Pay = Convert.ToDecimal(thanhToan);
            item.Debt = Convert.ToDecimal(duNo);
            item.Note = ghiChu;
            item.User_ID = userId;

            db.Supplier_transaction.Add(item);
            return db.SaveChanges();
        }
        //danh sach cong no cung cap
        public List<DanhSachNoCungCap> listDebtSupplier()
        {
            List<DanhSachNoCungCap> lst = new List<DanhSachNoCungCap>();
            var query1 = from supplier in db.Supplier_transaction
                         group supplier by supplier.Supplier_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from supplier in db.Supplier_transaction
                         join maxId in query1 on supplier.Transaction_ID equals maxId.transactionId
                         join sup in db.Suppliers on supplier.Supplier_ID equals sup.Supplier_ID
                         select new
                         {
                             supplier,
                             tenNhaCungCap = sup.Supplier_name
                         };

            if (query2 != null)
            {
                foreach (var item in query2)
                {
                    DanhSachNoCungCap ds = new DanhSachNoCungCap();

                    ds.tenNhaCungCap = item.tenNhaCungCap;
                    ds.noDauKy = Convert.ToDecimal(item.supplier.Old_debt);
                    ds.nhapTrongKy = Convert.ToDecimal(item.supplier.Sub_total);
                    ds.thanhToan = Convert.ToDecimal(item.supplier.Pay);
                    ds.conNo = Convert.ToDecimal(item.supplier.Debt);
                    ds.supplierId = Convert.ToInt32(item.supplier.Supplier_ID);
                    ds.dienGiai = item.supplier.Description;

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<DanhSachNoCungCap> lstSearchDebtSupplier(string supplierId, string fromValue, string toValue)
        {
            List<DanhSachNoCungCap> lst = new List<DanhSachNoCungCap>();
            var query1 = from supplier in db.Supplier_transaction
                         group supplier by supplier.Supplier_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from supplier in db.Supplier_transaction
                         join maxId in query1 on supplier.Transaction_ID equals maxId.transactionId
                         join sup in db.Suppliers on supplier.Supplier_ID equals sup.Supplier_ID
                         select new
                         {
                             supplier,
                             tenNhaCungCap = sup.Supplier_name
                         };
            if (!string.IsNullOrEmpty(supplierId))
            {
                query2 = query2.Where(x => x.tenNhaCungCap.ToString().Equals(supplierId));
            }
            if (!string.IsNullOrEmpty(fromValue) && string.IsNullOrEmpty(toValue))
            {
                Decimal value = Convert.ToDecimal(fromValue);
                query2 = query2.Where(x => x.supplier.Debt >= value);
            }
            if (string.IsNullOrEmpty(fromValue) && !string.IsNullOrEmpty(toValue))
            {
                Decimal value = Convert.ToDecimal(toValue);
                query2 = query2.Where(x => x.supplier.Debt <= value);
            }
            if (!string.IsNullOrEmpty(fromValue) && !string.IsNullOrEmpty(toValue))
            {
                Decimal value1 = Convert.ToDecimal(fromValue);
                Decimal value2 = Convert.ToDecimal(toValue);
                query2 = query2.Where(x => x.supplier.Debt >= value1 && x.supplier.Debt <= value2);
            }
            if (query2 != null)
            {
                foreach (var item in query2)
                {
                    DanhSachNoCungCap ds = new DanhSachNoCungCap();

                    ds.tenNhaCungCap = item.tenNhaCungCap;
                    ds.noDauKy = Convert.ToDecimal(item.supplier.Old_debt);
                    ds.nhapTrongKy = Convert.ToDecimal(item.supplier.Sub_total);
                    ds.thanhToan = Convert.ToDecimal(item.supplier.Pay);
                    ds.conNo = Convert.ToDecimal(item.supplier.Debt);
                    ds.supplierId = Convert.ToInt32(item.supplier.Supplier_ID);
                    ds.dienGiai = item.supplier.Description;

                    lst.Add(ds);
                }
            }
            return lst;
        }
        public List<Autocomplete> autoCompleteNameSearch(string searchValue)
        {
            List<Autocomplete> lst = new List<Autocomplete>();

            var query1 = from supplier in db.Supplier_transaction
                         group supplier by supplier.Supplier_ID into g
                         select new
                         {
                             transactionId = g.Max(x => x.Transaction_ID)
                         };
            var query2 = from supplier in db.Supplier_transaction
                         join maxId in query1 on supplier.Transaction_ID equals maxId.transactionId
                         join sup in db.Suppliers on supplier.Supplier_ID equals sup.Supplier_ID
                         select new
                         {
                             supplier,
                             tenNhaCungCap = sup.Supplier_name
                         };

            query2 = query2.Where(x => x.tenNhaCungCap.ToLower().Contains(searchValue.ToLower()));
            if (query2 != null)
            {
                foreach (var item in query2)
                {
                    Autocomplete obj = new Autocomplete();
                    obj.key = item.tenNhaCungCap;
                    obj.value = Convert.ToInt32(item.supplier.Supplier_ID);
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public int insertNewSupplierDebt(string supplierId, string nhapTrongKy, string thanhToan, string conNo, string noDauKy, string dienGiai, int userId)
        {
            Supplier_transaction item = new Supplier_transaction();

            item.Date_Created = DateTime.Now;
            item.Supplier_ID = Convert.ToInt32(supplierId);
            item.Old_debt = Convert.ToDecimal(noDauKy);
            item.Description = dienGiai;
            item.Sub_total = nhapTrongKy != "" ? Convert.ToDecimal(nhapTrongKy) : 0;
            item.Pay = thanhToan != "" ? Convert.ToDecimal(thanhToan) : 0;
            item.Debt = Convert.ToDecimal(conNo);
            item.User_ID = Convert.ToInt32(userId);

            db.Supplier_transaction.Add(item);


            return db.SaveChanges();
        }
    }
}
