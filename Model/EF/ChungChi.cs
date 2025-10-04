namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChungChi")]
    public partial class ChungChi
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string TenChungChi { get; set; }

        [StringLength(250)]
        public string ToChuc { get; set; }

        public int? ThangXacThuc { get; set; }

        public int? NamXacThuc { get; set; }

        public int? ThangHetHan { get; set; }

        public int? NamHetHan { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string Img { get; set; }

        [StringLength(250)]
        public string LienKet { get; set; }
    }
}
