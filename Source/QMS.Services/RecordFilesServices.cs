namespace QMS.Services
{
    using Contracts;
    using Models;
    using QMS.Data;
    using System;

    public class RecordFilesServices : IRecordFilesServices
    {
        private IQmsData data;

        public RecordFilesServices(IQmsData data)
        {
            this.data = data;
        }

        public RecordFile Create(int id)
        {
            var recordFile = new RecordFile
            {
                DateUpdated = DateTime.UtcNow,
                RecordId = id
            };

            this.data.RecordFiles.Add(recordFile);
            this.data.SaveChanges();

            return recordFile;
        }

        public void SetPath(RecordFile recordFile, string savePath)
        {
            recordFile.Path = savePath;
            this.data.SaveChanges();
        }
    }
}
