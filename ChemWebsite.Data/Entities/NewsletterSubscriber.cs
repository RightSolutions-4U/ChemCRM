using System;
using System.ComponentModel.DataAnnotations;
using ChemWebsite.Helper;

namespace ChemWebsite.Data
{
    public class NewsletterSubscriber
    {
        private DateTime _createdDate;

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(50)]
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate
        {
            get => _createdDate.UTCDateTime();
            set => _createdDate = value.UTCDateTime();
        }
    }
}
