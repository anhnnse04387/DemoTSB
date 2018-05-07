namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_items
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Order_part_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Order_ID { get; set; }

        public int? Product_ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public byte? Discount { get; set; }

        public float? Box { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Order_total Order_total { get; set; }
    }
}
