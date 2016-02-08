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
    }
}
