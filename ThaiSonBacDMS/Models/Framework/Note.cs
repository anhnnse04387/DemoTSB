namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        [Key]
        [StringLength(10)]
        public string Note_ID { get; set; }

        [StringLength(10)]
        public string Account_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        public string Contents { get; set; }

        public virtual Account Account { get; set; }
    }
}
