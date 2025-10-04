namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DuAn")]
    public partial class DuAn
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string TenDuAn { get; set; }

        [StringLength(250)]
        public string TenKhachHang { get; set; }

        public int? SoThanhVien { get; set; }

        [StringLength(250)]
        public string ViTri { get; set; }

        [StringLength(250)]
        public string NhiemVu { get; set; }

        [StringLength(250)]
        public string CongNgheSuDung { get; set; }

        public int? ThangBatDau { get; set; }

        public int? NamBatDau { get; set; }

        public int? ThangKetThuc { get; set; }

        public int? NamKetThuc { get; set; }

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
