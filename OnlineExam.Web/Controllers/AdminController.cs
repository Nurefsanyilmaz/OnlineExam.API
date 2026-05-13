using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OnlineExam.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult ExamList()
        {
            return View();
        }

        public IActionResult TakeExam(int id)
        {
            ViewBag.ExamId = id;
            return View();
        }

        public IActionResult EditExam(int id)
        {
            return View();
        }

        public IActionResult CreateExam()
        {
            return View();
        }

        
        public IActionResult QuestionList(int id)
        {
            ViewBag.ExamId = id; 
            return View();
        }

        public IActionResult ExamResults()
        {
            return View();
        }

        public IActionResult UserList()
        {
            return View();
        }
    }
}