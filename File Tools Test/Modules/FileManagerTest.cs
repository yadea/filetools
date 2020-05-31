using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File_Tools.Modules;
using System.Text;

namespace File_Tools_Test.Modules
{
    [TestClass]
    public class FileManagerTest
    {
        static string baseFolder = Environment.CurrentDirectory;
        static string testFolder = baseFolder + "//Test Files//";
        static string sourceFolder = testFolder + "Source//";
        static string destFolder = testFolder + "Destination//";

        [TestInitialize]
        public void TestMethod1()
        {

            string destinationFolder = baseFolder + "//Test Files//Destination";
            //clean up folders
            Directory.Delete(destinationFolder, true);

            //Let it clean for a while
            Task.Delay(2000);

            //Recreate Destination folder
            while (Directory.Exists(destinationFolder)) { }
            Directory.CreateDirectory(destinationFolder);
        }

        [TestMethod]
        public void FileManager_CopyFiles_ShouldCopyFileToDestinationBaseFolder()
        {
            FileManager fm = new FileManager();
            string testFileName = "TestTextFile.txt";

            fm.CopyFiles(new String[] { testFileName }, sourceFolder, destFolder);

            Assert.IsTrue(File.ReadAllText(sourceFolder + testFileName).Equals(File.ReadAllText(destFolder + testFileName)));
        }

        [TestMethod]
        public void FileManager_CopyFiles_ShouldCopyLargeFileToDestinationBaseFolder()
        {
            FileManager fm = new FileManager();
            string testFileName = "1gb.zip";
            string sourceMD5 = null;
            string destMD5 = null;

            using (MD5 hasher = MD5.Create())
            {
                using (FileStream stream = File.Open(sourceFolder + testFileName, FileMode.Open))
                {
                    byte[] hash = hasher.ComputeHash(stream);
                    sourceMD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }

                fm.CopyFiles(new String[] { testFileName }, sourceFolder, destFolder);

                using (FileStream stream = File.Open(destFolder + testFileName, FileMode.Open))
                {
                    byte[] hash = hasher.ComputeHash(stream);
                    destMD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }

            //File.geth

            Assert.AreEqual(sourceMD5, destMD5);
        }

        [TestMethod]
        public void FileManager_CopyFiles_ShouldCopyNestedFilesToDestinationsNestedFolders()
        {
            FileManager fm = new FileManager();
            string testFileName = "Nested1\\Nested2\\testbitmap.bmp";

            fm.CopyFiles(new String[] { testFileName }, sourceFolder, destFolder);

            Assert.IsTrue(File.ReadAllText(sourceFolder + testFileName).Equals(File.ReadAllText(destFolder + testFileName)));
        }
        
        [TestMethod]
        public void FileManager_CopyFiles_ShouldOverwriteFileIfFileExists()
        {
            FileManager fm = new FileManager();
            string testFileName = "TestTextFile.txt";
            File.WriteAllText(Path.Combine(destFolder, testFileName), "Incorrect file");

            fm.CopyFile(testFileName,sourceFolder, destFolder, true);

            Assert.IsTrue(File.ReadAllText(sourceFolder + testFileName).Equals(File.ReadAllText(destFolder + testFileName)));
        }

        string ConvertBytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x"));
            }
            return sb.ToString();
        }

    }
}
