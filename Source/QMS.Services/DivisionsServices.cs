﻿namespace QMS.Services
{
    using Contracts;
    using QMS.Data;
    using QMS.Models;
    using System;
    using System.Linq;

    public class DivisionsServices : IDivisionsServices
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

        public void delete(int id)
        {
            this.data.Divisions.Delete(id);
            this.data.SaveChanges();
        }
    }
}
