namespace QMS.Web.Models.Timesheet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TimesheetViewModel
    {
        public IEnumerable Events { get; set; }

        public int MinYear { get; set; }

        public int MaxYear { get; set; }
    }
}
