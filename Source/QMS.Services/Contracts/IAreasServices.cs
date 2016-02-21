namespace QMS.Services.Contracts
{
    using QMS.Models;
    using System.Linq;

    public interface IAreasServices
    {
        int Add(string name, string description, int departmentId, string employeeId);

        IQueryable<Area> all();

        void Delete(int id);

        Area GetById(int id);

        IQueryable<Area> GetByUserId(string userId);

        void Update(int id, string name, string description, string employeeId);
    }
}