using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsFinder_Main.Models
{
    public class JobModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string RequestCandidate { get; set; }
        public string Interest { get; set; }
        public string Image { get; set; }
        public bool? Salary { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public int? Quantity { get; set; }
        public long? CategoryID { get; set; }
        public string Details { get; set; }
        public DateTime? Deadline { get; set; }
        public string Rank { get; set; }
        public string Gender { get; set; }
        public string Experience { get; set; }
        public string WorkLocation { get; set; }
        public int? CompanyID { get; set; }
        public int? CarrerID { get; set; }
        public long? UserID { get; set; }
        public string code { get; set; }
    }
}