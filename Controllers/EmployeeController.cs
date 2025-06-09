using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using mvcCore.Models;
using mvcCore.Models.Domain;

namespace mvcCore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult AddEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee{Name = "Minh", Email = "minh12@gmail.com"},
                new Employee{Name = "Trang", Email = "trang12@gmail.com"},
                new Employee{Name = "Nam", Email = "nam12@gmail.com"},
                new Employee{Name = "Hung", Email = "hung12@gmail.com"},
                new Employee{Name = "Ba", Email = "ba12@gmail.com"},
            };
            _context.AddRange(employees);
            try
            {
                _context.SaveChanges();
                return Content("Done");
            }
            catch (Exception ex)
            {
                return Content("Failed: " + ex.Message + " --- " + ex.InnerException?.Message);
            }

        }

        public IActionResult Employees(string term = "", string orderBy="", int currentPage=1)
        {
            
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var empData = new EmployeeViewModel();
            empData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
            empData.EmailSortOrder = orderBy == "email" ? "email_desc" : "email";
            var employees = (from emp in _context.Employees
                             where term=="" || emp.Name.ToLower().StartsWith(term)
                             select new Employee
                             {
                                 Id = emp.Id,
                                 Name = emp.Name,
                                 Email = emp.Email
                             });

            switch(orderBy)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(a => a.Name);
                    break;
                case "email_desc":
                    employees = employees.OrderByDescending(a => a.Email);
                    break;
                case "email":
                    employees = employees.OrderBy(a => a.Email);
                    break;
                default:
                    employees = employees.OrderBy(a => a.Name);
                    break;
            }

            // phan trang 5 ban ghi 1 trnag
            int totalRecords = employees.Count();
            int pageSize = 5;
            int toltalPages = (int)Math.Ceiling(totalRecords/(double)pageSize);
            employees = employees.Skip((currentPage - 1) * pageSize).Take(pageSize);
             

            empData.Employees = employees;
            empData.CurrentPage = currentPage;
            empData.TotalPages = toltalPages;
            empData.PageSize = pageSize;
            empData.Term = term;
            empData.OrderBy = orderBy;
            return View(empData);
        }
    }
}



