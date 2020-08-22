using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CShap2JSAnalysis;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Task = System.Threading.Tasks.Task;

namespace JavaScriptTool.JavaScriptViewModel
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CmdCreateViewModel
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("4a8e233f-9fae-466e-8234-7bd5cc0e0483");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdCreateViewModel"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CmdCreateViewModel(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CmdCreateViewModel Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CmdCreateViewModel's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CmdCreateViewModel(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);

            //string content = File.ReadAllText(@"D:\DEV Tool\AnkhSVN2019\src\Ankh.VS.UnitTest\Account.txt");
            //var ressult = new Gennertor(content).Result;
            TextViewSelection selection = GetSelection(package);
            string activeDocumentPath = GetActiveFilePath(package);

            string title = "Result gennerate code";
            var methods = new List<string>();
            string data = selection.Text;
            if (string.IsNullOrEmpty(selection.Text))
            {
                data = File.ReadAllText(activeDocumentPath);
            }
            var ann = (new Gennertor(data)).Result;
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            var prs = getProject(package, out string nameSolution);
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
            var frm = new FrmSelectProperties(ann, prs);
            frm.ShowDialog();

            // ghi file
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            _genFiileAsync(Util.ResultSelected, _getTemplatate(), Path.Combine(Util.ProjectSelected.Value,"wwwroot","js","model"), nameSolution, Util.IsMiniFy).Wait();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits


            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                string.Join("\n ", Util.ResultSelected.Properties.Select(s => s.Key)),
                //title,
                $"{Util.ProjectSelected.Key}--{Util.ProjectSelected.Value}",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
        private string GetActiveFilePath(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            try
            {

#pragma warning disable VSSDK006 // Check services exist
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
                var service = serviceProvider.GetServiceAsync(typeof(DTE)).Result as _DTE;
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
#pragma warning restore VSSDK006 // Check services exist
                return service.ActiveDocument.FullName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            //            var applicationObject = serviceProvider.GetServiceAsync(typeof(DTE)).Result;
            //#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
            //            if (applicationObject != null)
            //                return (applicationObject as EnvDTE80.DTE2).ActiveDocument.FullName;



        }
        private TextViewSelection GetSelection(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof(SVsTextManager));
            var textManager = service as IVsTextManager2;
            IVsTextView view;
            int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out view);

            view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn);//end could be before beginning
            var start = new TextViewPosition(startLine, startColumn);
            var end = new TextViewPosition(endLine, endColumn);

            view.GetSelectedText(out string selectedText);

            TextViewSelection selection = new TextViewSelection(start, end, selectedText);
            return selection;
        }

        private async Task _genFiileAsync(AnaniticsResult ananiticsResult, string template, string folder, string solutionName= "", bool minify = true)
        {
            // class
            StringBuilder cmtClass = new StringBuilder();
            cmtClass.AppendLine("/**");
            cmtClass.AppendLine("*");
            foreach (var item in ananiticsResult.Properties)
            {
                cmtClass.AppendLine("* @param {" + item.Value + "} " + item.Value + $" {ananiticsResult.ClassName}");
            }
            cmtClass.AppendLine("*");
            cmtClass.Append("* **/");
            // properties
            StringBuilder propertiesBuilder = new StringBuilder();

            foreach (var item in ananiticsResult.Properties)
            {
                propertiesBuilder.AppendLine($"        this.{item.Key}    ; // {item.Value}");
            }

            StringBuilder builder = new StringBuilder(template);
            builder.Replace("[$className]", ananiticsResult.ClassName);
            builder.Replace("[$commentClass]", $"{ananiticsResult.SpaceName}.{ananiticsResult.ClassName}");
            builder.Replace("    /*[$commentContructore]*/", cmtClass.ToString());
            builder.Replace("        /**[$properties]**/", propertiesBuilder.ToString());
            builder.Replace("[$solution]", string.IsNullOrEmpty(solutionName)?"solution": solutionName);
            // create filder if not Exists
            string folderPath = Path.Combine(folder, ananiticsResult.SpaceName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (StreamWriter sw = new StreamWriter(folderPath + $"/{ananiticsResult.ClassName}.js"))
            {
                await sw.WriteAsync(builder.ToString());
            }

            //// template.Replace("/**[$properties]**/", $"{ananiticsResult.SpaceName}.{ananiticsResult.ClassName}");
            //if (minify)
            //{
            //    string author = File.ReadAllText(@"JavaScriptViewModel\Resources\template.janauthor");
            //    var minData = builder.ToString().MinJavascript(author);
            //    using (StreamWriter ssw = new StreamWriter(folderPath + $"/{ananiticsResult.ClassName}.min.js"))
            //    {
            //        await ssw.WriteAsync(minData);
            //    }
            //}
           

        }
        private string _getTemplatate(string fileName = @"JavaScriptViewModel\Resources\ViewModeJS.janTemplate")
        {
            return File.ReadAllText(fileName);
        }
        private List<KeyValuePair<string,string>> getProject(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider, out string solutionName)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var result = new List<KeyValuePair<string, string>>();

#pragma warning disable VSSDK006 // Check services exist
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            var dte = serviceProvider.GetServiceAsync(typeof(DTE)).Result as DTE;
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
#pragma warning restore VSSDK006 // Check services exist
#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread
            solutionName = Path.GetFileNameWithoutExtension(dte.Solution.FileName);
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread

            var ps = dte.Solution.Projects;
            //foreach (var item in ps.GetEnumerator())
            //{
            //    var folderSource = item.ProjectItems;
            //    result.Add(new KeyValuePair<string, string>($"{sName}-{item.Name}", item.Name));
            //}
            List<Project> list = new List<Project>();
            list.AddRange(ps.Cast<Project>());
#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread
            result.AddRange(ps.Cast<Project>().Select(s => new KeyValuePair<string, string>($"{s.Name}", Path.GetDirectoryName(s.FullName))));
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread

            return result;
        }
    }
}
