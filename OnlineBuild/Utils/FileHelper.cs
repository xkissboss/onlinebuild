using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Utils
{
    public class FileHelper
    {
        public static void SaveAsFile(string content, string filename, string path)
        {
            using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                }
            }
        }

        public static bool IsExists(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Exists;
        }

        public static string GetDir(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.DirectoryName;
        }

        public static string GetName(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Name;
        }

        public static string DirectorySepChar = Path.DirectorySeparatorChar.ToString();
    }
}
