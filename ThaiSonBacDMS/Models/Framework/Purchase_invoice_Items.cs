namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_invoice_Items
    {
        public byte ID { get; set; }

        [StringLength(10)]
        public string Purchase_invoice_ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public string NOTE { get; set; }

        public virtual Purchase_invoice Purchase_invoice { get; set; }
    }
}
