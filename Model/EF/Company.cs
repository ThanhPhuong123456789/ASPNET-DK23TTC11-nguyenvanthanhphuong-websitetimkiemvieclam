namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [StringLength(250)]
        public string LinkPage { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Avatar { get; set; }

        [StringLength(250)]
        public string Background { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public int? Employees { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

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
    }
}
