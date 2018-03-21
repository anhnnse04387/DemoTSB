namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier_transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Transaction_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        public int? Supplier_ID { get; set; }

        public int? Purchase_invoice_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Old_debt { get; set; }

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

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Purchase_invoice Purchase_invoice { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
