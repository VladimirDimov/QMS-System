namespace QMS.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int RecordId { get; set; }

        public virtual Record Record { get; set; }
    }
}