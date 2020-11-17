using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Models;
using NewsProject.ViewModels;

namespace NewsProject.Controllers
{
    public class HomeController : Controller
    {
        private INewsRepository _newsRepository;

        public HomeController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public ViewResult Index()
        {
            var model = _newsRepository.GetAllNews();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(NewsCreateDTO news)
        {
            if (ModelState.IsValid)
            {
                News addedNews = _newsRepository.Create(news);
                return RedirectToAction("details", new { id = addedNews.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public ViewResult Update(int id)
        {
            NewsUpdateDTO news = _newsRepository.GetUpdate(id);
            return View(news);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(NewsUpdateDTO news)
        {
            if (ModelState.IsValid)
            {
                News updatedNews = _newsRepository.Update(news);
                return RedirectToAction("details", new { id = updatedNews.Id });
            }
            else
            {
                return View();
            }
        }

        public ViewResult Details(int id)
        {
            NewsDetailsDTO newsDetailsDTO = new NewsDetailsDTO()
            {
                PageTitle = "News details",
                News = _newsRepository.GetNews(id)
            };

            return View(newsDetailsDTO);
        }

        public IActionResult Delete(int id)
        {
            _newsRepository.Delete(id);

            return RedirectToAction("index", "home");
        }

    }
}

