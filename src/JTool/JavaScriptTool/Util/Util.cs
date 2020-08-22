using CShap2JSAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaScriptTool
{
    public static class Util
    {
        public static List<string> PropertiesSelected { get; set; }
        public static AnaniticsResult ResultSelected { get; set; }
        public static KeyValuePair<string,string> ProjectSelected { get; set; }
        public static bool IsMiniFy { get; set; }
    }
}
