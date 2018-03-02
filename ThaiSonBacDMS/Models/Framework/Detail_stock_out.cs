namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detail_stock_out
    {
        public byte ID { get; set; }

        [StringLength(10)]
        public string Stock_out_ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        [StringLength(10)]
        public string Purchase_invoice_ID { get; set; }

        public int? Quantities { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Stock_out Stock_out { get; set; }
    }
}
