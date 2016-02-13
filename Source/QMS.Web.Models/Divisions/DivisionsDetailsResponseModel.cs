namespace QMS.Web.Models.Divisions
{
    using QMS.Models;
    using Infrastructure.Mappings;
    using QMS.Web.Models.Departments;
    using System.Collections.Generic;
    using AutoMapper;
    using System;
    using System.ComponentModel;

    public class DivisionsDetailsResponseModel : IMapFrom<Division>
    {
        public int Id { get; set; }

        [DisplayName("Division name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DepartmentDetailsSmallResponseModel> Departments { get; set; }
    }
}
