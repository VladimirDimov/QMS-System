namespace QMS.Web.Models.Areas
{
    using Records;
    using System.Linq;

    public class AreaManageModel
    {
        public AreaDetailsModel Area { get; set; }

        public IQueryable<RecordListModel> Records { get; set; }
    }
}
