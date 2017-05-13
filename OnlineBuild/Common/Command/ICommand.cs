using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Common.Command
{
    public interface ICommand
    {
        CmResult RunC(string path);

        CmResult RunCPlusPlus(string path);

        CmResult RunJava(string path);

        CmResult RunPython(string path);

        CmResult RunNetCore(string path);

        CmResult RunCSharp(string path);

        CmResult RunNodejs(string path);

        //CmResult BuildC(string path);

        //CmResult BuildCPlusPlus(string path);
    }
}
