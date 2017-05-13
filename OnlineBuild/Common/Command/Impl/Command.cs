using Microsoft.Extensions.Logging;
using OnlineBuild.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Common.Command.Impl
{
    public class Command : ICommand
    {

        private CmResult BuildCmm(string cmd, int waitMils = 3000)
        {
            string strOutput = null;
            bool success = false;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("sh");
                psi.UseShellExecute = false;
                psi.RedirectStandardError = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;
                using (Process proCompiler = new Process())
                {
                    proCompiler.StartInfo = psi;
                    proCompiler.Start();
                    proCompiler.StandardInput.WriteLine(cmd);

                    proCompiler.StandardInput.Flush();
                    proCompiler.StandardInput.Dispose();

                    //  是否有错误信息
                    strOutput = proCompiler.StandardError.ReadToEnd();
                    if (string.IsNullOrEmpty(strOutput))
                    {
                        strOutput = proCompiler.StandardOutput.ReadToEnd();
                        success = true;
                    }
                    proCompiler.WaitForExit(waitMils);
                }
                return success ? CmResult.BuildSuccess(string.IsNullOrEmpty(strOutput) ? "执行成功" : strOutput) : CmResult.BuildFail("执行失败", strOutput);
            }
            catch (Exception ex)
            {
                return CmResult.BuildFail("执行失败", ex.Message);
            }
        }



        private CmResult BuildC(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            string dir = FileHelper.GetDir(path);
            string guid = Guid.NewGuid().ToString("N");
            string runsh = Path.Combine(dir, guid);
            CmResult cr = BuildCmm($"gcc {path} -o {runsh}");
            if (cr.Success)
                return CmResult.BuildSuccess(Path.Combine(dir, $"./{guid}"));
            return cr;
        }


        private CmResult BuildCPlusPlus(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            string dir = FileHelper.GetDir(path);
            string guid = Guid.NewGuid().ToString("N");
            string runsh = Path.Combine(dir, guid);
            CmResult cr = BuildCmm($"g++ {path} -o {runsh}");
            if (cr.Success)
                return CmResult.BuildSuccess(Path.Combine(dir, $"./{guid}"));
            return cr;
        }

        private CmResult BuildJava(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            //string dir = FileHelper.GetDir(path);
            CmResult cr = BuildCmm($"javac -encoding utf8 {path}");
            if (!cr.Success)
                return cr;
            string runName = path.Substring(0, path.LastIndexOf(".java"));
            return CmResult.BuildSuccess(runName);
        }


        public CmResult RunC(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");

            CmResult cr = BuildC(path);
            if (!cr.Success)
                return cr;

            if (!FileHelper.IsExists(cr.Message))
                return CmResult.BuildFail("", "编译文件不存在");


            return BuildCmm($"{cr.Message}");
        }

        public CmResult RunCPlusPlus(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");

            CmResult cr = BuildCPlusPlus(path);
            if (!cr.Success)
                return cr;

            if (!FileHelper.IsExists(cr.Message))
                return CmResult.BuildFail("", "文件不存在1");


            return BuildCmm($"{cr.Message}");
        }

        public CmResult RunNetCore(string path)
        {
            throw new NotImplementedException();
        }

        public CmResult RunJava(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".java"))
                return CmResult.BuildFail("", "文件类型不正确");
            CmResult cr = BuildJava(path);
            if (!cr.Success)
                return cr;
            if (!FileHelper.IsExists($"{cr.Message}.class"))
                return CmResult.BuildFail("", "文件不存在1");
            string classPath = FileHelper.GetDir(path);

            string className = FileHelper.GetName(path).Replace(".java", "");
            return BuildCmm($"java -classpath {classPath} -Dfile.encoding=utf8 {className}");
        }

        public CmResult RunCSharp(string path)
        {
            try
            {
                object result = CSharpScriptEngine.Execute(path + "return Test.Main();");
                if (result == null)
                    return CmResult.BuildSuccess("执行成功：没有返回值");
                return CmResult.BuildSuccess(string.IsNullOrEmpty(result.ToString()) ? "执行成功" : result.ToString());
            }
            catch(Exception ex)
            {
                return CmResult.BuildFail("执行失败", ex.StackTrace);
            }

        }

        public CmResult RunPython(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".py"))
                return CmResult.BuildFail("", "文件类型不正确");
            return BuildCmm($"python {path}");
        }


        public CmResult RunNodejs(string path)
        {
            if (!FileHelper.IsExists(path))
                return CmResult.BuildFail("", "文件不存在");
            if (!path.EndsWith(".js"))
                return CmResult.BuildFail("", "文件类型不正确");
            return BuildCmm($"node {path}");
        }
    }
}
