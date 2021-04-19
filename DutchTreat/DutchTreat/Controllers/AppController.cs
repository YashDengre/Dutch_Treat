using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        //private readonly DutchContext _context;
        private readonly IDutchRepository _repository;

        //public AppController(IMailService mailService, DutchContext context)
        //{
        //    _mailService = mailService;
        //    _context = context;

        //}
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;

        }
        public IActionResult Index()
        {
            //throw new InvalidProgramException("Bad things");
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            //throw new InvalidProgramException("Bad");  //used this to see the production environment error with only razor pages
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Contact Us";
                _mailService.SendMessage("yash.dengre68@gmail.com", model.Subject, $"From: {model.Email} Message : {model.Message}");
                ViewBag.UserMessage = "Mail Sent Successfully!";
                ModelState.Clear();
            }
            else
            {
                ViewBag.UserMessage = "Something is wrong!";
            }
            return View();

        }
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            //var result = _context.Products.ToList();
            //var result = _context.Products.OrderBy(p => p.Category).ToList();
            //var result = from p in _context.Products orderby p.Category select p;
            var result = _repository.GetProducts();
            return View(result.ToList());
        }
    }
}
