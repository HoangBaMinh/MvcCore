using System;
using Microsoft.AspNetCore.Mvc;
using mvcCore.Models.Domain;

namespace mvcCore.Controllers
{
    public class PersonController : Controller
    {

        private readonly DatabaseContext _context;
        public PersonController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.greeting = "Hello from PersonController!";
            ViewData["greeting2"] = "Welcome to the Person page!";
            TempData["greeting3"] = "This is a temporary greeting message.";
            return View();
        }


        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Add(person);
                _context.SaveChanges();
                TempData["msg"] = "Đã thêm người thành công!";
                return RedirectToAction("AddPerson");
            }
            catch(Exception ex)
            {
                TempData["msg"] = "Không thể thêm người này!";
                return View();
            }
        }

        public IActionResult DisplayPersons()
        {
            var persons = _context.Person.ToList();
            return View(persons);
        }


        [HttpGet]
        public IActionResult EditPerson(int id)
        {
            var person = _context.Person.Find(id);
            if (person == null)
            {
                TempData["msg"] = "Không tìm thấy người để sửa!";
                return RedirectToAction("DisplayPersons");
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            try
            {
                _context.Update(person);
                _context.SaveChanges();
                TempData["msg"] = "Đã cập nhật người thành công!";
                return RedirectToAction("DisplayPersons");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Không thể cập nhật người này!";
                return View(person);
            }
        }

        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _context.Person.Find(id);
                if (person != null)
                {
                    _context.Person.Remove(person);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                TempData["msg"] = "Không thể xóa người này!";
                return RedirectToAction("DisplayPersons");
            }
            return RedirectToAction("DisplayPersons");
        }
    }
}
