using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<DocumentList> GetDocuments(DocumentResource documentResource);
        Task<DocumentList> GetDocumentsLibrary(string email, DocumentResource documentResource);
        Task<DocumentDto> GetDocumentById(Guid Id);
    }
}
