namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exchange_rate
    {
        [Key]
        public int Change_ID { get; set; }

        public int? User_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_change { get; set; }

        public virtual User User { get; set; }
    }
}
