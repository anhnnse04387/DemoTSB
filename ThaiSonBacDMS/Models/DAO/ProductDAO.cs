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
            return db.Products.Where(x => x.Product_ID == id).SingleOrDefault();
        }
        //ProductDAO by CuongNM
        public List<Product> getProductByDateSold(DateTime date)
        {
            var query = from p in db.Products
                        join oi in db.Order_items on p.Product_ID equals oi.Product_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= date && oi.Order_part_ID == null
                        select new
                        {
                            productID = p.Product_ID,
                            categoryID = p.Category_ID
                        };
            List<Product> listProduct = new List<Product>();
            if (query == null)
            {
                return new List<Product>();
            }
            else
            {
                foreach (var item in query)
                {
                    Product prod = new Product();
                    int prod_id = item.productID;
                    prod = new ProductDAO().getProductById(prod_id);
                    listProduct.Add(prod);
                }
                return listProduct;
            }
        }

        
        //ProductDAO by ThuongTX
        public List<Product> getListProduct()
        {
            return db.Products.Where(x => x.Status == 1).ToList();
        }
        public List<Category> getListCate()
        {
            return db.Categories.Where(x => x.Status == 1).ToList();
        }

        public int insertProduct(Product item)
        {
            db.Products.Add(item);
            return db.SaveChanges();
        }
        //autocomplete
        public List<Product> getLstProductSearch(string value)
        {
            return db.Products.Where((x => (x.Product_code.Contains(value) || x.Product_name.Contains(value)) && x.Status == 1)).ToList();
        }

        public List<Product> getLstSearch(Product product)
        {
            var result = db.Products.AsQueryable();
            if (product.Category_ID != null && product.Category_ID != "0")
            {
                result = result.Where(x => x.Category_ID == product.Category_ID && x.Status == 1);
            }
            if (product.Product_code != null)
            {
                result = result.Where(x => x.Product_code.Equals(product.Product_code) && x.Status == 1);
            }
            if (product.Supplier_ID != null)
            {
                result = result.Where(x => x.Supplier_ID.Contains(product.Supplier_ID) && x.Status == 1);
            }

            return result.ToList();
        }
        public string getCateIdByProductCode(string pCode)
        {
            var cateId = "";
            List<Product> productList = db.Products.Where(x => x.Product_code.Equals(pCode) && x.Status == 1).ToList();
            foreach (Product item in productList)
            {
                cateId = item.Category_ID;
            }
            return cateId;
        }

        //get product_id by supplier_id
        public List<Product> lstProductBySupplierId(string supplierId)
        {
            return db.Products.Where(x => x.Supplier_ID.Contains(supplierId) && x.Status == 1).ToList();
        }
        //get product list by product id
        public Product getProductByProductId(int productId)
        {
            Product product = new Product();
            product = db.Products.SingleOrDefault(x => x.Product_ID == productId && x.Status == 1);
            return product;
        }
        //get list product by from price before VAT
        public List<Product> getProductListByPriceBeforeVAT(decimal priceFrom, Boolean checkboxValue)
        {
            if (checkboxValue)
            {
                return db.Products.Where(x => x.Price_before_VAT_USD >= priceFrom && x.Status == 1).ToList();
            }
            return db.Products.Where(x => (x.Price_before_VAT_VND + (x.Price_before_VAT_VND * x.VAT)) >= priceFrom && x.Status == 1).ToList();
        }
        //function search for GiaSanPham
        public List<Product> getLstSearch(Product product, decimal priceFrom, decimal priceTo, Boolean checkboxValue)
        {
            var result = db.Products.AsQueryable();
            if (product.Category_ID != null && product.Category_ID != "0")
            {
                result = result.Where(x => x.Category_ID == product.Category_ID && x.Status == 1);
            }
            if (product.Product_code != null)
            {
                result = result.Where(x => x.Product_code.Equals(product.Product_code) && x.Status == 1);
            }
            if (priceFrom != 0 && checkboxValue)
            {
                result = result.Where(x => x.Price_before_VAT_VND >= priceFrom && x.Status == 1);
            }
            if (priceFrom != 0 && !checkboxValue)
            {
                result = result.Where(x => (x.Price_before_VAT_VND + (x.Price_before_VAT_VND * (x.VAT / 100)) >= priceFrom && x.Status == 1));
            }
            if (priceTo != 0 && checkboxValue)
            {
                result = result.Where(x => x.Price_before_VAT_VND <= priceTo && x.Status == 1);
            }
            if (priceTo != 0 && !checkboxValue)
            {
                result = result.Where(x => (x.Price_before_VAT_VND + (x.Price_before_VAT_VND * (x.VAT / 100))) <= priceTo && x.Status == 1);
            }
            if (priceFrom != 0 && priceTo != 0 && checkboxValue)
            {
                result = result.Where(x => (priceFrom <= x.Price_before_VAT_VND && x.Price_before_VAT_VND <= priceTo) && x.Status == 1);
            }
            if (priceFrom != 0 && priceTo != 0 && !checkboxValue)
            {
                result = result.Where(x => (priceFrom <= (x.Price_before_VAT_VND + x.Price_before_VAT_VND * (x.VAT / 100)) && (x.Price_before_VAT_VND + x.Price_before_VAT_VND * (x.VAT / 100)) <= priceTo) && x.Status == 1);
            }
            return result.ToList();
        }
    }
}

