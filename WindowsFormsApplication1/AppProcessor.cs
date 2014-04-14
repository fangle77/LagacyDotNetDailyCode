using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace WebServerManager
{
    public class AppProcessor
    {
        public static int GetPortByProcessID(int processID)
        {
            int port = -1;

            Process pro = new Process();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;

            pro.Start();

            pro.StandardInput.WriteLine("netstat -ano");
            pro.StandardInput.WriteLine("exit");

            Regex reg = new Regex(@"\s{1,}", RegexOptions.Compiled);
            string line = null;
            while ((line = pro.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");
                    string[] arr = line.Split(',');
                    if (arr[4].Trim() == processID.ToString())
                    {
                        port = Int32.Parse(arr[1].Split(':')[1]);
                        pro.StandardOutput.ReadToEnd();
                    }
                }
            }
            pro.Close();
            return port;
        }

        public static Process GetProcessByNameAndPort(string processName, int port)
        {
            Process process = null;
            var pros = Process.GetProcessesByName(processName);
            foreach (var p in pros)
            {
                if (port == GetPortByProcessID(p.Id))
                {
                    process = p;
                    break;
                }
            }

            return process;
        }

        public static bool CloseProcessByNameAndPort(string processName, int port)
        {
            var p = GetProcessByNameAndPort(processName, port);
            if (p != null)
            {
                try
                {
                    p.Kill();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static Process StartNewProcess(string processFile, string argument)
        {
            Process p = new Process();
            p.StartInfo.FileName = processFile;
            p.StartInfo.Arguments = argument;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            //p.StandardInput.WriteLine(BuildCmd(site));
            return p;
        }

        public static string StartNewCMDProcess(string processFile, string argument)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.StandardInput.WriteLine(processFile + argument + "\r\n");
            p.StandardInput.WriteLine("exit");
            string results = p.StandardOutput.ReadToEnd();
            p.Close();
            return results;
        }
    }
}
