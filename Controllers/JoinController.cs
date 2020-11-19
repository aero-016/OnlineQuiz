using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Quiz.Models;

namespace Online_Quiz.Controllers
{
    [Authorize]
    public class JoinController : Controller
    {
        private readonly IPaperRepository _paperRepository;

        public JoinController(IPaperRepository paperRepository)
        {
            _paperRepository = paperRepository;
        }
        [HttpGet]
        public IActionResult Join()
        {
            Paper paper = new Paper();
            paper.Owner = User.Identity.Name;
            return View(paper);
        }
        [HttpPost]
        public IActionResult Join(Paper paper)
        {
            if (_paperRepository.isSubmitted(paper.Owner, paper.PaperCode))
            {
                ViewData["Submitted"] = "Submitted";
                return View();
            }    
            var exist = _paperRepository.GetAllPapers().First(m => m.PaperCode == paper.PaperCode);
            if (exist == null)
            {
                ViewData["NoQuiz"] = "NoQuiz";
                return View();
            }
            foreach(var question in exist.Questions)
            {
                foreach(var option in question.Options)
                {
                    option.Correct = false;
                }
            }
            if (exist.StartDate < DateTime.Now && exist.EndDate > DateTime.Now)
            {
                ViewData["exist"] = exist;
                //return Json(exist);
               return View(exist);
            }
            else
            {
                // return Json(exist.EndDate);
                ViewData["Finished"] = "Finished";
                return View();
            }
        }



        [HttpPost]
        public IActionResult JoinQuiz(Paper paper)
        {
            //return Json(paper);
            AnswerSheet answerSheet = _paperRepository.AddAnswerSheet(paper,User.Identity.Name);
            ViewModel mymodel = new ViewModel();
            mymodel.Papers = _paperRepository.GetAllPapers().First(m => m.PaperId == answerSheet.Paper.PaperId);
            mymodel.AnswerSheets =answerSheet;
            mymodel.AnswerSheet_Questions = _paperRepository.GetAllAnswerSheet_Questions().Where(m=>m.AnswerSheetId==answerSheet.AnswerSheetId).ToList();
            return View(mymodel);
            //return View(answerSheet);
        }
    }
}
