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
        public void CopyFiles(string[] files, string source, string destination, bool overwriteFile = false)
        {
            foreach (string file in files)
            {
                CopyFile(file, source, destination, overwriteFile);
            }
        }

        public bool CopyFile(string file, string source, string destination, bool overwriteFile=false)
        {
            //generate files md5
            string md5 = null;
            string SOURCEFILE = Path.Combine(source, file);
            string DESTFILE = Path.Combine(destination, file);

            //generate MD5
            md5 = GenerateMD5(SOURCEFILE);

            if (md5 == null)
                return false;

            //check directory tree 
            GenerateNestedDirectory(file, destination);

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
            string destMd5 = GenerateMD5(DESTFILE);
            if (destMd5 == md5)
                return true;
            else
                return false;
        }
        
        private string GenerateMD5(string filePath)
        {
            string md5 = null;
            for (int retryMD5 = 0; retryMD5 < 3; retryMD5++)
            {
                try
                {
                    using (MD5 hasher = MD5.Create())
                    {
                        using (FileStream stream = File.OpenRead(filePath))
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
                            FRMRESULTS.ShowMessagebox("Error copying file " + filePath + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    Task.Delay(500);
                }
            }
            return md5;
        }

        private bool GenerateNestedDirectory(string filePath, string destination)
        {
            if (filePath.Contains('\\'))
            {
                for (int retryCreateDir = 0; retryCreateDir < 3; retryCreateDir++)
                {
                    string destFilePath = filePath.Substring(0, filePath.LastIndexOf('\\'));
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
                            FRMRESULTS.ShowMessagebox("Error copying file " + filePath + ". Please ensure you close any programs reading that file and try again.", "Error copying file.", System.Windows.Forms.MessageBoxIcon.Error);

                        return false;
                    }
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
