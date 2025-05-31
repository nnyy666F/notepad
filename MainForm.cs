using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
	public partial class MainForm : Form
	{
		private string currentFilePath = null;
		private bool isModified = false;
		private FindForm findForm;

		public MainForm()
		{
			InitializeComponent();
			this.AllowDrop = true;
			richTextBox1.AllowDrop = true;
			this.DragEnter += MainForm_DragEnter;
			this.DragDrop += MainForm_DragDrop;
			richTextBox1.DragEnter += RichTextBox1_DragEnter;
			richTextBox1.DragDrop += RichTextBox1_DragDrop;
			this.KeyPreview = true;
			this.KeyDown += MainForm_KeyDown;
			richTextBox1.Font = new Font("宋体", 9);
			richTextBox1.SuspendLayout();
			richTextBox1.ResumeLayout();
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		public FindForm GetValidFindForm()
		{
			if (findForm == null || findForm.IsDisposed)
			{
				findForm = new FindForm(richTextBox1);
			}
			return findForm;
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files != null && files.Length > 0)
			{
				OpenFile(files[0]);
			}
		}

		private void RichTextBox1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files != null && files.Length > 0)
			{
				OpenFile(files[0]);
			}
		}

		private void OpenFile(string filePath)
		{
			try
			{
				if (!CheckSaveChanges())
				{
					return;
				}

				richTextBox1.Text = File.ReadAllText(filePath);
				currentFilePath = filePath;
				isModified = false;
				UpdateFormTitle();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"无法打开文件: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.W)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				CloseCurrentFile();
			}
		}

		private void CloseCurrentFile()
		{
			if (!CheckSaveChanges())
			{
				return;
			}

			richTextBox1.Clear();
			currentFilePath = null;
			isModified = false;
			UpdateFormTitle();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateStatusBar();
		}

		private void UpdateStatusBar()
		{
			if (statusBarToolStripMenuItem.Checked)
			{
				int line = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
				int col = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
				toolStripStatusLabel1.Text = $"行 {line}, 列 {col}";
				statusStrip1.Visible = true;
			}
			else
			{
				statusStrip1.Visible = false;
			}
		}

		private void UpdateFormTitle()
		{
			this.Text = string.IsNullOrEmpty(currentFilePath) ?
				"无标题 - 记事本" :
				$"{Path.GetFileName(currentFilePath)} - 记事本";
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			if (!isModified)
			{
				isModified = true;
				UpdateFormTitle();
			}
			UpdateStatusBar();
		}

		private void richTextBox1_SelectionChanged(object sender, EventArgs e)
		{
			UpdateStatusBar();
		}

		private bool CheckSaveChanges()
		{
			if (isModified)
			{
				DialogResult result = MessageBox.Show(
					"是否保存更改？",
					"记事本",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					return SaveFile();
				}
				else if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
		}

		private bool SaveFile()
		{
			if (string.IsNullOrEmpty(currentFilePath))
			{
				return SaveFileAs();
			}
			else
			{
				try
				{
					File.WriteAllText(currentFilePath, richTextBox1.Text, Encoding.UTF8);
					isModified = false;
					UpdateFormTitle();
					return true;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"保存文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
		}

		private bool SaveFileAs()
		{
			saveFileDialog1.FileName = string.IsNullOrEmpty(currentFilePath) ?
				"无标题.txt" :
				Path.GetFileName(currentFilePath);
			saveFileDialog1.InitialDirectory = string.IsNullOrEmpty(currentFilePath) ?
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) :
				Path.GetDirectoryName(currentFilePath);

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				currentFilePath = saveFileDialog1.FileName;
				return SaveFile();
			}
			return false;
		}

		private void OpenFile()
		{
			if (!CheckSaveChanges())
			{
				return;
			}

			openFileDialog1.InitialDirectory = string.IsNullOrEmpty(currentFilePath) ?
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) :
				Path.GetDirectoryName(currentFilePath);

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
					currentFilePath = openFileDialog1.FileName;
					isModified = false;
					UpdateFormTitle();
				}
				catch (Exception ex)
				{
					MessageBox.Show($"打开文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void NewFile()
		{
			if (!CheckSaveChanges())
			{
				return;
			}

			richTextBox1.Clear();
			currentFilePath = null;
			isModified = false;
			UpdateFormTitle();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !CheckSaveChanges();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewFile();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileAs();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.CanUndo)
			{
				richTextBox1.Undo();
				//richTextBox1.ClearUndo();
			}
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.CanRedo)
			{
				richTextBox1.Redo();
			}
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.SelectionLength > 0)
			{
				richTextBox1.Cut();
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.SelectionLength > 0)
			{
				richTextBox1.Copy();
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Text)))
			{
				richTextBox1.Paste();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.SelectionLength > 0)
			{
				richTextBox1.SelectedText = "";
			}
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBox1.SelectAll();
		}

		private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBox1.SelectedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wordWrapToolStripMenuItem.Checked = !wordWrapToolStripMenuItem.Checked;
			richTextBox1.WordWrap = wordWrapToolStripMenuItem.Checked;
		}

		private void fontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fontDialog1.Font = richTextBox1.Font;
			fontDialog1.Color = richTextBox1.ForeColor;

			if (fontDialog1.ShowDialog() == DialogResult.OK)
			{
				richTextBox1.SuspendLayout();
				richTextBox1.Font = fontDialog1.Font;
				richTextBox1.ForeColor = fontDialog1.Color;
				richTextBox1.ResumeLayout();
			}
		}

		private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
			UpdateStatusBar();
		}

		private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("记事本帮助文档", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("记事本 1.0\n\n简单的文本编辑器", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (findForm == null || findForm.IsDisposed)
			{
				findForm = new FindForm(richTextBox1);
			}
			findForm.Show();
		}

		private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ReplaceForm replaceForm = new ReplaceForm(richTextBox1);
			replaceForm.Show();
		}

		private void goToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (GoToForm goToForm = new GoToForm(richTextBox1))
			{
				goToForm.ShowDialog();
			}
		}
	}
}