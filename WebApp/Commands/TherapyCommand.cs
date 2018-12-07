using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Commands
{
    public class TherapyCommand
    {
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string Comments { get; set; }
        public DateTime DataOfExam { get; set; }
    }
}
