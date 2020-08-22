using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CShap2JSAnalysis
{
    public /*internal*/ class Gennertor
    {
        public string content;
        public AnaniticsResult Result { get; private set; }

        public Gennertor(string content)
        {
            try
            {
                Result = new AnaniticsResult();
                this.content = content;
                SyntaxTree tree = CSharpSyntaxTree.ParseText(content);
                var root = (CompilationUnitSyntax)tree.GetRoot();
                var firstMember = root.Members;
                if (firstMember.Count() != 1)
                    throw new ArgumentOutOfRangeException("nameSpace", " only support 'one' nameSpace in parser code ");
                var nameSpace = (NamespaceDeclarationSyntax)firstMember.FirstOrDefault();
                var rightValue = (nameSpace.Name as QualifiedNameSyntax).Right.Identifier.ValueText;
                var leftSynctax = (nameSpace.Name as QualifiedNameSyntax).Left;
                while (leftSynctax is QualifiedNameSyntax)
                {
                    var tmp = (leftSynctax as QualifiedNameSyntax).Right.Identifier.ValueText;
                    rightValue = $"{tmp}.{rightValue}";
                    leftSynctax = (leftSynctax as QualifiedNameSyntax).Left;
                }
                Result.SpaceName = $"{(leftSynctax as IdentifierNameSyntax).Identifier.ValueText}.{rightValue}";

                var classNames = nameSpace.Members;
                if (classNames.Count() != 1)
                    throw new ArgumentOutOfRangeException("_className", " only support 'one' class in parser code ");
                Result.ClassName = (classNames.FirstOrDefault() as ClassDeclarationSyntax).Identifier.ValueText;

                //result.Methods = (classNames[0] as ClassDeclarationSyntax).Members.Select(s=>s as PropertyDeclarationSyntax).Select(s => s.Identifier.ValueText);
                var contructors = (classNames[0] as ClassDeclarationSyntax).Members.Where(e => e.Kind() == SyntaxKind.ConstructorDeclaration);
                foreach (var ctt in contructors)
                {
                    var c = ctt as ConstructorDeclarationSyntax;
                    if (c.ParameterList.Parameters.Count == 0)
                        Result.Contructors.Add(new KeyValuePair<string, List<string>>(c.Identifier.ValueText, new List<string>()));
                    else
                    {
                        var args = c.ParameterList.Parameters.Select(s => s.Identifier.ValueText);
                        Result.Contructors.Add(new KeyValuePair<string, List<string>>(c.Identifier.ValueText, new List<string>(args)));
                    }
                }
                var methods = (classNames[0] as ClassDeclarationSyntax).Members.Where(e => e.Kind() == SyntaxKind.PropertyDeclaration);
                foreach (var mt in methods)
                {
                    var m = mt as PropertyDeclarationSyntax;
                    Result.Properties.Add(new KeyValuePair<string, string>(m.Identifier.ValueText, m.Type.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    public class AnaniticsResult
    {
        /// <summary>
        /// namespage of class
        /// </summary>
        public string SpaceName { get; set; }
        /// <summary>
        /// name of class
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// List property using Key is Name, Value is type
        /// </summary>
        public List<System.Collections.Generic.KeyValuePair<string, string>> Properties { get; set; }
        /// <summary>
        /// List Contructor using key is name, value is List[params]
        /// </summary>
        public List<KeyValuePair<string, List<string>>> Contructors { get; set; }
        public AnaniticsResult()
        {
            Contructors = new List<KeyValuePair<string, List<string>>>();
            Properties = new List<KeyValuePair<string, string>>();
        }
    }
}