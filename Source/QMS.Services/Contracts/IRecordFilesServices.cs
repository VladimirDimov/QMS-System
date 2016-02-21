namespace QMS.Services.Contracts
{
    using QMS.Models;

    public interface IRecordFilesServices
    {
        RecordFile Create(int id);

        void SetPath(RecordFile recordFile, string savePath);
    }
}
