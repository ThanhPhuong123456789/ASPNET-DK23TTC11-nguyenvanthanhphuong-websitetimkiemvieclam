using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobsFinder_Main.Models
{
    [Serializable]
    public class RecumentItem
    {
        [Key]
        public int Id { get; set; }
        public int JobID {  get; set; }
        public User user {  get; set; }

        [Column(TypeName = "ntext")]
        public string LetterInfo { get; set; }

        public int? Status { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}