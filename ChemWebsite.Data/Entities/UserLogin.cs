using Microsoft.AspNetCore.Identity;
using System;

namespace ChemWebsite.Data
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
