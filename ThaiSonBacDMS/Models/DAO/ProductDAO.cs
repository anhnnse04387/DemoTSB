using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDAO
    {
        private ThaiSonBacDMSDbContext db = null;

        public ProductDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public List<Product> getProduct(String code)
        {
            db.Configuration.AutoDetectChangesEnabled = false; //added line
            db.Configuration.LazyLoadingEnabled = false; //added line
            db.Configuration.ProxyCreationEnabled = false; //added line
            return db.Products.Where(x => x.Product_code.Trim().ToLower().Contains(code.Trim().ToLower()) && x.Status == 1).ToList();
        }

        public Product getProductById(int? id)
        {
            return db.Products.Where(x => x.Product_ID == id && x.Status == 1).SingleOrDefault();
        }
    }
}
