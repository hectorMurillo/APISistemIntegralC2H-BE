using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace C2HApiControlInterno
{
    public class Util
    {
        public static String GetRutaExecutableBin()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(codeBase);
            var pathDll = uri.LocalPath;
            String path = Path.GetDirectoryName(pathDll);

            return path;
        }

        public static string GetTempFolderGuid()
        {
            string folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            while (Directory.Exists(folder) | File.Exists(folder))
            {
                folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            }

            return folder;
        }

        public static string GetTempFolderGuid(String PathPersonalizado)
        {
            string folder = Path.Combine(PathPersonalizado, Guid.NewGuid().ToString());
            while (Directory.Exists(folder) | File.Exists(folder))
            {
                folder = Path.Combine(PathPersonalizado, Guid.NewGuid().ToString());
            }

            if (!File.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }
    }
}