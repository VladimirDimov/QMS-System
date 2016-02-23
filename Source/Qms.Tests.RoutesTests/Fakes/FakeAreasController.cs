using QMS.Models;
using QMS.Services.Contracts;
using QMS.Web.Areas.Private.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qms.Tests.RoutesTests.Fakes
{
    public class FakeAreasController : AreasController
    {
        public FakeAreasController(
            IAreasServices areas,
            IRecordsServices records,
            IDocumentsServices documents,
            IUsersServices users) : base(areas, records, documents, users)
        {
        }

        public void CreateFileOfRecord(string fromPath, string toPath)
        {
            // Do nothing
        }

        public Record Create(string a, string b, DateTime c, DateTime d,
                RecordStatus e, DateTime f, int g, int h, string i)
        {
            return new Record() { };
        }
    }
}
