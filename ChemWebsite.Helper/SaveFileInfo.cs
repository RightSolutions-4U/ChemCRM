using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Helper
{
    public static class FileData
    {
        public static async Task SaveFile(string path, string source)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string base64 = source.Split(',').LastOrDefault();
            if (!string.IsNullOrWhiteSpace(base64))
            {
                byte[] bytes = Convert.FromBase64String(base64);
                await System.IO.File.WriteAllBytesAsync(path, bytes);
            }
        }

        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
