namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_media
    {
        public byte ID { get; set; }

        [StringLength(10)]
        public string Product_ID { get; set; }

        [StringLength(10)]
        public string Media_ID { get; set; }

        public virtual Medium Medium { get; set; }

        public virtual Product Product { get; set; }
    }
}
