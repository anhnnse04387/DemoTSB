namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [StringLength(10)]
        public string Notif_ID { get; set; }

        public DateTime? Notif_date { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public int? Status { get; set; }
    }
}
