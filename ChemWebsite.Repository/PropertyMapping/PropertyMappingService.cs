using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChemWebsite.Repository
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _loginAuditMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } )},
                { "LoginTime", new PropertyMappingValue(new List<string>() { "LoginTime" } )},
                { "RemoteIP", new PropertyMappingValue(new List<string>() { "RemoteIP" } )},
                { "Status", new PropertyMappingValue(new List<string>() { "Status" } )},
                { "Provider", new PropertyMappingValue(new List<string>() { "Provider" } )}
            };

        private Dictionary<string, PropertyMappingValue> _userMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } )},
                { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                { "FirstName", new PropertyMappingValue(new List<string>() { "FirstName" } )},
                { "LastName", new PropertyMappingValue(new List<string>() { "LastName" } )},
                { "PhoneNumber", new PropertyMappingValue(new List<string>() { "PhoneNumber" } )},
                { "IsActive", new PropertyMappingValue(new List<string>() { "IsActive" } )}
            };

        private Dictionary<string, PropertyMappingValue> _nLogMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "MachineName", new PropertyMappingValue(new List<string>() { "MachineName" } )},
                { "Logged", new PropertyMappingValue(new List<string>() { "Logged" } )},
                { "Level", new PropertyMappingValue(new List<string>() { "Level" } )},
                { "Message", new PropertyMappingValue(new List<string>() { "Message" } )},
                { "Logger", new PropertyMappingValue(new List<string>() { "Logger" } )},
                { "Properties", new PropertyMappingValue(new List<string>() { "Properties" } )},
                { "Callsite", new PropertyMappingValue(new List<string>() { "Callsite" } )},
                { "Exception", new PropertyMappingValue(new List<string>() { "Exception" } )}
            };

        private Dictionary<string, PropertyMappingValue> _supplierPropertyMapping =
              new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
              {
                       { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                       { "SupplierName", new PropertyMappingValue(new List<string>() { "SupplierName" } )},
                       { "ContactPerson", new PropertyMappingValue(new List<string>() { "ContactPerson" } )},
                       { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                       { "MobileNo", new PropertyMappingValue(new List<string>() { "MobileNo" } )},
                       { "PhoneNo", new PropertyMappingValue(new List<string>() { "PhoneNo" } )},
                       { "Website", new PropertyMappingValue(new List<string>() { "Website" } )},
                       { "IsVarified", new PropertyMappingValue(new List<string>() { "IsVarified" } )},
                       { "IsUnsubscribe", new PropertyMappingValue(new List<string>() { "IsUnsubscribe" } )},
                       { "BusinessType", new PropertyMappingValue(new List<string>() { "BusinessType" } )}
              };

        private Dictionary<string, PropertyMappingValue> _customerPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
                       { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                       { "CustomerName", new PropertyMappingValue(new List<string>() { "CustomerName" } )},
                       { "ContactPerson", new PropertyMappingValue(new List<string>() { "ContactPerson" } )},
                       { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                       { "MobileNo", new PropertyMappingValue(new List<string>() { "MobileNo" } )},
                       { "PhoneNo", new PropertyMappingValue(new List<string>() { "PhoneNo" } )},
                       { "Website", new PropertyMappingValue(new List<string>() { "Website" } )},
                       { "IsVarified", new PropertyMappingValue(new List<string>() { "IsVarified" } )},
                       { "IsUnsubscribe", new PropertyMappingValue(new List<string>() { "IsUnsubscribe" } )}

          };

        private Dictionary<string, PropertyMappingValue> _chemicalPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "Name", new PropertyMappingValue(new List<string>() { "Name" } )},
               { "CasNumber", new PropertyMappingValue(new List<string>() { "CasNumber" } )},
               { "HBondAcceptor", new PropertyMappingValue(new List<string>() { "HBondAcceptor" } )},
               { "HBondDonor", new PropertyMappingValue(new List<string>() { "HBondDonor" } )},
               { "IUPACName", new PropertyMappingValue(new List<string>() { "IUPACName" } )},
               { "InChIKey", new PropertyMappingValue(new List<string>() { "InChIKey" } )},
               { "MolecularFormulla", new PropertyMappingValue(new List<string>() { "MolecularFormulla" } )},
               { "MolecularWeight", new PropertyMappingValue(new List<string>() { "MolecularWeight" } )},
               { "Synonyms", new PropertyMappingValue(new List<string>() { "Synonyms" } )},
               { "ChemicalDetailId", new PropertyMappingValue(new List<string>() { "ChemicalDetailId" } )},
               { "ObjectState", new PropertyMappingValue(new List<string>() { "ObjectState" } )}
            };

        private Dictionary<string, PropertyMappingValue> _inquiryPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                           { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                           { "CompanyName", new PropertyMappingValue(new List<string>() { "CompanyName" } )},
                           { "MobileNo", new PropertyMappingValue(new List<string>() { "MobileNo" } )},
                           { "Phone", new PropertyMappingValue(new List<string>() { "Phone" } )},
                           { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                           { "Status", new PropertyMappingValue(new List<string>() { "InquiryStatus.Name" } )},
                           { "Source", new PropertyMappingValue(new List<string>() { "InquirySource.Name" } )},
                           { "CityName", new PropertyMappingValue(new List<string>() { "City.CityName" } )},
                           { "AssignTo", new PropertyMappingValue(new List<string>() { "AssignUser.FirstName" } )},
                           { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }, true )}
            };

        private Dictionary<string, PropertyMappingValue> _contactUsPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
                           { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                           { "Name", new PropertyMappingValue(new List<string>() { "Name" } )},
                           { "Email", new PropertyMappingValue(new List<string>() { "Email" } )},
                           { "Phone", new PropertyMappingValue(new List<string>() { "Phone" } )},
                           { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }, true )}
          };

        private Dictionary<string, PropertyMappingValue> _articleMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "Title", new PropertyMappingValue(new List<string>() { "Title" } )},
                { "BannerUrl", new PropertyMappingValue(new List<string>() { "BannerUrl" } )},
                { "ShortDescription", new PropertyMappingValue(new List<string>() { "ShortDescription" } )},
                { "LongDescription", new PropertyMappingValue(new List<string>() { "LongDescription" } )},
                { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" } )},
                { "PublishDate", new PropertyMappingValue(new List<string>() { "PublishDate" } )}
            };

        private Dictionary<string, PropertyMappingValue> _documentPropertyMapping =
         new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
         {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "Name", new PropertyMappingValue(new List<string>() { "Name" } )},
               { "Description", new PropertyMappingValue(new List<string>() { "Description" } )},
               { "CreatedBy", new PropertyMappingValue(new List<string>() { "CreatedByUser.FirstName" } )},
               { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" } )},
               { "CategoryName", new PropertyMappingValue(new List<string>() { "Category.Name" } )}
         };
        private Dictionary<string, PropertyMappingValue> _documentAuditTrailPropertyMapping =
        new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "DocumentName", new PropertyMappingValue(new List<string>() { "Document.Name" } )},
               { "OperationName", new PropertyMappingValue(new List<string>() { "OperationName" } )},
               { "DocumentId", new PropertyMappingValue(new List<string>() { "DocumentId" } )},
               { "CategoryName", new PropertyMappingValue(new List<string>() { "Document.Category.Name" } )},
               { "CreatedBy", new PropertyMappingValue(new List<string>() { "CreatedByUser.FirstName" } )},
               { "PermissionUser", new PropertyMappingValue(new List<string>() { "AssignToUser.FirstName" } )},
               { "PermissionRole", new PropertyMappingValue(new List<string>() { "AssignToRole.Name" } )},
               { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" } )}

        };


        private Dictionary<string, PropertyMappingValue> _reminderMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "Subject", new PropertyMappingValue(new List<string>() { "Subject" } )},
                { "Message", new PropertyMappingValue(new List<string>() { "Message" } )},
                { "Frequency", new PropertyMappingValue(new List<string>() { "Frequency" } )},
                { "StartDate", new PropertyMappingValue(new List<string>() { "StartDate" },true )},
                { "EndDate", new PropertyMappingValue(new List<string>() { "EndDate" },true )},
                { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" } )},
                { "IsRepeated", new PropertyMappingValue(new List<string>() { "IsRepeated" } )},
                { "IsEmailNotification", new PropertyMappingValue(new List<string>() { "IsEmailNotification" } )},
                { "IsActive", new PropertyMappingValue(new List<string>() { "IsActive" } )}
            };

        private Dictionary<string, PropertyMappingValue> _reminderSchedulerMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "Subject", new PropertyMappingValue(new List<string>() { "Subject" } )},
                { "Message", new PropertyMappingValue(new List<string>() { "Message" } )},
                { "IsRead", new PropertyMappingValue(new List<string>() { "IsRead" } )},
                { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }, true )}
           };

        private Dictionary<string, PropertyMappingValue> _purchaseOrderMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
                { "POCreatedDate", new PropertyMappingValue(new List<string>() { "POCreatedDate" }, true )},
                { "OrderNumber", new PropertyMappingValue(new List<string>() { "OrderNumber" } )},
                { "SupplierName", new PropertyMappingValue(new List<string>() { "Supplier.SupplierName" } )},
                { "ChemicalName", new PropertyMappingValue(new List<string>() { "Chemical.Name" } )},
                { "TotalAmount", new PropertyMappingValue(new List<string>() { "TotalAmount" } )},
                { "TotalQuantity", new PropertyMappingValue(new List<string>() { "TotalQuantity" } )},
                { "PricePerUnit", new PropertyMappingValue(new List<string>() { "PricePerUnit" } )},
                { "IsClosed", new PropertyMappingValue(new List<string>() { "IsClosed" } )},
                { "InStockQuantity", new PropertyMappingValue(new List<string>() { "InStockQuantity" } )},
                { "PackagingTypeName", new PropertyMappingValue(new List<string>() { "PackagingType.Name" } )},
                { "ClosedDate", new PropertyMappingValue(new List<string>() { "ClosedDate" }, true )}
           };

        private Dictionary<string, PropertyMappingValue> _saleOrderMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
                { "CustomerId", new PropertyMappingValue(new List<string>() { "CustomerId" } ) },
                { "SalesOrderNumber", new PropertyMappingValue(new List<string>() { "SalesOrderNumber" } )},
                { "SalesOrderDate", new PropertyMappingValue(new List<string>() { "SalesOrderDate" }, true )},
                { "ChemicalId", new PropertyMappingValue(new List<string>() { "ChemicalId" } )},
                { "Quantity", new PropertyMappingValue(new List<string>() { "Quantity" } )},
                { "Rate", new PropertyMappingValue(new List<string>() { "Rate" } )},
                { "IsClosed", new PropertyMappingValue(new List<string>() { "IsClosed" } )},
                { "Tax", new PropertyMappingValue(new List<string>() { "Tax" } )},
              { "Total", new PropertyMappingValue(new List<string>() { "Total" } )},
              { "ChemicalName", new PropertyMappingValue(new List<string>() { "Chemical.Name" } )},
              { "CustomerName", new PropertyMappingValue(new List<string>() { "Customer.CustomerName" } )},

          };

        private Dictionary<string, PropertyMappingValue> _expenseMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Reference", new PropertyMappingValue(new List<string>() { "Reference" } ) },
                { "ExpenseCategoryId", new PropertyMappingValue(new List<string>() { "ExpenseCategoryId" } )},
                { "CreatedDate", new PropertyMappingValue(new List<string>() { "CreatedDate" }, true )},
                { "Description", new PropertyMappingValue(new List<string>() { "Description" } )},
                { "ExpenseBy", new PropertyMappingValue(new List<string>() { "ExpenseBy.FirstName" } )},
                { "ExpenseCategory", new PropertyMappingValue(new List<string>() { "ExpenseCategory.Name" } )},
                { "ExpenseDate", new PropertyMappingValue(new List<string>() { "ExpenseDate" }, true )},
                { "ExpenseById", new PropertyMappingValue(new List<string>() { "ExpenseById" })}
            };

        private IList<IPropertyMapping> propertyMappings = new List<IPropertyMapping>();
        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<LoginAuditDto, LoginAudit>(_loginAuditMapping));
            propertyMappings.Add(new PropertyMapping<UserDto, User>(_userMapping));
            propertyMappings.Add(new PropertyMapping<NLogDto, NLog>(_nLogMapping));
            propertyMappings.Add(new PropertyMapping<SupplierDto, Supplier>(_supplierPropertyMapping));
            propertyMappings.Add(new PropertyMapping<ChemicalDto, Chemical>(_chemicalPropertyMapping));
            propertyMappings.Add(new PropertyMapping<InquiryDto, Inquiry>(_inquiryPropertyMapping));
            propertyMappings.Add(new PropertyMapping<CustomerDto, Customer>(_customerPropertyMapping));
            propertyMappings.Add(new PropertyMapping<ContactUsDto, ContactRequest>(_contactUsPropertyMapping));
            propertyMappings.Add(new PropertyMapping<ArticleDto, Article>(_articleMapping));
            propertyMappings.Add(new PropertyMapping<DocumentDto, Document>(_documentPropertyMapping));
            propertyMappings.Add(new PropertyMapping<DocumentAuditTrailDto, DocumentAuditTrail>(_documentAuditTrailPropertyMapping));
            propertyMappings.Add(new PropertyMapping<ReminderDto, Reminder>(_reminderMapping));
            propertyMappings.Add(new PropertyMapping<ReminderSchedulerDto, ReminderScheduler>(_reminderSchedulerMapping));
            propertyMappings.Add(new PropertyMapping<PurchaseOrderDto, PurchaseOrder>(_purchaseOrderMapping));
            propertyMappings.Add(new PropertyMapping<SalesOrderListDto, SalesOrder>(_saleOrderMapping));
            propertyMappings.Add(new PropertyMapping<ExpenseDto, Expense>(_expenseMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;

        }

    }
}
