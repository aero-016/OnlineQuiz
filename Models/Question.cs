using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Models
{
    public class Question
    {
        public Question()
        {

        }
        public int QuestionId{ get; set; }
        public String QuestionName { get; set; }
        public int Qmarks { get; set; }
        [Required]
        public int PaperId { get; set; }
        public Paper Paper { get; set; }
        public ICollection<Option> Options { get; set; }
        public Question(ICollection<Option> options)
        {
            options = Options;
        }
        public ICollection<AttendeeQuestion> AttendeeQuestions { get; set; }

    }

}
