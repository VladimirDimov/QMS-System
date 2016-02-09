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

        public int Add(string title, string description, string code)
        {
            var document = new Document
            {
                Title = title,
                Description = description,
                Code = code,
                LastUpdate = DateTime.Now
            };

            this.data.Documents.Add(document);
            this.data.SaveChanges();
            return document.Id;
        }

        public void Update(string title, string description, string code)
        {
            var document = new Document
            {
                Title = title,
                Description = description,
                Code = code
            };

            this.data.Documents.Add(document);
            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            this.data.Documents.Delete(id);
        }
    }
}
