using QMS.Models;
using QMS.Web.Infrastructure.Mappings;

namespace QMS.Web.ViewModels.Areas
{
    public class AreaShortViewModel : IMapFrom<Area>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
