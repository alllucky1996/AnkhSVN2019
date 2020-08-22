using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.Yui.Compressor;

namespace CShap2JSAnalysis
{
    public static class MiniFile
    {
        public static string MinJavascript(this string source, string template)
        {
            try
            {
                var jsCompress = new JavaScriptCompressor();
                jsCompress.Encoding = Encoding.UTF8;
                jsCompress.ThreadCulture = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                jsCompress.IgnoreEval = true;
                string data = jsCompress.Compress(source);
                StringBuilder builderData = new StringBuilder(template);
                builderData.AppendLine(data);
                return builderData.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
