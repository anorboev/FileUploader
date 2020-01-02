using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class FileSettingsViewModel
    {
        public int AllowedFileSize { get; set; }
        public string[] AllowedFileExtensions { get; set; }
    }
}
