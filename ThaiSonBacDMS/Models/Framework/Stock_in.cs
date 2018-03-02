namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock_in
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock_in()
        {
            Detail_stock_in = new HashSet<Detail_stock_in>();
        }

        [Key]
        [StringLength(10)]
        public string Stock_in_ID { get; set; }

        [StringLength(10)]
        public string Order_part_ID { get; set; }

        [StringLength(10)]
        public string Purchase_invoice_ID { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_import { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detail_stock_in> Detail_stock_in { get; set; }

        public virtual Order_part Order_part { get; set; }
    }
}
