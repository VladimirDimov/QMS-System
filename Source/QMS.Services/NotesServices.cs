namespace QMS.Services
{
    using Contracts;
    using QMS.Data;
    using QMS.Models;
    using System;
    using System.Linq;

    public class NotesServices : INotesServices
    {
        private IQmsData data;

        public NotesServices(IQmsData data)
        {
            this.data = data;
        }

        public void Create(int recordId, string title, string text)
        {
            var note = new Note
            {
                Title = title,
                Text = text,
                RecordId = recordId,
                CreatedOn = DateTime.UtcNow
            };

            this.data.Notes.Add(note);
            this.data.SaveChanges();
        }

        public void delete(int id)
        {
            this.data.Notes.Delete(id);
            this.data.SaveChanges();
        }

        public IQueryable<Note> GetUserNotes(string userId)
        {
            var notes = this.data.Notes.All()
                .Where(n => n.Record.Area.EmployeeId == userId);

            return notes;
        }
    }
}
