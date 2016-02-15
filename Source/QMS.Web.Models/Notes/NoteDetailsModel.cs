namespace QMS.Web.Models.Notes
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;

    public class NoteDetailsModel : IMapFrom<Note>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int RecordId { get; set; }
    }
}
