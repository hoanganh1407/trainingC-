namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_user()
        {
            tbl_detail_user_japan = new HashSet<tbl_detail_user_japan>();
        }

        [Key]
        public int user_id { get; set; }

        public int group_id { get; set; }

        [Required]
        [StringLength(15)]
        public string login_name { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [Required]
        [StringLength(255)]
        public string full_name { get; set; }

        [Required]
        [StringLength(255)]
        public string full_name_kana { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(15)]
        public string tel { get; set; }

        [Column(TypeName = "date")]
        public DateTime birthday { get; set; }

        public virtual mst_group mst_group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_detail_user_japan> tbl_detail_user_japan { get; set; }
    }
}
