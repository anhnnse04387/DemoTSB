namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_Function
    {
        public byte ID { get; set; }

        public byte? Role_ID { get; set; }

        [StringLength(20)]
        public string Function_ID { get; set; }

        [StringLength(10)]
        public string Permission { get; set; }

        public virtual Role_detail Role_detail { get; set; }
    }
}
