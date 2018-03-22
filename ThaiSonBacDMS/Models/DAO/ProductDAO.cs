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
            return db.Products.Where(x => x.Product_code.Trim().ToLower().Contains(code.Trim().ToLower())).ToList();
        }

        public Product getProductById(int? id)
        {
            return db.Products.Where(x => x.Product_ID.Equals(id)).SingleOrDefault();
        }

        public List<Product> getProductByDateSold(DateTime date)
        {
            var query = from p in db.Products
                        join oi in db.Order_items on p.Product_ID equals oi.Product_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= date
                        select new
                        {
                            productID = p.Product_ID,
                            categoryID = p.Category_ID
                        };
            List<Product> listProduct = new List<Product>();
            if(query == null)
            {
                return new List<Product>();
            }else
            {
                foreach(var item in query)
                {
                    Product prod = new Product();
                    prod = new ProductDAO().getProductById(item.productID);
                    listProduct.Add(prod);
                }
                return listProduct;
            }
        }
    }
}
