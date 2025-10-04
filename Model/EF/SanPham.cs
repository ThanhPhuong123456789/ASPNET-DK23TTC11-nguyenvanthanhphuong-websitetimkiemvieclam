namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        [StringLength(250)]
        public string TheLoai { get; set; }

        public int? ThangHoanThanh { get; set; }

        public int? NamHoanThanh { get; set; }

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

        [Column(TypeName = "ntext")]
        public string MoTaChiTiet { get; set; }
    }
}
