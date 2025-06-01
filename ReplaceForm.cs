using notepad;
using System;
using System.Windows.Forms;

namespace Notepad
{
	public partial class ReplaceForm : Form
	{
		private TextSearchService searchService;
		private RichTextBox textBox;

		public string FindText
		{
			get { return txtFindWhat.Text; }
			set { txtFindWhat.Text = value; }
		}

		public ReplaceForm(RichTextBox textBox)
		{
			InitializeComponent();
			this.textBox = textBox;
			searchService = new TextSearchService(textBox);
			this.TopMost = true;
		}

		private void btnFindNext_Click(object sender, EventArgs e)
		{
			if (!searchService.FindNext(txtFindWhat.Text, chkMatchCase.Checked, false))
			{
				MessageBox.Show("找不到\"" + txtFindWhat.Text + "\"", "查找",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void btnReplace_Click(object sender, EventArgs e)
		{
			if (textBox.SelectionLength > 0 &&
				textBox.SelectedText.Equals(txtFindWhat.Text,
					chkMatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
			{
				textBox.SelectedText = txtReplaceWith.Text;
			}
			else
			{
				btnFindNext_Click(sender, e);
			}
		}
		private void btnReplaceAll_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtFindWhat.Text))
				return;

			string originalText = textBox.Text;
			string findText = txtFindWhat.Text;
			string replaceText = txtReplaceWith.Text;

			StringComparison comparison = chkMatchCase.Checked
				? StringComparison.Ordinal
				: StringComparison.OrdinalIgnoreCase;

			if (comparison == StringComparison.OrdinalIgnoreCase)
			{
				int index = originalText.IndexOf(findText, comparison);
				int count = 0;

				while (index >= 0)
				{
					textBox.Text = originalText.Substring(0, index) + replaceText +
								  originalText.Substring(index + findText.Length);
					originalText = textBox.Text;
					count++;
					index = originalText.IndexOf(findText, index + replaceText.Length, comparison);
				}

				if (count > 0)
				{
					MessageBox.Show($"已替换 {count} 处", "替换",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("找不到\"" + findText + "\"", "替换",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				string newText = originalText.Replace(findText, replaceText);
				if (newText != originalText)
				{
					int count = (originalText.Length - newText.Length) / (findText.Length - replaceText.Length);
					textBox.Text = newText;
					MessageBox.Show($"已替换 {count} 处", "替换",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("找不到\"" + findText + "\"", "替换",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}