using ChemWebsite.Data;
using ChemWebsite.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChemWebsite.Domain
{
    public class ChemWebsiteDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ChemWebsiteDbContext(DbContextOptions options) : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Data.Action> Actions { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<NLog> NLog { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }
        public DbSet<UserAllowedIP> UserAllowedIPs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSMTPSetting> EmailSMTPSettings { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<ChemicalDetail> ChemicalDetails { get; set; }
        public DbSet<ChemicalSupplier> ChemicalSuppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<InquiryChemical> InquiryChemicals { get; set; }
        public DbSet<SupplierVerification> SupplierVerifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChemicalCategory> ChemicalCategories { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<ChemicalSynonym> ChemicalSynonyms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ChemicalCustomer> ChemicalCustomers { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<ChemicalIndustry> ChemicalIndustries { get; set; }
        public DbSet<ChemicalStatistics> ChemicalStatistics { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<SupplierEmail> SupplierEmails { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ChemicalType> ChemicalTypes { get; set; }
        public DbSet<InquiryStatus> InquiryStatuses { get; set; }
        public DbSet<InquiryNote> InquiryNotes { get; set; }
        public DbSet<InquiryActivity> InquiryActivities { get; set; }
        public DbSet<InquiryAttachment> InquiryAttachments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<DocumentRolePermission> DocumentRolePermissions { get; set; }
        public DbSet<DocumentUserPermission> DocumentUserPermissions { get; set; }
        public DbSet<DocumentAuditTrail> DocumentAuditTrails { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderNotification> ReminderNotifications { get; set; }
        public DbSet<ReminderUser> ReminderUsers { get; set; }
        public DbSet<ReminderScheduler> ReminderSchedulers { get; set; }
        public DbSet<HalfYearlyReminder> HalfYearlyReminders { get; set; }
        public DbSet<QuarterlyReminder> QuarterlyReminders { get; set; }
        public DbSet<DailyReminder> DailyReminders { get; set; }
        public DbSet<SendEmail> SendEmails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PackagingType> PackagingTypes { get; set; }
        public DbSet<PurchaseOrderDeliverySchedule> PurchaseOrderDeliverySchedules { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesPurchaseOrderItem> SalesPurchaseOrderItems { get; set; }
        public DbSet<SalesOrderAttachment> SalesOrderAttachments { get; set; }
        public DbSet<PurchaseOrderAttachment> PurchaseOrderAttachments { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<ReminderUser>(b =>
            {
                b.HasKey(e => new { e.ReminderId, e.UserId });
                b.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(ur => ur.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Data.Action>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PageAction>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Page>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Inquiry>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.AssignUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.AssignTo)
                    .OnDelete(DeleteBehavior.NoAction);

            });
            builder.Entity<InquiryActivity>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.AssignUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.AssignTo)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<UserAllowedIP>().HasKey(c => new { c.UserId, c.IPAddress });

            builder.Entity<EmailSMTPSetting>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ChemicalSupplier>()
                .HasKey(c => new
                {
                    c.ChemicalId,
                    c.SupplierId
                });
            builder.Entity<ChemicalIndustry>()
                .HasKey(c => new
                {
                    c.ChemicalId,
                    c.IndustryId
                });
            builder.Entity<ChemicalCustomer>()
                .HasKey(c => new
                {
                    c.ChemicalId,
                    c.CustomerId
                });

            builder.Entity<InquiryChemical>()
                .HasKey(c => new
                {
                    c.ChemicalId,
                    c.InquiryId
                });

            builder.Entity<ChemicalSynonym>()
                .HasIndex(c => new
                {
                    c.Name,
                    c.CasNumber
                });

            builder.Entity<Chemical>()
                .HasIndex(chem => new { chem.CasNumber, chem.Name });

            builder.Entity<Supplier>()
                .HasMany(c => c.SupplierAddresses);

            builder.Entity<Supplier>()
                .HasMany(c => c.SupplierEmails);

            builder.Entity<Category>()
                .HasMany(c => c.ChemicalCategories)
                .WithOne(cs => cs.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.Entity<Category>()
                .HasMany(c => c.ChemicalSubCategories)
                .WithOne(cs => cs.SubCategory)
                .HasForeignKey(c => c.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Chemical>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ChemicalDetail>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ChemicalSupplier>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<City>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Country>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Customer>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Industry>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Supplier>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<SupplierVerification>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Testimonials>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Category>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<EmailTemplate>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Document>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<DocumentAuditTrail>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<DocumentCategory>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<DocumentRolePermission>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<DocumentUserPermission>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Reminder>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<SalesPurchaseOrderItem>(b =>
            {
                b.HasKey(e => new { e.SalesOrderId, e.PurchaseOrderId });
            });

            builder.Entity<SalesOrder>(b =>
            {
                b.HasMany(c => c.SalesPurchaseOrderItems)
                    .WithOne(cs => cs.SalesOrder)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PurchaseOrder>(b =>
            {
                b.HasMany(c => c.SalesPurchaseOrderItems)
                    .WithOne(cs => cs.PurchaseOrder)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PurchaseOrderAttachment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<SalesOrderAttachment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Expense>(b =>
            {
                b.HasOne(e => e.ExpenseBy)
                    .WithMany()
                    .HasForeignKey(rc => rc.ExpenseById)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<ExpenseCategory>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.DefalutMappingValue();
            builder.DefalutDeleteValueFilter();
        }
    }
}
