using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsFinder_Main.Models
{
    public class ProfileModel
    {
        [Key]
        public long UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfileID { get; set; }

        [StringLength(250)]
        public string HoVaTen { get; set; }

        [StringLength(250)]
        public string AnhCaNhan { get; set; }

        [StringLength(50)]
        public string GioiTinh { get; set; }

        public int? NgaySinh { get; set; }

        public int? ThangSinh { get; set; }

        public int? NamSinh { get; set; }

        [StringLength(250)]
        public string DiaChiHienTai { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        [Column(TypeName = "ntext")]
        public string GioiThieu { get; set; }
    }
}