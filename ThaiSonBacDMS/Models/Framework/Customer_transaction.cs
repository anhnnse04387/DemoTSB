namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_transaction
    {
        [Key]
        [StringLength(10)]
        public string Transaction_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        [StringLength(10)]
        public string Customer_ID { get; set; }

        [StringLength(10)]
        public string Order_ID { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Order_total { get; set; }

        public byte? VAT { get; set; }

        [Column(TypeName = "money")]
        public decimal? Pay { get; set; }

        [Column(TypeName = "money")]
        public decimal? Debt { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public string Note { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Order_total Order_total1 { get; set; }
    }
}
