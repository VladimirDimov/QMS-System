namespace QMS.Services
{
    using QMS.Data;
    using QMS.Models;
    using System.Linq;
    using System;

    public class RecordsServices
    {
        private IQmsData data;

        public RecordsServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<Record> All()
        {
            return this.data.Records.All();
        }

        public IQueryable<Record> GetByAreaId(int id)
        {
            return this.data.Records.All()
                .Where(r => r.AreaId == id);
        }

        public Record Create(string title, string description, DateTime dateCreated, DateTime? finishingDate,
            RecordStatus status, DateTime statusDate, int documentId, int areaId, string fileExtension)
        {
            var record = new Record
            {
                Title = title,
                Description = description,
                AreaId = areaId,
                DateCreated = dateCreated,
                FinishingDate = finishingDate,
                Status = status,
                StatusDate = statusDate,
                DocumentId = documentId
            };

            var recordFile = new RecordFile
            {
                DateUpdated = DateTime.Now
            };

            this.data.Records.Add(record);
            this.data.RecordFiles.Add(recordFile);
            this.data.SaveChanges();

            var filePath = $"~/Files/Records/{recordFile.Id % 1000}/{recordFile.Id}-{DateTime.Now.ToShortDateString()}{fileExtension}";

            recordFile.Path = filePath;
            record.RecordFile = recordFile;
            this.data.SaveChanges();

            return record;
        }

        public Record GetById(int id)
        {
            return this.data.Records.GetById(id);
        }
    }
}
