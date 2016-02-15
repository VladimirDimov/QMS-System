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
            //Create record
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

            this.data.Records.Add(record);

            //Create file to record
            var recordFile = new RecordFile
            {
                DateUpdated = DateTime.Now,
                RecordId = record.Id
            };

            this.data.RecordFiles.Add(recordFile);

            record.RecordFiles.Add(recordFile);
            this.data.SaveChanges();

            var filePath = $"~/Files/Records/{recordFile.Id % 1000}/{recordFile.Id}-{DateTime.Now.ToShortDateString()}{fileExtension}";

            recordFile.Path = filePath;
            this.data.SaveChanges();

            return record;
        }

        public Record GetById(int id)
        {
            return this.data.Records.GetById(id);
        }

        public Record Update(int id, string title, string description, RecordStatus status,
            DateTime statusDate, DateTime dateCreated, DateTime? finishingDate, int documentId)
        {
            var record = this.data.Records.GetById(id);

            record.Title = title;
            record.Description = description;
            record.Status = status;
            record.StatusDate = statusDate;
            record.DateCreated = dateCreated;
            record.FinishingDate = finishingDate;
            record.DocumentId = documentId;

            this.data.SaveChanges();
            return record;
        }

        public IOrderedQueryable<Record> GetUpcomingByAreaId(int id)
        {
            var upcoming = this.data.Records.All()
                .Where(r => r.AreaId == id)
                .Where(r => r.Status == RecordStatus.Active)
                .Where(r => r.FinishingDate != null && r.FinishingDate > DateTime.UtcNow)
                .OrderBy(r => r.FinishingDate);

            return upcoming;
        }

        public IOrderedQueryable<Record> GetMissedByAreaId(int id)
        {
            var missed = this.data.Records.All()
                .Where(r => r.AreaId == id)
                .Where(r => r.Status == RecordStatus.Active)
                .Where(r => r.FinishingDate != null && r.FinishingDate < DateTime.UtcNow)
                .OrderBy(r => r.FinishingDate);

            return missed;
        }

        public void UpdateFile(int recordId, int recordFileId)
        {
            var record = this.data.Records.GetById(recordId);
            var recordFile = this.data.RecordFiles.GetById(recordFileId);
            record.RecordFiles.Add(recordFile);
            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            this.data.Records.Delete(id);
            this.data.SaveChanges();
        }
    }
}
