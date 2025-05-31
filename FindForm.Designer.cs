namespace Notepad
{
	partial class FindForm
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
			this.btnFindNext = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.chkSearchUp = new System.Windows.Forms.CheckBox();
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
			// btnFindNext
			// 
			this.btnFindNext.Location = new System.Drawing.Point(258, 34);
			this.btnFindNext.Name = "btnFindNext";
			this.btnFindNext.Size = new System.Drawing.Size(75, 23);
			this.btnFindNext.TabIndex = 2;
			this.btnFindNext.Text = "下一个";
			this.btnFindNext.UseVisualStyleBackColor = true;
			this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(258, 63);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new System.Drawing.Point(15, 65);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(83, 17);
			this.chkMatchCase.TabIndex = 4;
			this.chkMatchCase.Text = "大小写";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			// 
			// chkSearchUp
			// 
			this.chkSearchUp.AutoSize = true;
			this.chkSearchUp.Location = new System.Drawing.Point(15, 88);
			this.chkSearchUp.Name = "chkSearchUp";
			this.chkSearchUp.Size = new System.Drawing.Size(79, 17);
			this.chkSearchUp.TabIndex = 5;
			this.chkSearchUp.Text = "上一个";
			this.chkSearchUp.UseVisualStyleBackColor = true;
			// 
			// FindForm
			// 
			this.AcceptButton = this.btnFindNext;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(345, 122);
			this.Controls.Add(this.chkSearchUp);
			this.Controls.Add(this.chkMatchCase);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnFindNext);
			this.Controls.Add(this.txtFindWhat);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "查找";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFindWhat;
		private System.Windows.Forms.Button btnFindNext;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkMatchCase;
		private System.Windows.Forms.CheckBox chkSearchUp;
	}
}