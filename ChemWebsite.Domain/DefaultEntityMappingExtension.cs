using ChemWebsite.Data;
using ChemWebsite.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemWebsite.Domain
{
    public static class DefaultEntityMappingExtension
    {
        public static void DefalutMappingValue(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>()
               .Property(b => b.ModifiedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Page>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<PageAction>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Role>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Chemical>()
               .Property(b => b.ModifiedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ChemicalDetail>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ChemicalSupplier>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Country>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<City>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Supplier>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ContactRequest>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Inquiry>()
             .Property(b => b.ModifiedDate)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<InquiryAttachment>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Category>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Article>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Testimonials>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<PurchaseOrder>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SalesOrder>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SalesOrderAttachment>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<PurchaseOrderAttachment>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Expense>()
               .Property(b => b.ModifiedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ExpenseCategory>()
               .Property(b => b.ModifiedDate)
               .HasDefaultValueSql("getdate()");
        }

        public static void DefalutDeleteValueFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Role>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Action>()
              .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Page>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<PageAction>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<EmailTemplate>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<EmailSMTPSetting>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Chemical>()
              .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ChemicalDetail>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ChemicalSupplier>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Country>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<City>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Supplier>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<SupplierAddress>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ContactRequest>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Inquiry>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Category>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Customer>()
               .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Article>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Testimonials>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<InquiryAttachment>()
              .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<InquiryActivity>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<InquiryNote>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Reminder>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<PurchaseOrder>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<SalesOrder>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<SalesOrderAttachment>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<PurchaseOrderAttachment>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Expense>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ExpenseCategory>()
                .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
