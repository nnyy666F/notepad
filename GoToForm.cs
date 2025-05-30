using System;
using System.Windows.Forms;

namespace Notepad
{
	public partial class GoToForm : Form
	{
		private RichTextBox textBox;
		private int maxLineNumber;

		public GoToForm(RichTextBox textBox)
		{
			InitializeComponent();
			this.textBox = textBox;
			maxLineNumber = textBox.Lines.Length;
			this.Text = "转到";
		}

		private void btnGoTo_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtLineNumber.Text, out int lineNumber))
			{
				if (lineNumber >= 1 && lineNumber <= maxLineNumber)
				{
					int charIndex = textBox.GetFirstCharIndexFromLine(lineNumber - 1);
					if (charIndex >= 0)
					{
						textBox.SelectionStart = charIndex;
						textBox.SelectionLength = 0;
						textBox.ScrollToCaret();
						this.Close();
					}
				}
				else
				{
					MessageBox.Show($"行号必须介于 1 和 {maxLineNumber} 之间", "转到",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("请输入有效的行号", "转到", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtLineNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
			{
				e.Handled = true;
			}
		}
	}
}