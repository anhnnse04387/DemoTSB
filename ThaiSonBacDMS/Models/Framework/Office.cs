namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Office")]
    public partial class Office
    {
        [Key]
        public byte Office_ID { get; set; }

        [StringLength(50)]
        public string Office_name { get; set; }

        public byte? Role_ID { get; set; }
    }
}
