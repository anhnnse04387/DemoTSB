namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PO")]
    public partial class PO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PO()
        {
            PO_Items = new HashSet<PO_Items>();
            Purchase_invoice = new HashSet<Purchase_invoice>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PO_ID { get; set; }

        [StringLength(20)]
        public string PO_no { get; set; }

        public int? Supplier_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_create { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_request_ex_work { get; set; }

        [StringLength(50)]
        public string Payment { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PO_Items> PO_Items { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_invoice> Purchase_invoice { get; set; }
    }
}
