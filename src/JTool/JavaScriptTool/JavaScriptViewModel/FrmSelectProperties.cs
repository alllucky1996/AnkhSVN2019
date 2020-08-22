using CShap2JSAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace JavaScriptTool.JavaScriptViewModel
{
    public partial class FrmSelectProperties : Form
    {
        public FrmSelectProperties(AnaniticsResult ananitics, List<KeyValuePair<string,string>> projectsNameValue)
        {
            InitializeComponent();
            txtNameSpace.Text = ananitics.SpaceName;
            txtClass.Text = ananitics.ClassName;
            Util.ResultSelected = ananitics;
            foreach (var item in ananitics.Properties)
            {
                cksProperties.Items.Add(item.Key, true);
            }

            cbProject.DataSource = projectsNameValue;
            cbProject.DisplayMember = "Key";
            cbProject.ValueMember = "Value";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAll.Checked)
            {
                for (int i = 0; i < cksProperties.Items.Count; i++)
                {
                    cksProperties.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < cksProperties.Items.Count; i++)
                {
                    cksProperties.SetItemChecked(i, false);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Util.PropertiesSelected = new List<string>();
            Util.ResultSelected.ClassName = txtClass.Text;
            Util.ResultSelected.SpaceName = txtNameSpace.Text;
            Util.ResultSelected.Properties = Util.ResultSelected.Properties.Where(w => cksProperties.CheckedItems.Contains(w.Key)).ToList();
            Util.ProjectSelected = new KeyValuePair<string, string>((cbProject.SelectedItem as dynamic).Key, (cbProject.SelectedItem as dynamic).Value);
            Util.IsMiniFy = ckMinify.Checked;
            this.Close();
        }
    }
}
