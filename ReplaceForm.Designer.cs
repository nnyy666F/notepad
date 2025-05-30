namespace Notepad
{
	partial class ReplaceForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtFindWhat = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtReplaceWith = new System.Windows.Forms.TextBox();
			this.btnFindNext = new System.Windows.Forms.Button();
			this.btnReplace = new System.Windows.Forms.Button();
			this.btnReplaceAll = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "查找内容(&N):";
			// 
			// txtFindWhat
			// 
			this.txtFindWhat.Location = new System.Drawing.Point(12, 36);
			this.txtFindWhat.Name = "txtFindWhat";
			this.txtFindWhat.Size = new System.Drawing.Size(240, 20);
			this.txtFindWhat.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "替换为(&P):";
			// 
			// txtReplaceWith
			// 
			this.txtReplaceWith.Location = new System.Drawing.Point(12, 79);
			this.txtReplaceWith.Name = "txtReplaceWith";
			this.txtReplaceWith.Size = new System.Drawing.Size(240, 20);
			this.txtReplaceWith.TabIndex = 3;
			// 
			// btnFindNext
			// 
			this.btnFindNext.Location = new System.Drawing.Point(258, 34);
			this.btnFindNext.Name = "btnFindNext";
			this.btnFindNext.Size = new System.Drawing.Size(105, 23);
			this.btnFindNext.TabIndex = 4;
			this.btnFindNext.Text = "查找下一个(&F)";
			this.btnFindNext.UseVisualStyleBackColor = true;
			this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
			// 
			// btnReplace
			// 
			this.btnReplace.Location = new System.Drawing.Point(258, 63);
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.Size = new System.Drawing.Size(105, 23);
			this.btnReplace.TabIndex = 5;
			this.btnReplace.Text = "替换(&R)";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
			// 
			// btnReplaceAll
			// 
			this.btnReplaceAll.Location = new System.Drawing.Point(258, 92);
			this.btnReplaceAll.Name = "btnReplaceAll";
			this.btnReplaceAll.Size = new System.Drawing.Size(105, 23);
			this.btnReplaceAll.TabIndex = 6;
			this.btnReplaceAll.Text = "全部替换(&A)";
			this.btnReplaceAll.UseVisualStyleBackColor = true;
			this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(258, 121);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(105, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new System.Drawing.Point(15, 108);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(83, 17);
			this.chkMatchCase.TabIndex = 8;
			this.chkMatchCase.Text = "区分大小写";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			// 
			// ReplaceForm
			// 
			this.AcceptButton = this.btnFindNext;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(375, 156);
			this.Controls.Add(this.chkMatchCase);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnReplaceAll);
			this.Controls.Add(this.btnReplace);
			this.Controls.Add(this.btnFindNext);
			this.Controls.Add(this.txtReplaceWith);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtFindWhat);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReplaceForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "替换";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFindWhat;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtReplaceWith;
		private System.Windows.Forms.Button btnFindNext;
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Button btnReplaceAll;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkMatchCase;
	}
}