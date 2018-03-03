namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detail_stock_in
    {
        public byte ID { get; set; }

        [StringLength(10)]
        public string Stock_in_ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        public int? Quantities { get; set; }

        public string Note { get; set; }

        public virtual Stock_in Stock_in { get; set; }
    }
}
