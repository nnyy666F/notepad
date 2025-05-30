namespace Notepad
{
	partial class GoToForm
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
			this.txtLineNumber = new System.Windows.Forms.TextBox();
			this.btnGoTo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "行号(&L):";
			// 
			// txtLineNumber
			// 
			this.txtLineNumber.Location = new System.Drawing.Point(12, 36);
			this.txtLineNumber.Name = "txtLineNumber";
			this.txtLineNumber.Size = new System.Drawing.Size(240, 20);
			this.txtLineNumber.TabIndex = 1;
			this.txtLineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineNumber_KeyPress);
			// 
			// btnGoTo
			// 
			this.btnGoTo.Location = new System.Drawing.Point(96, 72);
			this.btnGoTo.Name = "btnGoTo";
			this.btnGoTo.Size = new System.Drawing.Size(75, 23);
			this.btnGoTo.TabIndex = 2;
			this.btnGoTo.Text = "转到(&G)";
			this.btnGoTo.UseVisualStyleBackColor = true;
			this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(177, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// GoToForm
			// 
			this.AcceptButton = this.btnGoTo;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(264, 107);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnGoTo);
			this.Controls.Add(this.txtLineNumber);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GoToForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "转到";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLineNumber;
		private System.Windows.Forms.Button btnGoTo;
		private System.Windows.Forms.Button btnCancel;
	}
}