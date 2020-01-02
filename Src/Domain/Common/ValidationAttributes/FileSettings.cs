using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.ValidationAttributes
{
    public class FileSettings
    {
        public int AllowedFileSize { get; set; }
        public string AllowedFileExtensions { get; set; }
        public string FolderPath { get; set; }
        public string[] AllowedFileExtensionsList => AllowedFileExtensions.Split(",");
    }
}
