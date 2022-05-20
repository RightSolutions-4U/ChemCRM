﻿using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateInquiryNoteCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        public Guid InquiryId { get; set; }
        public string Note { get; set; }
    }
}
