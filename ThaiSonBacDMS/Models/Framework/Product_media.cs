namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_media
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? Product_ID { get; set; }

        public int? Media_ID { get; set; }

        public virtual Medium Medium { get; set; }

        public virtual Product Product { get; set; }
    }
}
