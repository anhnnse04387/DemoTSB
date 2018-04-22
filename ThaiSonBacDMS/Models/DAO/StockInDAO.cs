using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class StockInDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public StockInDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public void createStockIn(Stock_in model)
        {
            db.Stock_in.Add(model);
            db.SaveChanges();
        }

        public Stock_in getStockInById(int id)
        {
            return db.Stock_in.Where(x => x.Stock_in_ID == id).SingleOrDefault();
        }

    }
}
