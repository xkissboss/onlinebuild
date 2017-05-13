using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Common.Command
{
    public class CmResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string ExecMsg { get; set; }

        public static CmResult BuildFail(string message, string execMsg)
        {
            CmResult cr = new CmResult() { Success = false, Message = message, ExecMsg = execMsg };
            return cr;
        }

        public static CmResult BuildSuccess(string message, string execMsg = "")
        {
            CmResult cr = new CmResult() { Success = true, Message = message, ExecMsg = execMsg };
            return cr;
        }
    }
}
