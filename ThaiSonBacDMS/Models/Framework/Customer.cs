namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Customer_transaction = new HashSet<Customer_transaction>();
        }

        [Key]
        public int Customer_ID { get; set; }

        [StringLength(50)]
        public string Customer_name { get; set; }

        public int? Media_ID { get; set; }

        public string Delivery_address { get; set; }

        public string Export_invoice_address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public string Mail { get; set; }

        [StringLength(50)]
        public string Tax_code { get; set; }

        [StringLength(20)]
        public string Rank { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        [Column(TypeName = "money")]
        public decimal? Current_debt { get; set; }

        public int? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer_transaction> Customer_transaction { get; set; }
    }
}
