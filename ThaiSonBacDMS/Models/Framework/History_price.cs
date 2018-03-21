namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History_price
    {
        public int? Product_ID { get; set; }

        [Key]
        [StringLength(10)]
        public string History_code { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_change { get; set; }

        [Column(TypeName = "money")]
        public decimal? CIF { get; set; }

        [Column(TypeName = "money")]
        public decimal? TAX { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_VND { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_USD { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public virtual Product Product { get; set; }
    }
}
