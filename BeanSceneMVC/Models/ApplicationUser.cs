using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanSceneMVC.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [NotMapped]
        public string FullName { get => FirstName + " " + LastName; }

        [NotMapped]
        public UserManager<ApplicationUser>? UserManager { get; set; } = null;
        [NotMapped]
        public bool IsUser { get => IsInRole("User").Result; }

        [NotMapped]
        public bool IsStaff { get => IsInRole("Staff").Result; }

        [NotMapped]
        public bool IsManager { get => IsInRole("Manager").Result; }

        public async Task<bool> IsInRole(string roleName)
        {
            if (UserManager == null) return false;
            return await UserManager.IsInRoleAsync(this, roleName);
        }

    }
}
