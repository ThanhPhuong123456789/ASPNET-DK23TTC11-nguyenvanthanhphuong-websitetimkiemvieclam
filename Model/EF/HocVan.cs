namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocVan")]
    public partial class HocVan
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string Truong { get; set; }

        [StringLength(250)]
        public string ChuyenNganh { get; set; }

        public int? ThangBatDau { get; set; }

        public int? NamBatDau { get; set; }

        public int? ThangKetThuc { get; set; }

        public int? NamKetThuc { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaChiTiet { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }
    }
}
