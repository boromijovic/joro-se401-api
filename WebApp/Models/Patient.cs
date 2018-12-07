using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LBO { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
