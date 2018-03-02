namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KPI_customer
    {
        [Key]
        [StringLength(10)]
        public string KPI_ID { get; set; }

        [StringLength(10)]
        public string Customer_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_from { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_to { get; set; }

        [StringLength(50)]
        public string Target_name { get; set; }

        [Column(TypeName = "money")]
        public decimal? Target_detail { get; set; }
    }
}
