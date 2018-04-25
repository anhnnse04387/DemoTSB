namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account_role
    {
        public int ID { get; set; }

        public int? Account_ID { get; set; }

        public byte? Role_ID { get; set; }

        public virtual Account Account { get; set; }
    }
}
