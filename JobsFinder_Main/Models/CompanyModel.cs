using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsFinder_Main.Models
{
    public class CompanyModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên công ty không được trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trang chủ công ty không được trống")]
        public string LinkPage { get; set; }

        [Required(ErrorMessage = "Mô tả công ty không được trống")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng tải lên ảnh đại diện cho công ty")]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Vui lòng tải lên ảnh bìa cho công ty")]
        public string Background { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng nhân lực trong công ty")]
        public int? Employees { get; set; }

        [Required(ErrorMessage = "Vị trí công ty không được trống")]
        public string Location { get; set; }
    }
}