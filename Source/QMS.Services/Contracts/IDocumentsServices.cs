namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IDocumentsServices
    {
        int Add(string title, string description, string code, int procedureId);

        IQueryable<Document> All();

        void Delete(int id);

        Document GetById(int id);

        void Update(int id, string title, string description, string code, int procedureId);

        void UpdateFilePath(Document document, string filePath);
    }
}