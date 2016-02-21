namespace QMS.Models
{
    using System;

    public class Log
    {
        public int Id { get; set; }

        public DateTime DateOccured { get; set; }

        public string ExceptionMessage { get; set; }

        public string UserIP { get; set; }
    }
}
