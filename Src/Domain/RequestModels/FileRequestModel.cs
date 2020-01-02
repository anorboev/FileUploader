using Domain.Common.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.RequestModels
{
    public class FileRequestModel
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxFileSize]
        [AllowedExtensions]
        public IFormFile File { set; get; }

        public static string GenerateSystemName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
