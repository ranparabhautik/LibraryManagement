using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Model.Identity;

public class ApplicationUser:IdentityUser<Guid>
{
    public string? FullName { get; set; } 
    public string? City { get; set; }
    public string? Region { get; set; }
    public Member? Member { get; set; }
}
