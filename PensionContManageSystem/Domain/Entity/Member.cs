using PensionContManageSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PensionContManageSystem.Domain.Entity
{
    public class Member
    {//(Name, Date of Birth, Email, Phone, Age restrictions: 18-70
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        
        public MemberStatus MemberStatus { get; set; } = MemberStatus.NotActive;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public ICollection<Contribution> Contributions { get; set; }
        public int EmployerId { get; set; }
        public Employer Employers { get; set; }
        //Member to Employer = many to 1
        //Member to Contribution = 1 to many

        //public void SoftDelete() => IsDeleted = true;
    }
}
