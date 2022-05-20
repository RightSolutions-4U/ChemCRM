using AutoMapper;

namespace ChemWebsite.API.Helpers.Mapping
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionProfile());
                mc.AddProfile(new PageProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new PageActionProfile());
                mc.AddProfile(new NLogProfile());
                mc.AddProfile(new EmailTemplateProfile());
                mc.AddProfile(new EmailProfile());
                mc.AddProfile(new IndustryProfile());
                mc.AddProfile(new ChemicalProfile());
                mc.AddProfile(new CountryProfile());
                mc.AddProfile(new CustomerProfile());
                mc.AddProfile(new TestimonialsProfile());
                mc.AddProfile(new NewsletterSubscriberProfile());
                mc.AddProfile(new CityProfile());
                mc.AddProfile(new SupplierProfile());
                mc.AddProfile(new InquiryProfile());
                mc.AddProfile(new ArticleProfile());
                mc.AddProfile(new ContactUsMapping());
                mc.AddProfile(new ChemicalTypeProfile());
                mc.AddProfile(new InquiryNoteProfile());
                mc.AddProfile(new InquiryActivityProfile());
                mc.AddProfile(new InquiryAttachmentProfile());

                mc.AddProfile(new DocumentAuditTrailProfile());
                mc.AddProfile(new DocumentCategoryProfile());
                mc.AddProfile(new DocumentPermissionProfile());
                mc.AddProfile(new DocumentProfile());
                mc.AddProfile(new ReminderProfile());
                mc.AddProfile(new PurchaseOrderProfile());
                mc.AddProfile(new DeliveryMethodProfile());
                mc.AddProfile(new PaymentTermProfile());

                mc.AddProfile(new InquiryStatusProfile());
                mc.AddProfile(new InquirySourceProfile());
                mc.AddProfile(new PackagingTypeProfile());

                mc.AddProfile(new SalesOrderProfile());
                mc.AddProfile(new CompanyProfileProfile());
                mc.AddProfile(new ExpenseProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
