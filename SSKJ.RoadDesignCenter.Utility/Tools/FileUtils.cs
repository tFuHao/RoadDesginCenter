using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class FileUtils
    {
        public static string SaveFile(string rootPath, IFormFile file, string userId)
        {
            var webRootPath = rootPath;
            var path = webRootPath + "/upload/import/" + userId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fullPath = path + '/' + file.FileName;
            fullPath = fullPath.Replace("\\", "/");
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fs);
                fs.Flush();
                fs.Close();
            }
            return fullPath;
        }

        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                    DeleteDirectory(path);
                }
                catch (IOException)
                {
                    return;
                }
            }
        }

        private static void DeleteDirectory(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var array = path.Split("/");
                var fullPath = "";
                for (var i = 0; i < array.Length - 1; i++)
                {
                    fullPath += $"{array[i]}/";
                }
                var directory = new DirectoryInfo(fullPath);
                var count = directory.GetFiles();
                if (count.Length == 0)
                {
                    Directory.Delete(fullPath);
                }
            }
        }
    }
}