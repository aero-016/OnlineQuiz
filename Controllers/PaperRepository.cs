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
        public Boolean isSubmitted(String user,String paperCode)
        {
            var paper = context.Papers.First(m => m.PaperCode == paperCode);
            var paperid = paper.PaperId;
            var ansersheet = context.AnswerSheets.Where(m => m.PaperId == paperid).FirstOrDefault(n=>n.User==user);
            if (ansersheet != null) {
                return true;
            }
            else
            {
                return false;
            }

        }
        public AnswerSheet AddAnswerSheet(Paper paper,String user)
        {
            var totalMarks = 0;
            var answerSheet = new AnswerSheet();
            answerSheet.Paper = GetAllPapers().First(paper1 => paper1.PaperCode == paper.PaperCode);
            answerSheet.User = user;
            System.Diagnostics.Debug.Print(answerSheet.User+"----------------");
            context.AnswerSheets.Add(answerSheet);
            context.SaveChanges();
            for (var i=0;i<paper.Questions.Count();i++)
            {
                var answerSheet_Question = new AnswerSheet_Question();
                answerSheet_Question.Question = answerSheet.Paper.Questions[i];
                for (var j= 0;j < paper.Questions[i].Options.Count();j++)
                {

                    if (paper.Questions[i].Options[j].Correct==true) {
                        for(var k=0;k< paper.Questions[i].Options.Count(); k++)
                        {
                            if( answerSheet.Paper.Questions[i].Options[k].Correct==true && k == j)
                            {
                                totalMarks++;
                            }
                        }
                        answerSheet_Question.selectedoption = answerSheet.Paper.Questions[i].Options[j];
                     }
                }
               
                answerSheet_Question.AnswerSheet = answerSheet;
                context.AnswerSheet_Questions.Add(answerSheet_Question);
                context.SaveChanges();
            }

            answerSheet.ObtainedMarks = totalMarks;
            answerSheet.SubmitTime = DateTime.Now;
            context.SaveChanges();
            return answerSheet;
        }

        public Paper GetPaper(int id)
        {
            return context.Papers.Find(id);
        }
       public List<Paper> GetAllPapers()
        {
            return context.Papers.Include(papers => papers.Questions).ThenInclude(questions => questions.Options).ToList();

        }
        public List<AnswerSheet_Question> GetAllAnswerSheet_Questions()
        {
            return context.AnswerSheet_Questions.ToList();
        }
        IEnumerable<Question> IPaperRepository.GetAllQuestions()
        {
            return context.Questions;
        }
        IEnumerable<Option> IPaperRepository.GetAllOptions()
        {
            return context.Options;

        }
       
    }
}
