namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Delivery_Method
    {
        [Key]
        public byte Method_ID { get; set; }

        [StringLength(50)]
        public string Method_name { get; set; }
    }
}
