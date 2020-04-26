using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace File_Tools.Modules
{
    public class Logging
    {
        const string LOGFILENAME = "Logs\\Log.log";
        private static Queue<Message> messageQueue = new Queue<Message>();
        System.Timers.Timer timer;
        private bool timerEnabled;

        public enum LogType
        {
            Info,
            Debug,
            Error,
            Warn
        }

        private class Message
        {
            public string message;
            public LogType type;

            public Message(string message_, LogType type_)
            {
                message = message_;
                type = type_;
            }
        }

        public static void Log(string message, LogType type = LogType.Info)
        {
            message = DateTime.Now.ToString("dd/mm/yyyy hh:MM:ss - ") + message;
            Message msg = new Message(message, type);
            messageQueue.Enqueue(msg);
        }

        public void start()
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            if (File.Exists(LOGFILENAME))
            {
                File.Move(LOGFILENAME, "Logs\\Log(" + DateTime.Now.ToString("yyyymmddhhMMss") + ").log");
                Thread.Sleep(1000);
            }

            timerEnabled = true;

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void stop()
        {
            timerEnabled = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                while (messageQueue.Count > 0)
                {
                    string prefix = " ";
                    Message msg = messageQueue.Dequeue();
                    switch (msg.type)
                    {
                        case LogType.Debug:
                            prefix = "D";
                            break;
                        case LogType.Error:
                            prefix = "E";
                            break;
                    }
                    File.AppendAllText(LOGFILENAME, prefix + "-" + msg.message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {

            }
            if (timerEnabled)
            {
                timer.Start();
            }
        }
    }
}
