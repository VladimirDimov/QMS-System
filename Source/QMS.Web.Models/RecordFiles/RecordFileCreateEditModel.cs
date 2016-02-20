namespace QMS.Web.Models.RecordFiles
{
    using System.ComponentModel.DataAnnotations;

    public class RecordFileCreateEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
