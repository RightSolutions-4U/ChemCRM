using Microsoft.Extensions.Configuration;

namespace ChemWebsite.Helper
{
    public class PathHelper
    {
        public IConfiguration _configuration;

        public PathHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string UserProfilePath
        {
            get
            {
                return _configuration["UserProfilePath"];
            }
        }

        public string ChemicalImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ChemicalImages"];
            }
        }

        public string NoImageFound
        {
            get
            {
                return _configuration["ImagePathSettings:NoImageFound"];
            }
        }

        public string CompanyLogo
        {
            get
            {
                return _configuration["ImagePathSettings:CompanyLogo"];
            }
        }

        public string SupplierImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:SupplierImages"];
            }
        }

        public string ArticleBannerImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ArticleBannerImagePath"];
            }
        }

        public string CustomerImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:CustomerImages"];
            }
        }

        public string TestimonialsImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:TestimonialsImagePath"];
            }
        }

        public string Attachments
        {
            get
            {
                return _configuration["ImagePathSettings:Attachments"];
            }
        }

        public string SiteMapPath
        {
            get
            {
                return _configuration["SiteMapPath"];
            }
        }

        public string DocumentPath
        {
            get
            {
                return _configuration["DocumentPath"];
            }
        }
        public string AesEncryptionKey
        {
            get
            {
                return _configuration["AesEncryptionKey"];
            }
        }

        public string ReminderFromEmail
        {
            get
            {
                return _configuration["ReminderFromEmail"];
            }
        }
    }
}
