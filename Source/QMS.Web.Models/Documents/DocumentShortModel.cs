using QMS.Models;
using QMS.Web.Infrastructure.Mappings;

namespace QMS.Web.Models.Documents
{
    public class DocumentShortModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
