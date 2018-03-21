namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detail_stock_out
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? Stock_out_ID { get; set; }

        public int? Product_ID { get; set; }

        public int? Purchase_invoice_ID { get; set; }

        public int? Quantities { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Stock_out Stock_out { get; set; }
    }
}
