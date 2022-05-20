using System;

namespace ChemWebsite.Data.Dto
{
    public class UserAllowedIPDto
    {
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
    }
}
