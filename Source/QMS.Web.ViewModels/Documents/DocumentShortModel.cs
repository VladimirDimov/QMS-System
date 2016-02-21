namespace QMS.Web.ViewModels.Documents
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class DocumentShortViewModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
