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

        public void Add(string name, string description, int departmentId)
        {
            this.data.Areas.Add(new Area
            {
                Name = name,
                Description = description,
                DepartmentId = departmentId
            });

            this.data.SaveChanges();
        }

        public Area GetById(int id)
        {
            return this.data.Areas.GetById(id);
        }

        public void Update(int id, string name, string description)
        {
            var area = this.data.Areas.GetById(id);
            area.Name = name;
            area.Description = description;
            this.data.SaveChanges();
        }
    }
}
