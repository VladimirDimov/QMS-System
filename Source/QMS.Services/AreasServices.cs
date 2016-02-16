using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Services
{
    public class AreasServices
    {
        private IQmsData data;

        public AreasServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<Area> all()
        {
            return this.data.Areas.All();
        }

        public int Add(string name, string description, int departmentId, string employeeId)
        {
            var areaToAdd = new Area
            {
                Name = name,
                Description = description,
                DepartmentId = departmentId,
                EmployeeId = employeeId
            };

            this.data.Areas.Add(areaToAdd);
            this.data.SaveChanges();
            return areaToAdd.Id;
        }

        public IQueryable<Area> GetByUserId(string userId)
        {
            return this.data.Areas.All()
                .Where(a => a.EmployeeId == userId);
        }

        public Area GetById(int id)
        {
            return this.data.Areas.GetById(id);
        }

        public void Update(int id, string name, string description, string employeeId)
        {
            var area = this.data.Areas.GetById(id);
            area.Name = name;
            area.Description = description;
            area.EmployeeId = employeeId;
            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            this.data.Areas.Delete(id);
            this.data.SaveChanges();
        }
    }
}
