using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using GennerateScript.Helper;
using GennericJavaScript;
using Microsoft;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Task = System.Threading.Tasks.Task;

namespace GennerateScript
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class Cmd_GenScriptObject
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("90a7f769-8490-4e78-9db6-d1ff64f2c7c8");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmd_GenScriptObject"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private Cmd_GenScriptObject(AsyncPackage package, OleMenuCommandService commandService)
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
        public static Cmd_GenScriptObject Instance
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
            // Switch to the main thread - the call to AddCommand in Cmd_GenScriptObject's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Cmd_GenScriptObject(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void  Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            TextViewSelection selection = GetSelection(package);
            string activeDocumentPath =  GetActiveFilePath(package);
            
            string title = "Result gennerate code";
            var methods = new List<string>();
            string data = selection.Text;
            if (string.IsNullOrEmpty(selection.Text))
            {
                data = File.ReadAllText(activeDocumentPath);
            }
            //var ann = (new GennertorJS(data)).CShapeAnalysis();
            //var frm = new FrmSelectProperties(ann);
            //frm.ShowDialog();

            // Show a message box to prove we were here
            //message = $"name space: {ann.SpaceName}; \r\n class: {ann.ClassName} \r\n methods:{string.Join("\r\n ", ann.Methods)}." ;
            string message = "nội dung file: " + activeDocumentPath;
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            //TODO: lấy ra được các thuộc tính của class rồi còn save sang js nữa
        }
        private string  GetActiveFilePath(Microsoft.VisualStudio.Shell.IAsyncServiceProvider serviceProvider)
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
    
        private IEnumerable<string> _getProperties(AnaniticsResult ananiticsResult)
        {
            var result = new List<string>();


            return result;
        }
    }
}
