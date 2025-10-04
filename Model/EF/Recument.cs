namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recument")]
    public partial class Recument
    {
        public long ID { get; set; }

        public int JobID { get; set; }

        public long? UserID { get; set; }

        [Column(TypeName = "ntext")]
        public string LetterInfo { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Status { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public long? ProfileID { get; set; }
    }
}
