namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History_price
    {
        public int ID { get; set; }

        public int? Product_ID { get; set; }

        public int History_code { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_change { get; set; }

        public int? Category_ID { get; set; }

        public int? Sub_category_ID { get; set; }

        public int? Quantity_in_carton { get; set; }

        [Column(TypeName = "money")]
        public decimal? CIF_USD { get; set; }

        [Column(TypeName = "money")]
        public decimal? CIF_VND { get; set; }

        public double? TAX { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_VND { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price_before_VAT_USD { get; set; }

        public int? VAT { get; set; }

        public int? Status { get; set; }

        public int? User_ID { get; set; }

        public virtual Product Product { get; set; }
    }
}
