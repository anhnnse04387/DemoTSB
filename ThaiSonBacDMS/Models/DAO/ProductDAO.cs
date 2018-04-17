using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public int productCount()
        {
            return db.Products.Count();
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
        //function search for SanPhamDangKinhDoanh
        public List<Product> getLstSearch(Product product, string fromDate, string toDate)
        {
            var query = from p in db.Products
                        join d in db.Detail_stock_in on p.Product_ID equals d.Product_ID
                        join s in db.Stock_in on d.Stock_in_ID equals s.Stock_in_ID
                        where p.Status == 1
                        select new { p, s.Date_import };
            if (product.Category_ID != null)
            {
                query = query.Where(x => x.p.Category_ID == product.Category_ID);
            }
            if (product.Supplier_ID != null)
            {
                query = query.Where(x => x.p.Supplier_ID.Contains(product.Supplier_ID));
            }
            if (product.Product_code != null)
            {
                query = query.Where(x => x.p.Product_code.Equals(product.Product_code));
            }
            if (fromDate != null && toDate == null)
            {
                DateTime fromDateValue = DateTime.Parse(fromDate);
                query = query.Where(x => x.Date_import >= fromDateValue);
            }
            if (toDate != null && fromDate == null)
            {
                DateTime toDateValue = DateTime.Parse(toDate);
                toDateValue = DateTime.Parse(toDateValue.ToString("mm/dd/yyyy"));
                query = query.Where(x => x.Date_import <= toDateValue);
            }
            if (fromDate != null && toDate != null)
            {
                DateTime fromDateValue = DateTime.Parse(fromDate);
                DateTime toDateValue = DateTime.Parse(toDate);
                query = query.Where(x => x.Date_import <= toDateValue && x.Date_import >= fromDateValue);
            }
            List<Product> lstProduct = new List<Product>();
            if (query.Count() != 0)
            {
                foreach (var itemProduct in query)
                {
                    lstProduct.Add(itemProduct.p);
                }
            }
            return lstProduct;
        }
        //functions ton kho
        public List<Product> getAllProduct()
        {
            var query = from p in db.Products
                        join dsi in db.Detail_stock_in on p.Product_ID equals dsi.Product_ID
                        join dso in db.Detail_stock_out on p.Product_ID equals dso.Product_ID
                        where p.Status == 1 orderby p.Product_ID ascending
                        select p;
            List<Product> lstProduct = new List<Product>();
            if (query.Count() != 0)
            {
                foreach (Product itemProduct in query)
                {
                    lstProduct.Add(itemProduct);
                }
            }
            return lstProduct;
        }
        public List<Product> getLstProductSearch(string fromValue, string toValue, string pCode,string categorySearch)
        {
            var query = from p in db.Products
                        join dsi in db.Detail_stock_in on p.Product_ID equals dsi.Product_ID
                        join dso in db.Detail_stock_out on p.Product_ID equals dso.Product_ID
                        where p.Status == 1 orderby p.Product_ID ascending
                        select new { p, nhap = dsi.Quantities, xuat = dso.Quantities, ton = dsi.Quantities - dso.Quantities };
            if (pCode != null)
            {
                query = query.Where(x => x.p.Product_code.Equals(pCode));
            }
            if (categorySearch != null)
            {
                query = query.Where(x => x.p.Category_ID.Equals(categorySearch));
            }
            if (fromValue != null)
            {
                fromValue = fromValue.Replace(",", "");
                int valueSearch = Convert.ToInt32(fromValue);
                query = query.Where(x => x.ton >= valueSearch);
            }
            if (toValue != null)
            {
                toValue = toValue.Replace(",", "");
                int valueSearch = Convert.ToInt32(toValue);
                query = query.Where(x => x.ton <= valueSearch);
            }
            if (fromValue != null && toValue != null)
            {
                fromValue = fromValue.Replace(",", "");
                toValue = toValue.Replace(",", "");
                int valueSearch1 = Convert.ToInt32(fromValue);
                int valueSearch2 = Convert.ToInt32(toValue);
                query = query.Where(x => x.ton >= Convert.ToInt32(fromValue) && x.ton <= Convert.ToInt32(toValue));
            }
            List<Product> lstProduct = new List<Product>();
            if (query.Count() != 0)
            {
                foreach (var itemProduct in query)
                {
                    lstProduct.Add(itemProduct.p);
                }
            }
            return lstProduct;
        }
        //functions san pham ngung kinh doanh
        public List<Product> sanPhamNgungKinhDoanh()
        {
            return db.Products.Where(x => x.Status == 0).ToList();
        }
        public List<Product> sanPhamNgungKinhDoanh(Product product, string fromDate, string toDate)
        {
            var query = from p in db.Products
            join d in db.Detail_stock_in on p.Product_ID equals d.Product_ID
            join s in db.Stock_in on d.Stock_in_ID equals s.Stock_in_ID
            where p.Status == 0
            select new { p, s.Date_import };
            if (!String.IsNullOrEmpty(product.Category_ID))
            {
                query = query.Where(x => x.p.Category_ID.Equals(product.Category_ID));
            }
            if (!string.IsNullOrEmpty(product.Product_code))
            {
                query = query.Where(x => x.p.Product_code.Equals(product.Product_code));
            }
            if (!String.IsNullOrEmpty(product.Supplier_ID))
            {
                query = query.Where(x=>x.p.Supplier_ID.Contains(product.Supplier_ID));
            }
            if(!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = Convert.ToDateTime(fromDate);
                query = query.Where(x=>x.Date_import >= fromValue);
            }
            if(string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime toValue = Convert.ToDateTime(toDate);
                query = query.Where(x=>x.Date_import <= toValue);
            }
            if(!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime fromValue = Convert.ToDateTime(fromDate);
                DateTime toValue = Convert.ToDateTime(toDate);
                query = query.Where(x=>x.Date_import >= fromValue && x.Date_import <= toValue);
            }
            List<Product> lst = new List<Product>();
            if(query.Count() !=0)
            {
                foreach(var item in query)
                {
                    lst.Add(item.p);
                }
            }
            return lst;
        }
        public List<Product> searchNgungKinhDoanh(string value)
        {
            var query = from p in db.Products
                        join d in db.Detail_stock_in on p.Product_ID equals d.Product_ID
                        join s in db.Stock_in on d.Stock_in_ID equals s.Stock_in_ID
                        where p.Status == 0
                        select new { p, s.Date_import };
            query = query.Where(x => x.p.Product_code.Contains(value) || x.p.Product_code.Contains(value) && x.p.Status == 0);
            List<Product> lst = new List<Product>();
            if (query.Count() != 0)
            {
                foreach(var item in query)
                {
                    lst.Add(item.p);
                }
            }
            return lst;
        }
      
    }
}

