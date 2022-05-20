﻿using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class EmailOrPhoneExistCheckQuery : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}
