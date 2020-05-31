using System;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace File_Tools.Modules
{
    public class FileManager
    {
        frmResults FRMRESULTS;

        public FileManager() { }
        public FileManager(frmResults frmResults)
        {
            FRMRESULTS = frmResults;
        }

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

        public bool FileExists(string path, string file)
        {
            string destFile = Path.Combine(path, file);
            if (File.Exists(destFile))
                return true;
            return false;
        }

        /*to test:
          1. copy file from a to b in root folder [PASS]
          2. copy file from subfolders to dest without those subfolders [FAIL DirectoryNotFound]
          3. copy large file to dest and see if file can be successfully verified. [FAIL OutOfMemoryException]
        */
        public void CopyFiles(string[] files, string source, string destination)
        {
            foreach (string file in files)
            {
                //generate files md5
                string md5 = null;
                string SOURCEFILE = Path.Combine(source, file);
                string DESTFILE = Path.Combine(destination, file);

                //generate MD5
                for (int retryMD5 = 0; retryMD5 < 3; retryMD5++)
                {
                    try
                    {
                        using (MD5 hasher = MD5.Create())
                        {
                            using (FileStream stream = File.OpenRead(SOURCEFILE))
                            {
                                byte[] hash = hasher.ComputeHash(stream);
                                md5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                            }
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (retryMD5 == 2)
                        {
                            Logging.Log("CopyFiles@md5 - " + ex, Logging.LogType.Error);
                            if (FRMRESULTS != null)
                                FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        Task.Delay(500);
                    }
                }

                if (md5 == null)
                    continue;

                //check directory tree 
                if (file.Contains('\\'))
                {
                    for (int retryCreateDir = 0; retryCreateDir < 3; retryCreateDir++)
                    {
                        string destFilePath = file.Substring(0, file.LastIndexOf('\\'));
                        try
                        {
                            if (!Directory.Exists(destination + destFilePath))
                            {
                                Directory.CreateDirectory(destination + destFilePath);
                                Task.Delay(100);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.Log("CopyFiles@dir - " + ex, Logging.LogType.Error);

                            if (FRMRESULTS != null)
                                FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }

                //copy file
                for (int retryCopy = 0; retryCopy < 3; retryCopy++)
                {
                    try
                    {
                        File.Copy(SOURCEFILE, DESTFILE);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (retryCopy == 2)
                        {
                            Logging.Log("CopyFiles@copy - " + ex, Logging.LogType.Error);

                            if (FRMRESULTS != null)
                                FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        Task.Delay(500);
                    }
                }

                //verify destination file md5
                bool filecopysuccess = false;
                for (int retryMD5 = 0; retryMD5 < 3; retryMD5++)
                {
                    try
                    {
                        using (var hasher = MD5.Create())
                        {
                            using (var stream = File.OpenRead(DESTFILE))
                            {
                                byte[] hash = hasher.ComputeHash(stream);
                                if (md5 == BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant())
                                    filecopysuccess = true;
                            }
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (retryMD5 == 2)
                        {
                            Logging.Log("CopyFiles@md5dest - " + ex, Logging.LogType.Error);
                            if (FRMRESULTS != null)
                                FRMRESULTS.ShowMessagebox("File " + file + " copied but failed to verify integrity.", "Error verifying file.", System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        Task.Delay(500);
                    }
                }
            }
        }

        public bool CopyFile(string file, string source, string destination, bool overwriteFile=false)
        {
            //generate files md5
            string md5 = null;
            string SOURCEFILE = Path.Combine(source, file);
            string DESTFILE = Path.Combine(destination, file);

            //generate MD5
            for (int retryMD5 = 0; retryMD5 < 3; retryMD5++)
            {
                try
                {
                    using (MD5 hasher = MD5.Create())
                    {
                        using (FileStream stream = File.OpenRead(SOURCEFILE))
                        {
                            byte[] hash = hasher.ComputeHash(stream);
                            md5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (retryMD5 == 2)
                    {
                        Logging.Log("CopyFiles@md5 - " + ex, Logging.LogType.Error);
                        if (FRMRESULTS != null)
                            FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    Task.Delay(500);
                }
            }

            if (md5 == null)
                return false;

            //check directory tree 
            if (file.Contains('\\'))
            {
                for (int retryCreateDir = 0; retryCreateDir < 3; retryCreateDir++)
                {
                    string destFilePath = file.Substring(0, file.LastIndexOf('\\'));
                    try
                    {
                        if (!Directory.Exists(destination + destFilePath))
                        {
                            Directory.CreateDirectory(destination + destFilePath);
                            Task.Delay(100);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.Log("CopyFiles@dir - " + ex, Logging.LogType.Error);

                        if (FRMRESULTS != null)
                            FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }

            //copy file
            for (int retryCopy = 0; retryCopy < 3; retryCopy++)
            {
                try
                {
                    File.Copy(SOURCEFILE, DESTFILE, overwriteFile);
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCopy == 2)
                    {
                        Logging.Log("CopyFiles@copy - " + ex, Logging.LogType.Error);

                        if (FRMRESULTS != null)
                            FRMRESULTS.ShowMessagebox("Error copying file " + file + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    Task.Delay(500);
                }
            }

            //verify destination file md5
            bool filecopysuccess = false;
            for (int retryMD5 = 0; retryMD5 < 3; retryMD5++)
            {
                try
                {
                    using (var hasher = MD5.Create())
                    {
                        using (var stream = File.OpenRead(DESTFILE))
                        {
                            byte[] hash = hasher.ComputeHash(stream);
                            if (md5 == BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant())
                                filecopysuccess = true;
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (retryMD5 == 2)
                    {
                        Logging.Log("CopyFiles@md5dest - " + ex, Logging.LogType.Error);
                        if (FRMRESULTS != null)
                            FRMRESULTS.ShowMessagebox("File " + file + " copied but failed to verify integrity.", "Error verifying file.", System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    Task.Delay(500);
                }
            }
            return true;
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
