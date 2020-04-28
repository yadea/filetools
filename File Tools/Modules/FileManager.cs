using System;
using System.Collections;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace File_Tools.Modules
{
    class FileManager
    {
        public string[] GenerateFileList(string path, bool isRelativePath = false)
        {
            string[] files = null;
            try
            {
                if (isRelativePath)
                    files = GetRelativePaths(path).ToArray<string>();
                else
                    files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            }
            catch (Exception ex)
            {
                Logging.Log("GenerateFileList: " + ex, Logging.LogType.Error);
            }
            return files;
        }

        IEnumerable<string> GetRelativePaths(string path)
        {
            int rootLength = path.Length + (path[path.Length - 1] == '\\' ? 0 : 1);

            foreach (string p in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                yield return p.Remove(0, rootLength);
            }
        }
    }
}
