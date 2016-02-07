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
    }
}
