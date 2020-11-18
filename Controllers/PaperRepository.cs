using Microsoft.EntityFrameworkCore;
using Online_Quiz.Data;
using Online_Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Controllers
{
    public class PaperRepository : IPaperRepository
    {
        private readonly ApplicationDbContext context;

        public PaperRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Paper AddPaper(Paper paper)
        {
            context.Papers.Add(paper);
            context.SaveChanges();
            return paper;

        }
        public Paper DeletePaper(int id)
        {
            var paper = context.Papers.Find(id);
            context.Papers.Remove(paper);
            context.SaveChanges();
            return paper;
        }
        public Question AddQuestion(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
            return question;

        }
        public Option AddOption(Option option)
        {
            context.Options.Add(option);
            context.SaveChanges();
            return option;

        }
        public AttendeeQuestion AddAttendeeQuestion(AttendeeQuestion attendeeQuestion)
        {
            context.AttendeeQuestions.Add(attendeeQuestion);
            context.SaveChanges();
            return attendeeQuestion;

        }

        public Paper GetPaper(int id)
        {
            return context.Papers.Find(id);
        }
        List<Paper> IPaperRepository.GetAllPapers()
        {
            return context.Papers.Include(papers => papers.Questions).ThenInclude(questions=>questions.Options).ToList();
        }
        IEnumerable<Question> IPaperRepository.GetAllQuestions()
        {
            return context.Questions;
        }
        IEnumerable<Option> IPaperRepository.GetAllOptions()
        {
            return context.Options;

        }
        IEnumerable<AttendeeQuestion> IPaperRepository.GetAllAttendeeQuestions()
        {
            return context.AttendeeQuestions;
        }
    }
}
