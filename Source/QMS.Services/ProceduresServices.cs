﻿namespace QMS.Services
{
    using QMS.Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ProceduresServices
    {
        private IQmsData data;

        public ProceduresServices(IQmsData data)
        {
            this.data = data;
        }

        IQueryable<Procedure> All()
        {
            return this.data.Procedures.All();
        }

        public Procedure GetById(int id)
        {
            return this.data.Procedures.GetById(id);
        }

        public void Add(string name, string description, string filePath)
        {
            this.data.Procedures
                .Add(new Procedure
                {
                    Name = name,
                    Description = description,
                    FilePath = filePath
                });
        }
    }
}