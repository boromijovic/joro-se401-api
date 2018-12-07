using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Refferal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefferalId { get; set; }
        public int PatientId { get; set; }
        public string DescriptionOfProblem { get; set; }
        public string DoctorName { get; set; }
        public string RefferingMD { get; set; }
        public string Note { get; set; }
        public DateTime Data { get; set; }
    }
}
