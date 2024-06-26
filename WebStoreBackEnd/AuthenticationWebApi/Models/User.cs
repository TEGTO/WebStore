using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWebApi.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
    }
}
