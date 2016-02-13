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

        public Division Create(string name, string description)
        {
            var newDivision = new Division
            {
                Name = name,
                Description = description
            };

            this.data.Divisions.Add(newDivision);
            this.data.SaveChanges();

            return newDivision;
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
