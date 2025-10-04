using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsFinder_Main.Models
{
    public class SanPhamModel
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

        [Column(TypeName = "ntext")]
        public string MoTaChiTiet { get; set; }
    }
}