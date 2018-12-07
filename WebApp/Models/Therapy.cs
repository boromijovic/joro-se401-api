using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Therapy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TherapyId { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string Comments { get; set; }
        public DateTime DataOfExam { get; set; }
    }
}
