using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrescriptionId { get; set; }
        public string DescriptionOfUse { get; set; }
        public string DrugName { get; set; }
        public string DoctorName { get; set; }
        public string Note { get; set; }
        public DateTime DataOfExpiry { get; set; }
    }
}
