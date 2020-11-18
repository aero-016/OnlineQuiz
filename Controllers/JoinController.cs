using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Quiz.Models;

namespace Online_Quiz.Controllers
{
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
            return View();
        }
        [HttpPost]
        public IActionResult Join(Paper paper)
        {
            var exist = _paperRepository.GetAllPapers().First(m => m.PaperCode == paper.PaperCode);
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
                return View();
            }
        }



        [HttpPost]
        public IActionResult JoinQuiz(Paper paper)
        {
            //return Json(paper);
            var answerSheet = _paperRepository.AddAnswerSheet(paper,User.Identity.Name);

            return View();
        }
    }
}
