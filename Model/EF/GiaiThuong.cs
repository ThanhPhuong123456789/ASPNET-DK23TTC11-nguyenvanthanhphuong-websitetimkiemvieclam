namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaiThuong")]
    public partial class GiaiThuong
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string TenGiaiThuong { get; set; }

        [StringLength(250)]
        public string ToChuc { get; set; }

        public int? ThangNhan { get; set; }

        public int? NamNhan { get; set; }

        [StringLength(250)]
        public string Img { get; set; }

        [StringLength(250)]
        public string LienKet { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }
    }
}
