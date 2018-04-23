namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_part
    {
        [Required]
        [StringLength(10)]
        public string Order_ID { get; set; }

        public byte? Part_ID { get; set; }

        [Key]
        [StringLength(10)]
        public string Order_part_ID { get; set; }

        public int Customer_ID { get; set; }

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

        public int? Sales_user_ID { get; set; }

        public int? Stocker_user_ID { get; set; }

        public int? Shiper_ID { get; set; }

        public byte? DeliverMethod_ID { get; set; }

        [StringLength(10)]
        public string Driver_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_reveice_invoice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_reveice_ballot { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_take_invoice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_take_ballot { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Order_total Order_total { get; set; }
    }
}
