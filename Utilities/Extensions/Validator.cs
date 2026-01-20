using Microsoft.AspNetCore.Http;
using Simulation7.Utilities.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Simulation7.Utilities.Extensions
{
    public static class Validator
    {
        public static bool ValidateType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool ValidateSize(this IFormFile file, FileSize fileSize, int size)
        {
            switch (fileSize)
            {
                case FileSize.KB:
                    return file.Length <= size * 1024;

                case FileSize.MB:
                    return file.Length <= size * 1024 * 1024;

                case FileSize.GB:
                    return file.Length <= size * 1024 * 1024 * 1024;
            }
            return false;
        }

        public static async Task<string> CreateFileAsync(
            this IFormFile file,
            string root,
            params string[] folders)
        {
            string fileName = Guid.NewGuid() + file.FileName;

            string folderPath = root;
            foreach (string folder in folders)
            {
                folderPath = Path.Combine(folderPath, folder);
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
