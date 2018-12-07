using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardId { get; set; }
        public Patient Patient { get; set; }
        public List<Allergen> Allergens { get; set; }
        public List<Therapy> Therapies { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Refferal> Refferals { get; set; }
    }
}
