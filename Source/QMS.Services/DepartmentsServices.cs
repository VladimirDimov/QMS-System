namespace QMS.Services
{
    using Contracts;
    using QMS.Data;
    using QMS.Models;
    using System.Linq;

    public class DepartmentsServices : IDepartmentsServices
    {
        private IQmsData data;

        public DepartmentsServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<Department> All()
        {
            return this.data.Departments.All();
        }

        public Department GetById(int id)
        {
            return this.data.Departments.GetById(id);
        }

        public Department Create(string name, string description, int divisionId)
        {
            var departmentToAdd = new Department
            {
                Name = name,
                Description = description,
                DivisionId = divisionId
            };

            this.data.Departments.Add(departmentToAdd);
            this.data.SaveChanges();

            return departmentToAdd;
        }

        public void Update(int id, string name, string description, int divisionId)
        {
            var dbModel = this.data.Departments.GetById(id);
            dbModel.Name = name;
            dbModel.Description = description;
            dbModel.DivisionId = divisionId;
            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            this.data.Departments.Delete(id);
            this.data.SaveChanges();
        }
    }
}
