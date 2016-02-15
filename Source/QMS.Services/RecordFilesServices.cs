namespace QMS.Services
{
    using System;
    using QMS.Data;
    using Models;
    using System.IO;
    using System.Transactions;
    using System.Web;

    public class RecordFilesServices
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
