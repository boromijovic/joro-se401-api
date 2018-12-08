using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Commands
{
    public class RefferalCommand
    {
        public int PatientId { get; set; }
        public string DescriptionOfProblem { get; set; }
        public string DoctorName { get; set; }
        public string RefferingMD { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
