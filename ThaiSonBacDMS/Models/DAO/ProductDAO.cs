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
            return db.Products.Where(x => x.Product_name.Trim().ToLower().Contains(code.Trim().ToLower()) && x.Status == 1).ToList();
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
        //gia san pham
        public List<GiaSanPham> giaSanPham()
        {
            List<GiaSanPham> lst = new List<GiaSanPham>();
            var query = from p in db.Products
                        where p.Status == 1
                        select p;
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    GiaSanPham obj = new GiaSanPham();

                    obj.pName = item.Product_name;
                    obj.pParam = item.Product_parameters;
                    obj.cif = item.CIF_USD;
                    obj.vat = item.VAT;
                    obj.price_before_vat_vnd = item.Price_before_VAT_VND;
                    obj.price_before_vat_usd = item.Price_before_VAT_USD;
                    obj.price_after_vat_vnd = item.Price_before_VAT_VND + (item.Price_before_VAT_VND * item.VAT) / 100;
                    obj.price_after_vat_usd = item.Price_before_VAT_USD + (item.Price_before_VAT_USD * item.VAT) / 100;
                    obj.pCateId = item.Category_ID;
                    obj.pId = item.Product_ID.ToString();

                    lst.Add(obj);

                }
            }
            return lst;
        }
        //search gia san pham
        public List<GiaSanPham> giaSanPham(Product product, decimal priceFrom, decimal priceTo, Boolean checkboxValue)
        {
            List<GiaSanPham> lst = new List<GiaSanPham>();
            var result = from p in db.Products
                         where p.Status == 1
                         select p;
            if (product.Category_ID != null && product.Category_ID != "0")
            {
                result = result.Where(x => x.Category_ID == product.Category_ID && x.Status == 1);
            }
            if (product.Product_code != null)
            {
                result = result.Where(x => x.Product_name.Equals(product.Product_code) && x.Status == 1);
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
            if (result.Count() != 0)
            {
                foreach (var item in result)
                {
                    GiaSanPham obj = new GiaSanPham();

                    obj.pName = item.Product_name;
                    obj.pParam = item.Product_parameters;
                    obj.cif = item.CIF_VND;
                    obj.vat = item.VAT;
                    obj.price_before_vat_vnd = item.Price_before_VAT_VND;
                    obj.price_before_vat_usd = item.Price_before_VAT_USD;
                    obj.price_after_vat_vnd = item.Price_before_VAT_VND + (item.Price_before_VAT_VND * item.VAT) / 100;
                    obj.price_after_vat_usd = item.Price_before_VAT_USD + (item.Price_before_VAT_USD * item.VAT) / 100;
                    obj.pCateId = item.Category_ID;
                    obj.pId = item.Product_ID.ToString();

                    lst.Add(obj);

                }
            }
            return lst;
        }
        public int insertProduct(Product item)
        {
            db.Products.Add(item);
            return db.SaveChanges();
        }
        //autocomplete
        public List<Product> getLstProductSearch(string value)
        {
            return db.Products.Where((x => x.Product_name.Contains(value) && x.Status == 1)).ToList();
        }
        //autocomplete san pham dang kinh doanh
        public List<Autocomplete> autocompleteSanPhamKinhDoanh(string searchValue)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join s in db.Sub_category on p.Sub_category_ID.ToString() equals s.Sub_category_ID.ToString()
                        where p.Status == 1 && p.Product_name.ToLower().Contains(searchValue.ToLower())
                        select new { p, sup, c };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    Autocomplete obj = new Autocomplete();
                    obj.key = item.p.Product_name;
                    lst.Add(obj);
                }

            }
            return lst;
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
        public string getCateIdByProductName(string productName)
        {
            var cateId = "";
            List<Product> productList = db.Products.Where(x => x.Product_name.Equals(productName) && x.Status == 1).ToList();
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
            product = db.Products.SingleOrDefault(x => x.Product_ID == productId);
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
                result = result.Where(x => x.Product_name.Equals(product.Product_code) && x.Status == 1);
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
        public List<SanPham> getLstSearchSanPham(Product product)
        {
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join s in db.Sub_category on p.Sub_category_ID.ToString() equals s.Sub_category_ID.ToString()
                        where p.Status == 1
                        select new { p, sup, c, s };
            if (product.Sub_category_ID != null)
            {
                query = query.Where(x => x.p.Sub_category_ID == product.Sub_category_ID);
            }
            if (product.Category_ID != null)
            {
                query = query.Where(x => x.p.Category_ID == product.Category_ID);
            }
            if (product.Supplier_ID != null)
            {
                query = query.Where(x => x.p.Supplier_ID.Contains(product.Supplier_ID));
            }
            if (product.Product_name != null)
            {
                query = query.Where(x => x.p.Product_name.ToLower().Equals(product.Product_name.ToLower()));
            }
            List<SanPham> lst = new List<SanPham>();

            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    SanPham sp = new SanPham();

                    sp.cateId = item.p.Category_ID;
                    sp.cateName = item.c.Category_name;
                    sp.supplierId = item.sup.Supplier_ID.ToString();
                    sp.supplierName = item.sup.Supplier_name;
                    sp.pName = item.p.Product_name;
                    sp.pParam = item.p.Product_parameters;
                    sp.productId = item.p.Product_ID.ToString();
                    sp.quantity = item.p.Quantity_in_carton.ToString();
                    sp.subCate = item.s.Sub_category_name;

                    lst.Add(sp);
                }
            }
            return lst;
        }
        public List<SanPham> sanPhamDangKinhDoanh()
        {
            List<SanPham> lst = new List<SanPham>();
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join subCate in db.Sub_category on p.Sub_category_ID.ToString() equals subCate.Sub_category_ID.ToString()
                        where p.Status == 1
                        select new { p, sup, c, subCate };

            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    SanPham sp = new SanPham();

                    sp.cateId = item.p.Category_ID;
                    sp.cateName = item.c.Category_name;
                    sp.supplierId = item.sup.Supplier_ID.ToString();
                    sp.supplierName = item.sup.Supplier_name;
                    sp.pName = item.p.Product_name;
                    sp.pParam = item.p.Product_parameters;
                    sp.productId = item.p.Product_ID.ToString();
                    sp.quantity = item.p.Quantity_in_carton.ToString();
                    sp.subCate = item.subCate.Sub_category_name;

                    lst.Add(sp);
                }
            }
            return lst;
        }

        //functions ton kho
        public List<TonKho> getAllProduct()
        {
            var query = from p in db.Products
                        join dsi in db.Detail_stock_in on p.Product_ID equals dsi.Product_ID
                        join dso in db.Detail_stock_out on p.Product_ID equals dso.Product_ID
                        where p.Status == 1
                        orderby p.Product_ID ascending
                        select new { p, nhap = dsi.Quantities, xuat = dso.Quantities, ton = dsi.Quantities - dso.Quantities };
            List<TonKho> lstTonKho = new List<TonKho>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    TonKho obj = new TonKho();

                    obj.productName = item.p.Product_name;
                    obj.productParameter = item.p.Product_parameters;
                    obj.tongNhap = item.nhap;
                    obj.tongXuat = item.xuat;
                    obj.tongTon = item.ton;
                    obj.productId = item.p.Product_ID;
                    obj.categoryId = item.p.Category_ID;

                    lstTonKho.Add(obj);
                }
            }
            return lstTonKho;
        }
        public List<TonKho> getLstProductSearch(string fromValue, string toValue, string pCode, string categorySearch)
        {
            var query = from p in db.Products
                        join dsi in db.Detail_stock_in on p.Product_ID equals dsi.Product_ID
                        join dso in db.Detail_stock_out on p.Product_ID equals dso.Product_ID
                        where p.Status == 1
                        orderby p.Product_ID ascending
                        select new { p, nhap = dsi.Quantities, xuat = dso.Quantities, ton = dsi.Quantities - dso.Quantities };
            if (pCode != null)
            {
                query = query.Where(x => x.p.Product_name.Equals(pCode));
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
                query = query.Where(x => x.ton >= valueSearch1 && x.ton <= valueSearch2);
            }
            List<TonKho> lstTonKho = new List<TonKho>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    TonKho obj = new TonKho();

                    obj.productName = item.p.Product_name;
                    obj.productParameter = item.p.Product_parameters;
                    obj.tongNhap = item.nhap;
                    obj.tongXuat = item.xuat;
                    obj.tongTon = item.ton;
                    obj.productId = item.p.Product_ID;
                    obj.categoryId = item.p.Category_ID;

                    lstTonKho.Add(obj);
                }
            }
            return lstTonKho;
        }
        public List<TonKho> getLstProductTonKho(string searchValue)
        {
            var query = from p in db.Products
                        join dsi in db.Detail_stock_in on p.Product_ID equals dsi.Product_ID
                        join dso in db.Detail_stock_out on p.Product_ID equals dso.Product_ID
                        where p.Status == 1 && p.Product_name.Contains(searchValue)
                        orderby p.Product_ID ascending
                        select new { p, nhap = dsi.Quantities, xuat = dso.Quantities, ton = dsi.Quantities - dso.Quantities };
            List<TonKho> lstTonKho = new List<TonKho>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    TonKho obj = new TonKho();

                    obj.productName = item.p.Product_name;

                    lstTonKho.Add(obj);
                }
            }
            return lstTonKho;
        }
        //functions san pham ngung kinh doanh
        //public List<Product> sanPhamNgungKinhDoanh()
        //{
        //    return db.Products.Where(x => x.Status == 0).ToList();
        //}
        public List<Autocomplete> autocompleteSanPhamNgungKD(string searchValue)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join s in db.Sub_category on p.Sub_category_ID.ToString() equals s.Sub_category_ID.ToString()
                        where p.Status == 0 && p.Product_name.ToLower().Contains(searchValue.ToLower())
                        select new { p, sup, c, s };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    Autocomplete obj = new Autocomplete();
                    obj.key = item.p.Product_name;
                    lst.Add(obj);
                }

            }
            return lst;
        }
        public List<SanPham> getLstSearchSanPhamNgungKD(Product product)
        {
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join s in db.Sub_category on p.Sub_category_ID.ToString() equals s.Sub_category_ID.ToString()
                        where p.Status == 0
                        select new { p, sup, c, s };
            if (product.Category_ID != null)
            {
                query = query.Where(x => x.p.Category_ID == product.Category_ID);
            }
            if (product.Supplier_ID != null)
            {
                query = query.Where(x => x.p.Supplier_ID.Contains(product.Supplier_ID));
            }
            if (product.Product_name != null)
            {
                query = query.Where(x => x.p.Product_name.ToLower().Equals(product.Product_name.ToLower()));
            }
            if (product.Sub_category_ID != null)
            {
                query = query.Where(x => x.p.Sub_category_ID.ToString().Equals(product.Sub_category_ID.ToString()));
            }
            List<SanPham> lst = new List<SanPham>();

            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    SanPham sp = new SanPham();

                    sp.cateId = item.p.Category_ID;
                    sp.cateName = item.c.Category_name;
                    sp.supplierId = item.sup.Supplier_ID.ToString();
                    sp.supplierName = item.sup.Supplier_name;
                    sp.pName = item.p.Product_name;
                    sp.pParam = item.p.Product_parameters;
                    sp.productId = item.p.Product_ID.ToString();
                    sp.quantity = item.p.Quantity_in_carton.ToString();
                    sp.subCate = item.s.Sub_category_name;

                    lst.Add(sp);
                }
            }
            return lst;
        }

        public List<SanPham> sanPhamNgungKinhDoanh()
        {
            List<SanPham> lst = new List<SanPham>();
            var query = from p in db.Products
                        join sup in db.Suppliers on p.Supplier_ID.ToString() equals sup.Supplier_ID.ToString()
                        join c in db.Categories on p.Category_ID.ToString() equals c.Category_ID.ToString()
                        join s in db.Sub_category on p.Sub_category_ID.ToString() equals s.Sub_category_ID.ToString()
                        where p.Status == 0
                        select new { p, sup, c, s };

            if (query.Count() != 0)
            {
                foreach (var item in query)
                {
                    SanPham sp = new SanPham();

                    sp.cateId = item.p.Category_ID;
                    sp.cateName = item.c.Category_name;
                    sp.supplierId = item.sup.Supplier_ID.ToString();
                    sp.supplierName = item.sup.Supplier_name;
                    sp.pName = item.p.Product_name;
                    sp.pParam = item.p.Product_parameters;
                    sp.productId = item.p.Product_ID.ToString();
                    sp.quantity = item.p.Quantity_in_carton.ToString();
                    sp.subCate = item.s.Sub_category_name;

                    lst.Add(sp);
                }
            }
            return lst;
        }
        public List<Product> sanPhamNgungKinhDoanh(Product product)
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
                query = query.Where(x => x.p.Supplier_ID.Contains(product.Supplier_ID));
            }
            if (product.Sub_category_ID != null)
            {
                query = query.Where(x => x.p.Sub_category_ID.ToString().Equals(product.Sub_category_ID));
            }
            List<Product> lst = new List<Product>();
            if (query.Count() != 0)
            {
                foreach (var item in query)
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
                foreach (var item in query)
                {
                    lst.Add(item.p);
                }
            }
            return lst;
        }

        public int insertProduct(string pCode, string pName,
            string pParam, string pSupplier, string pCategory, string pSubCate,
            int quantity_Carton, string pDescription, string pDetail,
            decimal cifVND, decimal cifUSD, decimal beforeVatVND, decimal beforeVATUSD, int vat)
        {
            Product p = new Product();
            p.Product_code = pCode;
            p.Product_name = pName;
            p.Product_parameters = pParam;
            p.Supplier_ID = pSupplier;
            p.Category_ID = pCategory;
            p.Sub_category_ID = pSubCate;
            p.Quantity_in_carton = quantity_Carton;
            p.Overview = pDescription;
            p.Specification = pDetail;
            p.CIF_USD = cifUSD;
            p.CIF_VND = cifVND;
            p.Price_before_VAT_USD = beforeVATUSD;
            p.Price_before_VAT_VND = beforeVatVND;
            p.VAT = vat;
            p.Quantities_in_inventory = 0;
            p.Status = 1;
            db.Products.Add(p);
            db.SaveChanges();
            return p.Product_ID;
        }
        public Product getDetailProduct(string productId)
        {
            return db.Products.Single(x => x.Product_ID.ToString().Equals(productId));
        }
        public List<string> checkExistedCode(string productId)
        {
            List<string> lst = new List<string>();
            var query = from p in db.Products
                        where p.Product_ID.ToString() != productId
                        select new { p };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    lst.Add(item.p.Product_code);
                }
            }
            lst = lst.Distinct().ToList();
            return lst;

        }
        public List<string> checkExistedParam(string productId)
        {
            List<string> lst = new List<string>();
            var query = from p in db.Products
                        where p.Product_ID.ToString() != productId
                        select new { p };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    lst.Add(item.p.Product_parameters);
                }
            }
            lst = lst.Distinct().ToList();
            return lst;

        }
        public List<string> checkExistedName(string productId)
        {
            List<string> lst = new List<string>();
            var query = from p in db.Products
                        where p.Product_ID.ToString() != productId
                        select new { p };
            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    lst.Add(item.p.Product_name);
                }
            }
            lst = lst.Distinct().ToList();
            return lst;

        }
        public void deleteRecord(string product_Id)
        {
            var query = from m in db.Product_media
                        where m.Product_ID.ToString().Equals(product_Id)
                        select m;
            if (query != null)
            {
                foreach (var item in query)
                {
                    db.Product_media.Remove(item);
                }

            }
            db.SaveChanges();
        }

        public int updateProduct(Product product)
        {
            var query = db.Products.Single(x => x.Product_ID == product.Product_ID);
            if (query != null)
            {
                query.Product_code = product.Product_code;
                query.Product_name = product.Product_name;
                query.Product_parameters = product.Product_parameters;
                query.Supplier_ID = product.Supplier_ID;
                query.Category_ID = product.Category_ID;
                query.Sub_category_ID = product.Sub_category_ID;
                query.Quantity_in_carton = product.Quantity_in_carton;
                query.Overview = product.Overview;
                query.Specification = product.Specification;
                query.CIF_USD = product.CIF_USD;
                query.CIF_VND = product.CIF_VND;
                query.Price_before_VAT_USD = product.Price_before_VAT_USD;
                query.Price_before_VAT_VND = product.Price_before_VAT_VND;
                query.Status = product.Status;
                query.VAT = product.VAT;

            }
            int rowUpdated = db.SaveChanges();
            return rowUpdated;
        }
    }
}

