namespace QMS.Web.Models.Documents
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DocumentShortViewModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
