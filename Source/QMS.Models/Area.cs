namespace QMS.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Area
    {
        private ICollection<Record> records;

        public Area()
        {
            this.records = new HashSet<Record>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int EmployeeId { get; set; }

        public virtual User Employee { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Record> Records
        {
            get { return this.records; }
            set { this.records = value; }
        }
    }
}
