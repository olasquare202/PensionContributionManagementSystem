using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.Entity
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Member> Members { get; set; }

    }
}
