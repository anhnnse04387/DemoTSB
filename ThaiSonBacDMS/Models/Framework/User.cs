namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Accounts = new HashSet<Account>();
            Exchange_rate = new HashSet<Exchange_rate>();
        }

        [Key]
        [StringLength(10)]
        public string User_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string User_name { get; set; }

        public byte? Office_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_birth { get; set; }

        public string User_Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [StringLength(20)]
        public string Insurance_Code { get; set; }

        public byte? Role_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_created { get; set; }

        [StringLength(10)]
        public string Avatar_ID { get; set; }

        public int? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exchange_rate> Exchange_rate { get; set; }
    }
}
