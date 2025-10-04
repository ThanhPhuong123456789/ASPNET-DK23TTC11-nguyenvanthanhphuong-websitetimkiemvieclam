namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Job")]
    public partial class Job
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string RequestCandidate { get; set; }

        public string Interest { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public bool? Salary { get; set; }

        public decimal? SalaryMin { get; set; }

        public decimal? SalaryMax { get; set; }

        public int? Quantity { get; set; }

        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Details { get; set; }

        public DateTime? Deadline { get; set; }

        [StringLength(250)]
        public string Rank { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string Experience { get; set; }

        [StringLength(250)]
        public string WorkLocation { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        public int? RecumentID { get; set; }

        public int? CarrerID { get; set; }

        public long? UserID { get; set; }

        public Recument recument { get; set; }

        [Column(TypeName = "ntext")]
        public string ApplicationMethod { get; set; }
    }
}
