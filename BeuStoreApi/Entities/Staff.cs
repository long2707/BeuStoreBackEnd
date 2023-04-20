using Microsoft.AspNetCore.Identity;

namespace BeuStoreApi.Entities
{
    public class Staff: IdentityUser
    {
            public string? profileImage { get; set; }
    }
}
