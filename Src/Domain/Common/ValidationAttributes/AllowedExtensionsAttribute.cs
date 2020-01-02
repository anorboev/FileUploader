using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Domain.Common.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;            
            if (!(file == null))
            {
                var extension = Path.GetExtension(file.FileName);
                var serviceOptions = (IOptions<FileSettings>)validationContext.GetService(typeof(IOptions<FileSettings>));
                var service = serviceOptions.Value;

                if (!service.AllowedFileExtensionsList.Any(x => extension.ToLower().EndsWith(x)))
                {
                    return new ValidationResult(GetErrorMessage(extension));
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string extension)
        {
            return $"This file extension {extension} is not allowed!";
        }
    }
}
