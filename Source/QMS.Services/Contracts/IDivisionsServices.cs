namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IDivisionsServices
    {
        Division Create(string name, string description);

        void delete(int id);

        IQueryable<Division> GetAll();

        Division GetById(int id);

        void Update(int id, string name, string description);
    }
}