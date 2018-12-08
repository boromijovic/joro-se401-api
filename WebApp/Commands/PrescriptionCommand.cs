using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Commands
{
    public class PrescriptionCommand
    {
        public string DescriptionOfUse { get; set; }
        public string DrugName { get; set; }
        public string DoctorName { get; set; }
        public string Note { get; set; }
        public DateTime DataOfExpiry { get; set; }
    }
}
