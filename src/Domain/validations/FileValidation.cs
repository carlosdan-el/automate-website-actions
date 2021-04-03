using System;
using System.IO;

namespace Domain.Validations
{
    public class FileValidation
    {
        public static void FileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{filePath}' not exists.");
            }
        }

        public static void ExtensionIsValid(string filePath, string extension = ".json")
        {
            string ext = Path.GetExtension(filePath);

            if(ext != extension)
            {
                throw new Exception($"Settings file extension is invalid. Allowed is '{extension}', '{ext}' has given.");
            }
        }
    }
}
