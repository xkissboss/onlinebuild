using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Utils
{
    public class StaticVariable
    {

        private static string _CODE_SAVE_PATH;

        public static string CODE_SAVE_PATH
        {
            get
            {
                return _CODE_SAVE_PATH;
            }
        }

        public static void InitVariable(IConfigurationRoot configuration)
        {
            _CODE_SAVE_PATH = configuration.GetValue<string>("CodeSavePath");
        }
    }
}
