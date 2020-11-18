using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Models
{
    public class AttendeeQuestion
    {
        public int Id { get; set; }
       
        //public int? OptionId { get; set; }
        
        //public Option Option { get; set; }
        
        //public int? QuestionId { get; set; }
        
        //public Question Question { get; set; }
       
        // public List<Question> Questions { get; set; }
        public int?  PaperId { get; set; } 
        public Paper Paper { get; set; }
    }
}
