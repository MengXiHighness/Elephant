using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{
    public class CmdHelper
    {
        private Process pro = null;

        public CmdHelper()
        {
            if (pro == null)
            {
                pro = new Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.CreateNoWindow = true;
            }
        }
        public string Input(string cmdStr)
        {
            if (pro == null)
            {
                pro = new Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.CreateNoWindow = true;
            }
            pro.Start();
            pro.StandardInput.WriteLine(cmdStr);
            pro.StandardInput.WriteLine("exit");
            string strRst = pro.StandardOutput.ReadToEnd();
            return strRst;
        }
    }
}
