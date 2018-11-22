using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{

    /// <summary>
    /// 进程处理工具类
    /// 提供命令行执行程序
    /// </summary>
    public class ProcessUtility
    {
        public string ExeCmd(string[] cmds)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string strOutput = null;
            foreach(string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);

            }

            strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return strOutput;
        }
    }
}
