using System;
using System.Windows.Forms;

namespace Notepad
{
	public partial class FindForm : Form
	{
		private RichTextBox textBox;
		private int lastFoundIndex = -1;
		public Button BtnFindNext => btnFindNext;

		public string FindText
		{
			get { return txtFindWhat.Text; }
			set { txtFindWhat.Text = value; }
		}

		public bool MatchCase
		{
			get { return chkMatchCase.Checked; }
			set { chkMatchCase.Checked = value; }
		}

		public bool SearchUp
		{
			get { return chkSearchUp.Checked; }
			set { chkSearchUp.Checked = value; }
		}

		public FindForm(RichTextBox textBox)
		{
			InitializeComponent();
			this.textBox = textBox;
			this.Text = "查找";
		}

		private void btnFindNext_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtFindWhat.Text))
				return;

			string textToFind = txtFindWhat.Text;
			string content = textBox.Text;
			int startIndex = textBox.SelectionStart + textBox.SelectionLength;

			// 如果已经搜索过并且循环查找
			if (lastFoundIndex != -1 && startIndex >= content.Length)
			{
				if (MessageBox.Show("已经到达文档末尾。是否从头开始搜索?", "查找",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					startIndex = 0;
				}
				else
				{
					return;
				}
			}

			StringComparison comparison = chkMatchCase.Checked
				? StringComparison.Ordinal
				: StringComparison.OrdinalIgnoreCase;

			int index = content.IndexOf(textToFind, startIndex, comparison);

			if (index >= 0)
			{
				textBox.SelectionStart = index;
				textBox.SelectionLength = textToFind.Length;
				textBox.ScrollToCaret();
				lastFoundIndex = index;
			}
			else
			{
				MessageBox.Show("找不到\"" + textToFind + "\"", "查找",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				lastFoundIndex = -1;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}