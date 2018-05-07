namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_detail_status
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string Order_ID { get; set; }

        [StringLength(10)]
        public string Order_part_ID { get; set; }

        public byte? Status_ID { get; set; }

        public DateTime? Date_change { get; set; }

        public int? User_ID { get; set; }

        public virtual Order_total Order_total { get; set; }
    }
}
