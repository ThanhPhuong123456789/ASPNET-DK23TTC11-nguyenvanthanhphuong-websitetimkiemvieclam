using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsFinder_Main.Models
{
    public class ChungChiModel
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

        [StringLength(250)]
        public string Img { get; set; }

        [StringLength(250)]
        public string LienKet { get; set; }
    }
}