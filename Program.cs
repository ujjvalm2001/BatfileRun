using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string batFilePath = @"C:\HDFC\PGPUtility\PGPUtility.bat";
        string logFolderPath = @"E:\@Project\consoleAppForBatRun";

        try
        {
            if (!File.Exists(batFilePath))
            {
                Console.WriteLine("Batch file not found: " + batFilePath);
                return;
            }

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            string logFilePath = Path.Combine(
                logFolderPath,
                "BatExecutionLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"
            );

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c \"" + batFilePath + "\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(batFilePath)
            };

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine("=====================================");
                    sw.WriteLine("Execution Time : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    sw.WriteLine("BAT File       : " + batFilePath);
                    sw.WriteLine("Exit Code      : " + process.ExitCode);
                    sw.WriteLine("-------------------------------------");
                    sw.WriteLine("OUTPUT:");
                    sw.WriteLine(string.IsNullOrWhiteSpace(output) ? "No output generated." : output);

                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        sw.WriteLine("-------------------------------------");
                        sw.WriteLine("ERROR:");
                        sw.WriteLine(error);
                    }

                    sw.WriteLine("=====================================");
                    sw.WriteLine();
                }

                Console.WriteLine("Batch file executed successfully.");
                Console.WriteLine("Log saved at: " + logFilePath);
            }
        }
        catch (Exception ex)
        {
            try
            {
                if (!Directory.Exists(logFolderPath))
                {
                    Directory.CreateDirectory(logFolderPath);
                }

                string errorLogFilePath = Path.Combine(
                    logFolderPath,
                    "ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"
                );

                File.AppendAllText(
                    errorLogFilePath,
                    "=====================================" + Environment.NewLine +
                    "Error Time : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine +
                    "Message    : " + ex.Message + Environment.NewLine +
                    "StackTrace : " + ex.StackTrace + Environment.NewLine +
                    "=====================================" + Environment.NewLine + Environment.NewLine
                );
            }
            catch
            {
            }

            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadLine();
    }
}