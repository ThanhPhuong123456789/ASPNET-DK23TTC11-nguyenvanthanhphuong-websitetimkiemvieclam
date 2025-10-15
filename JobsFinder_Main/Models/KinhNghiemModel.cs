using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsFinder_Main.Models
{
    public class KinhNghiemModel
    {
        public long UserID { get; set; }

        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        public string CongTy { get; set; }

        [Column(TypeName = "ntext")]
        public string ChucVu { get; set; }

        public int? ThangBatDau { get; set; }

        public int? NamBatDau { get; set; }

        public int? ThangKetThuc { get; set; }

        public int? NamKetThuc { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaChiTiet { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [StringLength(250)]
        public string LienKet { get; set; }
    }
}