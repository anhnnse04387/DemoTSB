namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [StringLength(10)]
        public string Sub_category_ID { get; set; }

        [StringLength(10)]
        public string Category_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Sub_category_name { get; set; }

        public int? Status { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
