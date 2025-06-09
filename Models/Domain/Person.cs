using System.ComponentModel.DataAnnotations;

namespace mvcCore.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}
