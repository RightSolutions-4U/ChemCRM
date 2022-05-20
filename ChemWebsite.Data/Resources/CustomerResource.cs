﻿using ChemWebsite.Helper;
using System;

namespace ChemWebsite.Data
{
    public class CustomerResource : ResourceParameters
    {
        public CustomerResource() : base("CustomerName")
        {
        }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public Guid ChemicalId { get; set; }
    }
}
