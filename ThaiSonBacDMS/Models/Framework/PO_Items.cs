namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PO_Items
    {
        public byte ID { get; set; }

        [StringLength(10)]
        public string PO_ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(1)]
        public string NOTE { get; set; }

        public virtual PO PO { get; set; }
    }
}
