using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateTestimonialsCommand : IRequest<ServiceResponse<TestimonialsDto>>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public bool IsImageUpload { get; set; }
        public string ImageSrc { get; set; }
    }
}
