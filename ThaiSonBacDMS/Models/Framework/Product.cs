namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            History_price = new HashSet<History_price>();
            Product_media = new HashSet<Product_media>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Product_ID { get; set; }

        [StringLength(50)]
        public string Product_code { get; set; }

        public string Product_name { get; set; }

        [StringLength(100)]
        public string Product_parameters { get; set; }

        [StringLength(255)]
        public string Supplier_ID { get; set; }

        [StringLength(10)]
        public string Category_ID { get; set; }

        [StringLength(10)]
        public string Sub_category_ID { get; set; }

        public int? Quantity_in_carton { get; set; }

        public string Overview { get; set; }

        public string Specification { get; set; }

        [Column(TypeName = "money")]
        public decimal? CIF_USD { get; set; }

        [Column(TypeName = "money")]
        public decimal? CIF_VND { get; set; }

        [Column(TypeName = "money")]
        public decimal? TAX { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_VND { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_USD { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        public int? Quantities_in_inventory { get; set; }

        public int? Status { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History_price> History_price { get; set; }

        public virtual Sub_category Sub_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_media> Product_media { get; set; }
    }
}
