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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Note_ID { get; set; }

        public int? Account_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        public string Contents { get; set; }

        public virtual Account Account { get; set; }
    }
}
