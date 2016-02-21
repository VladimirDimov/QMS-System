namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System;
    using System.Linq;

    public interface IRecordsServices
    {
        IQueryable<Record> All();

        Record Create(string title, string description, DateTime dateCreated, DateTime? finishingDate, RecordStatus status, DateTime statusDate, int documentId, int areaId, string fileExtension);

        void Delete(int id);

        IQueryable<Record> GetByAreaId(int id);

        Record GetById(int id);

        IOrderedQueryable<Record> GetMissedByAreaId(int id);

        IOrderedQueryable<Record> GetUpcomingByAreaId(int id);

        IQueryable<Record> GetUserUpcomingRecords(string userId);

        Record Update(int id, string title, string description, RecordStatus status, DateTime statusDate, DateTime dateCreated, DateTime? finishingDate, int documentId);

        void UpdateFile(int recordId, int recordFileId);
    }
}