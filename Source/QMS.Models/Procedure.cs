﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Models
{
    public class Procedure
    {
        private ICollection<Document> documents;

        public Procedure()
        {
            this.documents = new HashSet<Document>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Document> Documents
        {
            get { return this.documents; }
            set { this.documents = value; }
        }
    }
}
