using System.IO;
using Microsoft.AspNetCore.Http;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class FileUtils
    {
        public static string SaveFile(string rootPath, IFormFile file)
        {
            var webRootPath = rootPath;
            var path = webRootPath + "/upload/import";
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
                }
                catch (IOException)
                {
                    return;
                }
            }
        }
    }
}