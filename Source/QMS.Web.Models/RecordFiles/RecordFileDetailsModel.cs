namespace QMS.Web.Models.RecordFiles
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using System;

    public class RecordFileDetailsViewModel : IMapFrom<RecordFile>
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
