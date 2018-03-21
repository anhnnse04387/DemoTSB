namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detail_stock_in
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? Stock_in_ID { get; set; }

        public int? Product_ID { get; set; }

        public int? Quantities { get; set; }

        public string Note { get; set; }

        public virtual Stock_in Stock_in { get; set; }
    }
}
