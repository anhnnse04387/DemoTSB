namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Edit_history
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Order_ID { get; set; }

        public byte Edit_code { get; set; }

        public int? Product_ID { get; set; }

        public int? Quantity_change { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_change { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public virtual Order_total Order_total { get; set; }
    }
}
