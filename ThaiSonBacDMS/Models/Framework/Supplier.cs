namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Supplier_transaction = new HashSet<Supplier_transaction>();
        }

        [Key]
        public int Supplier_ID { get; set; }

        [StringLength(50)]
        public string Supplier_name { get; set; }

        public int? Media_ID { get; set; }

        public string Supplier_address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Mail { get; set; }

        [StringLength(20)]
        public string Tax_code { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        [Column(TypeName = "money")]
        public decimal? Current_debt { get; set; }

        public int? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier_transaction> Supplier_transaction { get; set; }
    }
}
