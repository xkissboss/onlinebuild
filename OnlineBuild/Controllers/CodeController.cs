using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBuild.Common.Command;
using OnlineBuild.Utils;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Reflection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBuild.Controllers
{
    public class CodeController : BaseController
    {

        private ICommand cmd;

        private string aimPath;

        public CodeController(ICommand cmd, ILoggerFactory factory, IServiceProvider svp) : base(factory, svp)
        {
            this.cmd = cmd;
            aimPath = Path.Combine(StaticVariable.CODE_SAVE_PATH, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(aimPath))
                Directory.CreateDirectory(aimPath);
        }


        protected string GetNewPath()
        {
            string path = Path.Combine(aimPath, Guid.NewGuid().ToString("N"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;

        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Python");
        }



        [HttpPost]
        public APIReturn RunC(string code)
        {
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");
            string path = Path.Combine(GetNewPath(), $"{Guid.NewGuid().ToString("N")}.c");
            System.IO.File.WriteAllText(path, codeStr, Encoding.UTF8);
            CmResult cm = cmd.RunC(path);
            if (cm.Success)
                return APIReturn.BuildSuccess(cm.Message);
            return APIReturn.BuildFail(cm.ExecMsg);
        }

        public APIReturn RunCPlusPlus(string code)
        {

            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");

            string path = Path.Combine(GetNewPath(), $"{Guid.NewGuid().ToString("N")}.cpp");
            System.IO.File.WriteAllText(path, codeStr, Encoding.UTF8);
            CmResult cr = cmd.RunCPlusPlus(path);
            return cr.Success ? APIReturn.BuildSuccess(cr.Message) : APIReturn.BuildFail(cr.ExecMsg);
        }

        public APIReturn RunPython(string code)
        {
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");

            string path = Path.Combine(GetNewPath(), $"{Guid.NewGuid().ToString("N")}.py");


            System.IO.File.WriteAllText(path, codeStr, Encoding.UTF8);
            CmResult cr = cmd.RunPython(path);
            return cr.Success ? APIReturn.BuildSuccess(cr.Message) : APIReturn.BuildFail(cr.ExecMsg);
        }

        public APIReturn RunJava(string code)
        {

            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");

            string path = Path.Combine(GetNewPath(), "Test.java");
            //System.IO.File.WriteAllText(path, codeStr, Encoding.UTF8);
            using (StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.WriteLine(codeStr);
                sw.Flush();
            }
            CmResult cr = cmd.RunJava(path);
            return cr.Success ? APIReturn.BuildSuccess(cr.Message) : APIReturn.BuildFail(cr.ExecMsg);
        }

        public APIReturn RunCsharp(string code)
        {

            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");

            CmResult cr = cmd.RunCSharp(codeStr);

            return cr.Success ? APIReturn.BuildSuccess(cr.Message) : APIReturn.BuildFail(cr.ExecMsg);
        }


        public APIReturn RunNodejs(string code)
        {
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("code不能为空");

            string codeStr = code.Base64ToString();
            if (string.IsNullOrEmpty(code))
                APIReturn.BuildFail("无效的code");


            string path = Path.Combine(GetNewPath(), $"{Guid.NewGuid().ToString("N")}.js");
            System.IO.File.WriteAllText(path, codeStr, Encoding.UTF8);
            CmResult cr = cmd.RunNodejs(path);
            return cr.Success ? APIReturn.BuildSuccess(cr.Message) : APIReturn.BuildFail(cr.ExecMsg);
        }

        public IActionResult C()
        {
            return View();
        }

        public IActionResult Cpp()
        {
            return View();
        }

        public IActionResult Java()
        {
            return View();
        }

        public IActionResult Python()
        {
            return View();
        }

        public IActionResult Js()
        {
            return View();
        }

        public IActionResult Csharp()
        {
            return View();
        }

        public IActionResult Nodejs()
        {
            return View();
        }
    }
}
