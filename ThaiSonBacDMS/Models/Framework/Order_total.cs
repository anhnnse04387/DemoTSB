namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_total
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_total()
        {
            Customer_transaction = new HashSet<Customer_transaction>();
            Edit_history = new HashSet<Edit_history>();
            Order_detail_status = new HashSet<Order_detail_status>();
            Order_items = new HashSet<Order_items>();
            Order_part = new HashSet<Order_part>();
        }

        [Key]
        [StringLength(10)]
        public string Order_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Customer_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date_created { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_completed { get; set; }

        public int? Rate { get; set; }

        public byte? Status_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string User_ID { get; set; }

        [Required]
        public string Address_delivery { get; set; }

        public string Address_invoice_issuance { get; set; }

        [Column(TypeName = "money")]
        public decimal Sub_total { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        [Column(TypeName = "money")]
        public decimal? Order_discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_transaction> Customer_transaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Edit_history> Edit_history { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detail_status> Order_detail_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_items> Order_items { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_part> Order_part { get; set; }
    }
}
