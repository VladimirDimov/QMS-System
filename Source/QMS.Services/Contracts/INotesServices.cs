namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface INotesServices
    {
        void Create(int recordId, string title, string text);

        void delete(int id);

        IQueryable<Note> GetUserNotes(string userId);
    }
}