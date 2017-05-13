using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBuild.Utils
{
    public static class ET
    {
        public enum 指令类
        {
            GCC = 1,
            SH =  2
        }



        public static string GetExpandName(this ET.指令类 sc)
        {
            switch(sc)
            {
                case ET.指令类.GCC:
                    return "gcc";
                case ET.指令类.SH:
                    return "./";
                default:
                    return sc.ToString();
            }
        }
    }
}
