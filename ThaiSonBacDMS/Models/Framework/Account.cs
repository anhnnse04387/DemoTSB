namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Account_role = new HashSet<Account_role>();
            Notes = new HashSet<Note>();
        }

        [Key]
        [StringLength(10)]
        public string Account_ID { get; set; }

        [StringLength(30)]
        public string Account_name { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(10)]
        public string User_ID { get; set; }

        public byte? Role_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Account_Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Created { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account_role> Account_role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }
    }
}
