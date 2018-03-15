namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_part
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_part()
        {
            Order_detail_status = new HashSet<Order_detail_status>();
            Order_items = new HashSet<Order_items>();
            Stock_in = new HashSet<Stock_in>();
            Stock_out = new HashSet<Stock_out>();
        }

        [Required]
        [StringLength(10)]
        public string Order_ID { get; set; }

        public byte Part_ID { get; set; }

        [Key]
        [StringLength(10)]
        public string Order_part_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Customer_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date_created { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_completed { get; set; }

        [StringLength(10)]
        public string Invoice_number { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        public byte? Status_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_price { get; set; }

        [StringLength(10)]
        public string Sales_user_ID { get; set; }

        [StringLength(10)]
        public string Stocker_user_ID { get; set; }

        [StringLength(10)]
        public string Shiper_ID { get; set; }

        public byte? DeliverMethod_ID { get; set; }

        [StringLength(10)]
        public string Driver_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_reveice_invoice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_reveice_ballot { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detail_status> Order_detail_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_items> Order_items { get; set; }

        public virtual Order_total Order_total { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock_in> Stock_in { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock_out> Stock_out { get; set; }
    }
}
