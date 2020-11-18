using Online_Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Controllers
{
   public interface IPaperRepository
    {
        
        Paper AddPaper(Paper paper);
        Paper DeletePaper(int id);
        Question AddQuestion(Question question);
        
        Option AddOption(Option option);
        AttendeeQuestion AddAttendeeQuestion(AttendeeQuestion attendeeQuestion);
        Paper GetPaper(int id);
        List<Paper> GetAllPapers();
        IEnumerable<Option> GetAllOptions();
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<AttendeeQuestion> GetAllAttendeeQuestions();

    }
}
