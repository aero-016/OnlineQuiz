using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Models
{
    public class AttendeePaper
    {
        public int AttendeePaperId { get; set; }
        public String AttendeeName { get; set; }
        public int PaperId { get; set; }
        public Paper Paper { get; set; }
        public ICollection<AttendeeQuestion> AttendeeQuestions {get;set;}
    }
}
