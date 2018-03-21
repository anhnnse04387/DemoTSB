namespace Models.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ThaiSonBacDMSDbContext : DbContext
    {
        public ThaiSonBacDMSDbContext()
            : base("name=ThaiSonBacDMSDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Account_role> Account_role { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customer_transaction> Customer_transaction { get; set; }
        public virtual DbSet<Delivery_Method> Delivery_Method { get; set; }
        public virtual DbSet<Detail_stock_in> Detail_stock_in { get; set; }
        public virtual DbSet<Detail_stock_out> Detail_stock_out { get; set; }
        public virtual DbSet<Edit_history> Edit_history { get; set; }
        public virtual DbSet<Exchange_rate> Exchange_rate { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<History_price> History_price { get; set; }
        public virtual DbSet<KPI_customer> KPI_customer { get; set; }
        public virtual DbSet<KPI_emp> KPI_emp { get; set; }
        public virtual DbSet<KPI_supplier> KPI_supplier { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Order_detail_status> Order_detail_status { get; set; }
        public virtual DbSet<Order_items> Order_items { get; set; }
        public virtual DbSet<Order_part> Order_part { get; set; }
        public virtual DbSet<Order_total> Order_total { get; set; }
        public virtual DbSet<PO> POes { get; set; }
        public virtual DbSet<PO_Items> PO_Items { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_media> Product_media { get; set; }
        public virtual DbSet<Purchase_invoice> Purchase_invoice { get; set; }
        public virtual DbSet<Purchase_invoice_Items> Purchase_invoice_Items { get; set; }
        public virtual DbSet<Role_detail> Role_detail { get; set; }
        public virtual DbSet<Role_Function> Role_Function { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Stock_in> Stock_in { get; set; }
        public virtual DbSet<Stock_out> Stock_out { get; set; }
        public virtual DbSet<Sub_category> Sub_category { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Supplier_transaction> Supplier_transaction { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Account_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_name)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Account_role>()
                .Property(e => e.Account_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Customer_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Media_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Tax_code)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Current_debt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Transaction_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Customer_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Order_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Pay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.Debt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer_transaction>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Detail_stock_in>()
                .Property(e => e.Stock_in_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Detail_stock_in>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Detail_stock_out>()
                .Property(e => e.Stock_out_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Detail_stock_out>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Detail_stock_out>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Edit_history>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Edit_history>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Edit_history>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Exchange_rate>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Exchange_rate>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Function>()
                .Property(e => e.Function_ID)
                .IsUnicode(false);

            modelBuilder.Entity<History_price>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<History_price>()
                .Property(e => e.History_code)
                .IsUnicode(false);

            modelBuilder.Entity<History_price>()
                .Property(e => e.CIF)
                .HasPrecision(19, 4);

            modelBuilder.Entity<History_price>()
                .Property(e => e.TAX)
                .HasPrecision(19, 4);

            modelBuilder.Entity<History_price>()
                .Property(e => e.Price_before_VAT_VND)
                .HasPrecision(19, 4);

            modelBuilder.Entity<History_price>()
                .Property(e => e.Price_before_VAT_USD)
                .HasPrecision(19, 4);

            modelBuilder.Entity<History_price>()
                .Property(e => e.VAT)
                .HasPrecision(19, 4);

            modelBuilder.Entity<History_price>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_customer>()
                .Property(e => e.KPI_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_customer>()
                .Property(e => e.Customer_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_customer>()
                .Property(e => e.Target_detail)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KPI_emp>()
                .Property(e => e.KPI_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_emp>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_emp>()
                .Property(e => e.Target_detail)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KPI_supplier>()
                .Property(e => e.KPI_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_supplier>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<KPI_supplier>()
                .Property(e => e.Target_detail)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Medium>()
                .Property(e => e.Media_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Medium>()
                .Property(e => e.Upload_by)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Note_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Account_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Notif_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_detail_status>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_detail_status>()
                .Property(e => e.Order_part_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_detail_status>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_items>()
                .Property(e => e.Order_part_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_items>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_items>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_items>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Order_part_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Customer_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Invoice_number)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.VAT)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Total_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Sales_user_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Stocker_user_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Shiper_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_part>()
                .Property(e => e.Driver_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.Order_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.Customer_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.Sub_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.VAT)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.Order_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_total>()
                .Property(e => e.Total_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_total>()
                .HasMany(e => e.Edit_history)
                .WithRequired(e => e.Order_total)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_total>()
                .HasMany(e => e.Order_detail_status)
                .WithRequired(e => e.Order_total)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_total>()
                .HasMany(e => e.Order_items)
                .WithRequired(e => e.Order_total)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_total>()
                .HasMany(e => e.Order_part)
                .WithRequired(e => e.Order_total)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PO>()
                .Property(e => e.PO_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PO>()
                .Property(e => e.PO_no)
                .IsUnicode(false);

            modelBuilder.Entity<PO>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PO>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PO>()
                .Property(e => e.Total_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PO_Items>()
                .Property(e => e.PO_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PO_Items>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PO_Items>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Sub_category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CIF)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.TAX)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price_before_VAT_VND)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price_before_VAT_USD)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.VAT)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product_media>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product_media>()
                .Property(e => e.Media_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.Purchase_invoice_no)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.PO_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.Total_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_invoice>()
                .Property(e => e.Commodity)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice_Items>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice_Items>()
                .Property(e => e.Product_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_invoice_Items>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Role_Function>()
                .Property(e => e.Function_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_in>()
                .Property(e => e.Stock_in_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_in>()
                .Property(e => e.Order_part_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_in>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_in>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_out>()
                .Property(e => e.Stock_out_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_out>()
                .Property(e => e.Order_part_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_out>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Stock_out>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_category>()
                .Property(e => e.Sub_category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_category>()
                .Property(e => e.Category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Media_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Tax_code)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Current_debt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Transaction_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Supplier_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Purchase_invoice_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Old_debt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Order_total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Pay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.Debt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Supplier_transaction>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.User_ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Insurance_Code)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Avatar_ID)
                .IsUnicode(false);
        }
    }
}
