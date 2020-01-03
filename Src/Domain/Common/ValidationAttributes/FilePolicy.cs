namespace Domain.Common.ValidationAttributes
{
    public class FilePolicy
    {
        public int AllowedFileSize { get; set; }
        public string AllowedFileExtensions { get; set; }
        public string FolderPath { get; set; }
        public string[] AllowedFileExtensionsList => AllowedFileExtensions.Split(",");
    }
}
