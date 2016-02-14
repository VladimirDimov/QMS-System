﻿namespace QMS.Web.Models.Documents
{
    using QMS.Models;
    using QMS.Web.Infrastructure.Mappings;
    using QMS.Web.Models.Procedures;
    using System;

    public class DocumentDetailsModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string FilePath { get; set; }

        public DateTime LastUpdate { get; set; }

        public ProcedureShortModel Procedure { get; set; }
    }
}