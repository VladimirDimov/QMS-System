namespace QMS.Web.ViewModels.RecordFiles
{
    using System.ComponentModel.DataAnnotations;

    public class RecordFileCreateEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
