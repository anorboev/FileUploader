using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Helpers
{
    public static class ExtensionMethods
    {
        public static double ColculateSize(this IFormFile file)
        {
            return Math.Round((double)(file.Length), 2);
        }

        public static string GetFileExtension(this IFormFile file)
        {
            return Path.GetExtension(file.FileName);
        }
    }
}
