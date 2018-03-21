namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase_invoice()
        {
            Purchase_invoice_Items = new HashSet<Purchase_invoice_Items>();
            Supplier_transaction = new HashSet<Supplier_transaction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Purchase_invoice_ID { get; set; }

        [StringLength(20)]
        public string Purchase_invoice_no { get; set; }

        public int? PO_ID { get; set; }

        public int? Supplier_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_requested { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_price { get; set; }

        [StringLength(20)]
        public string Commodity { get; set; }

        [StringLength(20)]
        public string Payment_term { get; set; }

        public string Price_term { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Shipment_date { get; set; }

        [StringLength(30)]
        public string Post_of_Loading { get; set; }

        [StringLength(30)]
        public string Final_destination { get; set; }

        public string Packing { get; set; }

        public string Inspection { get; set; }

        public string Country_of_origin { get; set; }

        public string Validity { get; set; }

        public string Remarks { get; set; }

        public virtual PO PO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_invoice_Items> Purchase_invoice_Items { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier_transaction> Supplier_transaction { get; set; }
    }
}
