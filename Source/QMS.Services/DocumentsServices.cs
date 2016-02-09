using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Services
{
    public class DocumentsServices
    {
        private IQmsData data;

        public DocumentsServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<Document> All()
        {
            return this.data.Documents.All();
        }

        public Document GetById(int id)
        {
            var model = this.data.Documents.GetById(id);
            return model;
        }

        public int Add(string title, string description, string code, int procedureId)
        {
            var document = new Document
            {
                Title = title,
                Description = description,
                Code = code,
                ProcedureId = procedureId,
                LastUpdate = DateTime.Now
            };

            this.data.Documents.Add(document);
            this.data.SaveChanges();
            return document.Id;
        }

        public void Update(int id, string title, string description, string code, int procedureId)
        {
            var doxcumentToUpdate = this.data.Documents.GetById(id);

            doxcumentToUpdate.Title = title;
            doxcumentToUpdate.Description = description;
            doxcumentToUpdate.Code = code;
            doxcumentToUpdate.ProcedureId = procedureId;

            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            this.data.Documents.Delete(id);
        }

        public void UpdateFilePath(Document document, string filePath)
        {
            document.FilePath = filePath;
            this.data.SaveChanges();
        }
    }
}
