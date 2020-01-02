using System;

namespace Domain.ViewModels
{
    public class FileViewModel
    {
        public int FileId { get; set; }

        public string Extension { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public string CreatedBy { get; set; }

        public string Created => _created.ToString("dd.MM.yyyy");

        public DateTime _created { get; set; }
    }
}
