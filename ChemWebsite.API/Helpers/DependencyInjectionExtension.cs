using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ChemWebsite.Api.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<IUserAllowedIPRepository, UserAllowedIPRepository>();
            services.AddScoped<INLogRepository, NLogRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IChemicalCategoryRepository, ChemicalCategoryRepository>();
            services.AddScoped<IChemicalRepository, ChemicalRepository>();
            services.AddScoped<IChemicalStatisticsRepository, ChemicalStatisticsRepository>();
            services.AddScoped<IChemicalSynonymRepository, ChemicalSynonymRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IChemicalCustomerRepository, ChemicalCustomerRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
            services.AddScoped<IIndustryChemicalRepository, IndustryChemicalRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<INewsletterSubscriberRepository, NewsletterSubscriberRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierVerificationRepository, SupplierVerificationRepository>();
            services.AddScoped<ISupplierChemicalRepository, SupplierChemicalRepository>();
            services.AddScoped<ITestimonialsRepository, TestimonialsRepository>();
            services.AddScoped<IInquiryChemicalRepository, InquiryChemicalRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IChemicalTypeRepository, ChemicalTypeRepository>();
            services.AddScoped<IInquiryStatusRepository, InquiryStatusRepository>();
            services.AddScoped<IInquiryNoteRepository, InquiryNoteRepository>();
            services.AddScoped<IInquiryAttachmentRepository, InquiryAttachmentRepository>();
            services.AddScoped<IInquiryActivityRepository, InquiryActivityRepository>();
            services.AddScoped<IInquirySourceRepository, InquirySourceRepository>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentAuditTrailRepository, DocumentAuditTrailRepository>();
            services.AddScoped<IDocumentCategoryRepository, DocumentCategoryRepository>();
            services.AddScoped<IDocumentRolePermissionRepository, DocumentRolePermissionRepository>();
            services.AddScoped<IDocumentUserPermissionRepository, DocumentUserPermissionRepository>();
            services.AddScoped<IReminderNotificationRepository, ReminderNotificationRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderUserRepository, ReminderUserRepository>();
            services.AddScoped<IReminderFrequencyRepository, ReminderFrequencyRepository>();
            services.AddScoped<IReminderSchedulerRepository, ReminderSchedulerRepository>();
            services.AddScoped<IDailyReminderRepository, DailyReminderRepository>();
            services.AddScoped<IQuarterlyReminderRepository, QuarterlyReminderRepository>();
            services.AddScoped<IHalfYearlyReminderRepository, HalfYearlyReminderRepository>();
            services.AddScoped<ISendEmailRepository, SendEmailRepository>();

            // PO
            services.AddScoped<IPackagingTypeRepository, PackagingTypeRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderDeliveryScheduleRepository, PurchaseOrderDeliveryScheduleRepository>();
            services.AddScoped<IPurchaseOrderAttachmentRepository, PurchaseOrderAttachmentRepository>();
            //SO
            services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();
            services.AddScoped<IPaymentTermRepository, PaymentTermRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<ISalesPurchaseOrderItemRepository, SalesPurchaseOrderItemRepository>();
            services.AddScoped<ISalesOrderAttachmentRepository, SalesOrderAttachmentRepository>();

            services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();

            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
        }
    }
}
