using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Services
{
    public class DivisionsServices
    {
        private IQmsData data;

        public DivisionsServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<Division> GetAll()
        {
            return this.data.Divisions.All();
        }

        public void Create(string name, string description)
        {
            this.data.Divisions.Add(new Division
            {
                Name = name,
                Description = description
            });

            this.data.SaveChanges();
        }

        public Division GetById(int id)
        {
            return this.data.Divisions.GetById(id);
        }

        public void Update(int id, string name, string description)
        {
            var divisionToUpdate = this.data.Divisions.GetById(id);
            if (divisionToUpdate == null)
            {
                throw new ArgumentException("Invalid division id");
            }
            else
            {
                divisionToUpdate.Name = name;
                divisionToUpdate.Description = description;
                this.data.SaveChanges();
            }
        }
    }
}
