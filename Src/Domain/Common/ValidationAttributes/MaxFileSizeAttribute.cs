﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {        
            var file = value as IFormFile;
            if (file != null)
            {
                var serviceOptions = (IOptions<FilePolicy>)validationContext.GetService(typeof(IOptions<FilePolicy>));
                var service = serviceOptions.Value;
                var allowedSizeinBytes = service.AllowedFileSize * 1024 * 1024;

                if (file.Length > allowedSizeinBytes)
                {
                    return new ValidationResult(GetErrorMessage(service.AllowedFileSize));
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(int size)
        {
            return $"Maximum allowed file size is { size} MB.";
        }
    }
}
