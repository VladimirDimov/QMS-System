namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IDepartmentsServices
    {
        IQueryable<Department> All();

        Department Create(string name, string description, int divisionId);

        void Delete(int id);

        Department GetById(int id);

        void Update(int id, string name, string description, int divisionId);
    }
}