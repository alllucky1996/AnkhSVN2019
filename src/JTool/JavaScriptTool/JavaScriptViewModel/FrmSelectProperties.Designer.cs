namespace JavaScriptTool.JavaScriptViewModel
{
    partial class FrmSelectProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ckAll = new System.Windows.Forms.CheckBox();
            this.cksProperties = new System.Windows.Forms.CheckedListBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lbNameSpace = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.btnProject = new System.Windows.Forms.Button();
            this.ckMinify = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(246, 148);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Bỏ qua Constructor";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ckAll
            // 
            this.ckAll.AutoSize = true;
            this.ckAll.Checked = true;
            this.ckAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAll.Location = new System.Drawing.Point(147, 150);
            this.ckAll.Name = "ckAll";
            this.ckAll.Size = new System.Drawing.Size(81, 17);
            this.ckAll.TabIndex = 17;
            this.ckAll.Text = "Chọn tất cả";
            this.ckAll.UseVisualStyleBackColor = true;
            this.ckAll.CheckedChanged += new System.EventHandler(this.ckAll_CheckedChanged);
            // 
            // cksProperties
            // 
            this.cksProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cksProperties.FormattingEnabled = true;
            this.cksProperties.Location = new System.Drawing.Point(37, 189);
            this.cksProperties.Name = "cksProperties";
            this.cksProperties.ScrollAlwaysVisible = true;
            this.cksProperties.Size = new System.Drawing.Size(343, 124);
            this.cksProperties.TabIndex = 15;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(120, 112);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(231, 20);
            this.txtClass.TabIndex = 13;
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(120, 80);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(231, 20);
            this.txtNameSpace.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "properties: ";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(34, 112);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(60, 13);
            this.lblClass.TabIndex = 11;
            this.lblClass.Text = "class name";
            // 
            // lbNameSpace
            // 
            this.lbNameSpace.AutoSize = true;
            this.lbNameSpace.Location = new System.Drawing.Point(34, 80);
            this.lbNameSpace.Name = "lbNameSpace";
            this.lbNameSpace.Size = new System.Drawing.Size(65, 13);
            this.lbNameSpace.TabIndex = 12;
            this.lbNameSpace.Text = "name space";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(286, 399);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(166, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "Chọn các thuộc tính cần tạo trong view";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "target";
            // 
            // cbProject
            // 
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(120, 333);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(213, 21);
            this.cbProject.TabIndex = 18;
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(352, 333);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(28, 21);
            this.btnProject.TabIndex = 19;
            this.btnProject.Text = "...";
            this.btnProject.UseVisualStyleBackColor = true;
            // 
            // ckMinify
            // 
            this.ckMinify.AutoSize = true;
            this.ckMinify.Checked = true;
            this.ckMinify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckMinify.Location = new System.Drawing.Point(37, 370);
            this.ckMinify.Name = "ckMinify";
            this.ckMinify.Size = new System.Drawing.Size(114, 17);
            this.ckMinify.TabIndex = 17;
            this.ckMinify.Text = "Sử dụng mini code";
            this.ckMinify.UseVisualStyleBackColor = true;
            this.ckMinify.CheckedChanged += new System.EventHandler(this.ckAll_CheckedChanged);
            // 
            // FrmSelectProperties
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(418, 461);
            this.ControlBox = false;
            this.Controls.Add(this.btnProject);
            this.Controls.Add(this.cbProject);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ckMinify);
            this.Controls.Add(this.ckAll);
            this.Controls.Add(this.cksProperties);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lbNameSpace);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Name = "FrmSelectProperties";
            this.Text = "jansoft JSC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox ckAll;
        private System.Windows.Forms.CheckedListBox cksProperties;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lbNameSpace;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.Button btnProject;
        private System.Windows.Forms.CheckBox ckMinify;
    }
}