using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class FileDownloadViewModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
