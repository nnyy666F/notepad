using notepad;
using System;
using System.Windows.Forms;

namespace Notepad
{
	public partial class FindForm : Form
	{
		private RichTextBox textBox;
		private int lastFoundIndex = -1;
		public Button BtnFindNext => btnFindNext;
		private TextSearchService searchService;

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
			searchService = new TextSearchService(textBox);
			this.TopMost = true;
		}

		private void btnFindNext_Click(object sender, EventArgs e)
		{
			if (!searchService.FindNext(txtFindWhat.Text, chkMatchCase.Checked, chkSearchUp.Checked))
			{
				MessageBox.Show("’“≤ªµΩ\"" + txtFindWhat.Text + "\"", "≤È’“",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			this.Hide();
		}
	}
}