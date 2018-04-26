namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PO_Items
    {
        public int ID { get; set; }

        public int? PO_ID { get; set; }

        public int? Product_ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(1)]
        public string NOTE { get; set; }

        public virtual PO PO { get; set; }
    }
}
