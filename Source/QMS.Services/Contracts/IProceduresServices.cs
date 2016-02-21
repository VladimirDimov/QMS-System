namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IProceduresServices
    {
        Procedure Add(string name, string description);

        IQueryable<Procedure> All();

        Procedure GetById(int id);

        void Update(int id, string name, string description);
    }
}
