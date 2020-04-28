using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File_Tools.Modules;

namespace File_Tools.Tasks
{
    class Fill
    {
        readonly string PATH1;
        readonly string PATH2;
        readonly frmResults FRMRESULTS;

        public Fill(frmResults frmResults, string path1, string path2)
        {
            PATH1 = path1;
            PATH2 = path2;
            FRMRESULTS = frmResults;

            //Task.Run(() => Job());
            Job();
        }

        private void Job()
        {
            DateTime startTime = DateTime.Now;
            Logging.Log("Fill task started at " + startTime.ToString("dd/MM/yyyy hh:mm:ss"), Logging.LogType.Debug);

            FileManager fileManager = new FileManager();

            //Get files
            FRMRESULTS.SetProgressBarMax(2);

            string[] filesInPath1 = fileManager.GenerateFileList(PATH1, true);
            Logging.Log(filesInPath1.Length + " files found in path1 after " + DateTime.Now.Subtract(startTime).TotalMilliseconds + "ms.", Logging.LogType.Debug);
            FRMRESULTS.UpdateStatus("Found " + filesInPath1.Length + " files found in " + PATH1, 1);

            string[] filesInPath2 = fileManager.GenerateFileList(PATH2, true);
            Logging.Log(filesInPath2.Length + " files found in path2 after " + DateTime.Now.Subtract(startTime).TotalMilliseconds + "ms.", Logging.LogType.Debug);
            FRMRESULTS.UpdateStatus("Found " + filesInPath2.Length + " files found in " + PATH2, 2);
            
            //perform the merge
            foreach (string file in filesInPath1)
            {
                if (!filesInPath2.Contains(file))
                {
                    FRMRESULTS.AddFileToList(file);
                }
            }

            //End of task
            DateTime endTime = DateTime.Now;
            Logging.Log("Fill task completed at " + endTime.ToString("dd/MM/yyyy hh:mm:ss") + " in " + endTime.Subtract(startTime).TotalMilliseconds + "ms.", Logging.LogType.Debug);
        }
    }
}
