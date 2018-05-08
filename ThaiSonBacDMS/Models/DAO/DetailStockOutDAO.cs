using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
   public class DetailStockOutDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public DetailStockOutDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public int getStockOutQuantities(int productId)
        {
            return Convert.ToInt32(db.Detail_stock_out.SingleOrDefault(x => x.Product_ID == productId).Quantities);
        }
        //update quantities
        public int updateQuantites(int productId,string xuat)
        {
            var query = (from dso in db.Detail_stock_out
                         where dso.Product_ID == productId
                         select dso).Single();
            query.Quantities = Convert.ToInt32(xuat);
            return db.SaveChanges();
        }
    }
}
