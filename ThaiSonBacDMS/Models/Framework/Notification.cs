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
        public int Notif_ID { get; set; }

        public DateTime? Notif_date { get; set; }

        public int? User_ID { get; set; }

        public byte? Role_ID { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public int? Status { get; set; }
    }
}
