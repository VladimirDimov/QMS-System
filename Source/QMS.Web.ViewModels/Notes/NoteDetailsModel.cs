namespace QMS.Web.ViewModels.Notes
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using System;
    using System.ComponentModel;

    public class NoteDetailsViewModel : IMapFrom<Note>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int RecordId { get; set; }

        [DisplayName("Created on")]
        public DateTime CreatedOn { get; set; }
    }
}
