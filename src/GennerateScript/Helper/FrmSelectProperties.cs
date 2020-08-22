using GennericJavaScript;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GennerateScript.Helper
{
    public partial class FrmSelectProperties : Form
    {
        public FrmSelectProperties(AnaniticsResult ananitics)
        {
            InitializeComponent();
            txtNameSpace.Text = ananitics.SpaceName;
            txtClass.Text = ananitics.ClassName;
            foreach (var item in ananitics.Properties)
            {
                cksProperties.Items.Add($"{item.Key} // {item.Value}", true);
            }
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
    }
}
