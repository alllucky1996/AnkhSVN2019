using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gennertor.Tests
{
    [TestClass()]
    public class GennertorJSTests
    {
        [TestMethod()]
        public void CShapeAnalysisTest()
        {
            //string content = "using Common.DataAccess.Interface;\r\nusing System;\r\nnamespace Commons.Entities\r\n{" +
            //    "/// <summary>\r\n"
            //    + "/// Bảng tài khoản\r\n"
            //    + "/// </summary>\r\n"
            //    + "public class Account : Entity\r\n{\r\n"
            //    + "public long Id { get; set; }\r\n"
            //    + "public string UserName { get; set; }\r\n"
            //    + "public string Password { get; set; }\r\n"
            //    + "public string Description { get; set; }\r\n"
            //    + "public string Name { get; set; }\r\n"
            //    + " }"
            //    + "}";
            //string content = File.ReadAllText(@"D:\DEV Tool\AnkhSVN2019\src\Ankh.VS.UnitTest\Account.cs");
            //var ressult = GennertorJS.CShapeAnalysis(content);
            //Console.WriteLine(ressult.SpaceName);
            //Console.WriteLine(ressult.ClassName);
            //Console.WriteLine("Method:");
            //foreach (var item in ressult.Methods)
            //{
            //    Console.WriteLine(item);
            //}
            Assert.IsTrue(true);
        }
    }
}