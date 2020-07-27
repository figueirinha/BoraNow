using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recodme.RD.BoraNow.BusinessLayer.BusinessObjects.Quizzes;
using Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.Quizzes;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuizAnswerBusinessObject _bo = new QuizAnswerBusinessObject();
        private readonly QuizQuestionBusinessObject _qqbo = new QuizQuestionBusinessObject();
        private readonly QuizBusinessObject _qbo = new QuizBusinessObject();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Administration()
        {
            return View();
        }

        [Authorize(Roles = "Visitor")]
        public IActionResult Visitor()
        {
            return View();
        }

        [Authorize(Roles = "Company")]
        public IActionResult Company()
        {
            return View();
        }

        [Authorize(Roles = "Visitor, Admin")]
        public async Task<IActionResult> QuizStart(/*IEnumerable<QuizAnswerViewModel> vm*/)
        {
            var listOperation = await _bo.ListAsync();
            var qqListOperation = await _qqbo.ListAsync();

            var list = new List<QuizAnswerViewModel>();
            foreach (var item in listOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    list.Add(QuizAnswerViewModel.Parse(item));
                }

            }

            var qqList = new List<QuizQuestionViewModel>();
            foreach (var item in qqListOperation.Result)
            {
                if (!item.IsDeleted)
                {
                    qqList.Add(QuizQuestionViewModel.Parse(item));
                }
            }

            ViewBag.QuizQuestions = qqList;
            return View(list);

        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
