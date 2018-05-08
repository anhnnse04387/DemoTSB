using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class DetailStockInDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public DetailStockInDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public int getStockInQuantities(int productId)
        {
            return Convert.ToInt32(db.Detail_stock_in.SingleOrDefault(x => x.Product_ID == productId).Quantities);
        }
        //update quantities
        public int updateQuantities(int productId, string nhap)
        {
            var query = (from dsi in db.Detail_stock_in
                         where dsi.Product_ID == productId
                         select dsi).Single();
            query.Quantities = Convert.ToInt32(nhap);
            return db.SaveChanges();
        }
    }

}
