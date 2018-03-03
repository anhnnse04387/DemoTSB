namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Function
    {
        [Key]
        [StringLength(20)]
        public string Function_ID { get; set; }

        public string Function_name { get; set; }
    }
}
