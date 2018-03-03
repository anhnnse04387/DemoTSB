namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_items
    {
        [Key]
        [StringLength(10)]
        public string Order_part_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Order_ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public byte? Discount { get; set; }

        public byte? Box { get; set; }

        [StringLength(1)]
        public string Note { get; set; }

        public virtual Order_part Order_part { get; set; }

        public virtual Order_total Order_total { get; set; }
    }
}
