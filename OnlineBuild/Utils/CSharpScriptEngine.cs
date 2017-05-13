using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.IO;

namespace OnlineBuild.Utils
{
    public class CSharpScriptEngine
    {
        public static object Execute(string code)
        {
            ScriptOptions scriptOptions = ScriptOptions.Default;
            var mscorlib = typeof(System.Object).GetTypeInfo().Assembly;
            var systemCore = typeof(System.Linq.Enumerable).GetTypeInfo().Assembly;
            scriptOptions = scriptOptions.AddReferences(mscorlib, systemCore);
            scriptOptions = scriptOptions.AddImports("System");
            scriptOptions = scriptOptions.AddImports("System.Linq");
            scriptOptions = scriptOptions.AddImports("System.Collections.Generic");


            Script script = CSharpScript.Create(code, scriptOptions);
            var endState = script.RunAsync().Result;
            return endState.ReturnValue;
        }
    }
}
