using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChemWebsite.Repository
{
    public class ChemicalRepository : GenericRepository<Chemical, ChemWebsiteDbContext>, IChemicalRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly PathHelper _pathHelper;
        public ChemicalRepository(
          IUnitOfWork<ChemWebsiteDbContext> uow,
          IPropertyMappingService propertyMappingService,
          PathHelper pathHelper)
            : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _pathHelper = pathHelper;
        }

        public async Task<ChemicalList> GetChemicals(
                  ChemicalResource chemicalResourceParameters)
        {
            var collectionBeforePaging =
                All.ApplySort(chemicalResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<ChemicalDto, Chemical>());

            if (chemicalResourceParameters.ChemicalId != null && chemicalResourceParameters.ChemicalId != Guid.Empty)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Id == chemicalResourceParameters.ChemicalId);
            }

            if (!string.IsNullOrEmpty(chemicalResourceParameters.CasNumber) && !string.IsNullOrEmpty(chemicalResourceParameters.Name))
            {
                // trim & ignore casing
                var genreForWhereClause = chemicalResourceParameters.CasNumber
                    .Trim().ToLowerInvariant();
                var encodingName = GetUnescapestring(chemicalResourceParameters.Name);
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.CasNumber, $"{genreForWhereClause}%") || EF.Functions.Like(a.Name, $"%{encodingName}%", @"\"));

            }
            else if (!string.IsNullOrEmpty(chemicalResourceParameters.CasNumber))
            {
                // trim & ignore casing
                var genreForWhereClause = chemicalResourceParameters.CasNumber
                    .Trim().ToLowerInvariant();
                var encodingName = GetUnescapestring(chemicalResourceParameters.Name);
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.CasNumber, $"{genreForWhereClause}%") );

            }
            else if (!string.IsNullOrEmpty(chemicalResourceParameters.Name))
            {
                // trim & ignore casing
                var genreForWhereClause = chemicalResourceParameters.Name
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Name, $"%{encodingName}%", @"\"));
            }

            if (!string.IsNullOrEmpty(chemicalResourceParameters.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = chemicalResourceParameters.SearchQuery
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.CasNumber, $"%{searchQueryForWhereClause}%")
                                || EF.Functions.Like(c.Name, $"%{searchQueryForWhereClause}%"));
            }
            var chemicalPageList = new ChemicalList(_pathHelper);
            return await chemicalPageList.Create(collectionBeforePaging,
                chemicalResourceParameters.Skip,
                chemicalResourceParameters.PageSize);
        }


        private string GetUnescapestring(string str)
        {
            var genreForWhereClause = str.Trim().ToLowerInvariant();
            var name = Uri.UnescapeDataString(genreForWhereClause);
            var encodingName = WebUtility.UrlDecode(name);
            return Regex.Unescape(encodingName);
        }

    }
}
