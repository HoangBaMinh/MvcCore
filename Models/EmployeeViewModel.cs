using mvcCore.Models.Domain;

namespace mvcCore.Models
{
    public class EmployeeViewModel
    {
        public IQueryable<Employee> Employees { get; set; }
        public string NameSortOrder { get; set; }
        public string EmailSortOrder { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public string Term { get; set; }

        public string OrderBy { get; set; }
    }
}
    