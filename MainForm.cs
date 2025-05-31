using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
	public partial class MainForm : Form
	{
		private string currentFilePath = null;
		private bool isModified = false;
		private FindForm findForm;
		private float _initialFontSize;
		private float currentZoom = 1.0f; // 初始缩放比例为100%
		private const float zoomStep = 0.1f; // 缩放步长
		private const float maxZoom = 5.0f; // 最大缩放比例为500%
		private const float minZoom = 0.1f; // 最小缩放比例为10%
		private string originalContent = string.Empty;

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
			this.richTextBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseWheel);
			_initialFontSize = richTextBox1.Font.Size;
			currentZoom = 1.0f;
			UpdateZoomStatus();
		}

		private void richTextBox1_MouseWheel(object sender, MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				((HandledMouseEventArgs)e).Handled = true;

				// 保存旧缩放值用于日志
				float oldZoom = currentZoom;

				if (e.Delta > 0)
				{
					currentZoom = Math.Min(currentZoom + zoomStep, maxZoom);
				}
				else if (e.Delta < 0)
				{
					currentZoom = Math.Max(currentZoom - zoomStep, minZoom);
				}

				// 四舍五入到整数百分比避免浮点误差
				currentZoom = (float)Math.Round(currentZoom, 2);

				// 修正日志输出
				//Debug.WriteLine($"缩放更新: 之前={(int)(oldZoom * 100)}%, 之后={(int)(currentZoom * 100)}%");

				ApplyZoom();
			}
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
				if (!CheckSaveChanges()) return;
				originalContent = File.ReadAllText(filePath);
				//MessageBox.Show(originalContent, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				richTextBox1.Text = originalContent;

				currentFilePath = filePath;
				isModified = false;
				UpdateFormTitle();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"无法打开文件: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private string CalculateMD5(string filePath)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(filePath))
				{
					var hashBytes = md5.ComputeHash(stream);
					return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
				}
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

		private void ResetFileState()
		{
			richTextBox1.Clear();
			currentFilePath = null;
			isModified = false;
			originalContent = string.Empty; // 重置原始内容
			UpdateFormTitle();
		}
		private void CloseCurrentFile()
		{
			if (IsContentUnchanged())
			{
				ResetFileState();
				return;
			}

			if (!CheckSaveChanges())
			{
				return;
			}

			ResetFileState();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateStatusBar();
		}

		private void UpdateZoomStatus()
		{
			toolStripStatusLabelZoom.Text = $"缩放: {(int)(currentZoom * 100)}%";
		}

		private void ApplyZoom()
		{
			// 暂停布局更新
			richTextBox1.SuspendLayout();

			// 保存当前光标位置和选择范围
			int selectionStart = richTextBox1.SelectionStart;
			int selectionLength = richTextBox1.SelectionLength;
			Font currentFont = richTextBox1.Font;
			float newSize = _initialFontSize * currentZoom;

			// 设置最小字体大小限制（避免太小）
			if (newSize < 6) newSize = 6;

			// 创建新字体（保持原有字体名称和样式）
			Font newFont = new Font(
				currentFont.FontFamily,
				newSize,
				currentFont.Style,
				currentFont.Unit
			);
			richTextBox1.Font = newFont;
			richTextBox1.SelectionStart = selectionStart;
			richTextBox1.SelectionLength = selectionLength;
			UpdateZoomStatus();

			// 恢复布局更新
			richTextBox1.ResumeLayout();
		}

		private void UpdateStatusBar()
		{
			if (statusBarToolStripMenuItem.Checked)
			{
				int line = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
				int col = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
				toolStripStatusLabel1.Text = $"行 {line}, 列 {col}";

				if (!string.IsNullOrEmpty(currentFilePath))
				{
					try
					{
						using (var reader = new System.IO.StreamReader(currentFilePath, true))
						{
							Encoding detectedEncoding = reader.CurrentEncoding;
							toolStripStatusLabelEncoding.Text = $"编码: {detectedEncoding.EncodingName}";
						}
					}
					catch (Exception)
					{
						toolStripStatusLabelEncoding.Text = "编码: 未知";
					}
				}
				else
				{
					toolStripStatusLabelEncoding.Text = "编码: 无";
				}

				if (!string.IsNullOrEmpty(currentFilePath))
				{
					try
					{
						System.IO.FileInfo fileInfo = new System.IO.FileInfo(currentFilePath);
						long fileSize = fileInfo.Length;
						string sizeText;
						if (fileSize < 1024)
						{
							sizeText = $"{fileSize} 字节";
						}
						else if (fileSize < 1024 * 1024)
						{
							sizeText = $"{fileSize / 1024.0:F2} KB";
						}
						else
						{
							sizeText = $"{fileSize / (1024.0 * 1024):F2} MB";
						}
						toolStripStatusLabelFileSize.Text = $"大小: {sizeText}";
					}
					catch (Exception)
					{
						toolStripStatusLabelFileSize.Text = "大小: 未知";
					}
				}
				else
				{
					toolStripStatusLabelFileSize.Text = "大小: 无";
				}

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
			if (!isModified && !IsContentUnchanged())
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
			if (isModified && !IsContentUnchanged())
			{
				MessageBox.Show(richTextBox1.Text + "," + originalContent, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				DialogResult result = MessageBox.Show(
					"是否保存更改？",
					"记事本",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
					return SaveFile();
				else if (result == DialogResult.Cancel)
					return false;
			}

			if (IsContentUnchanged())
			{
				isModified = false;
			}

			return true;
		}

		private bool IsContentUnchanged()
		{
			return richTextBox1.Text == originalContent;
		}
		private string CalculateMD5FromString(string input)
		{
			if (input == null)
				input = string.Empty;

			using (var md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);
				return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
			}
		}
		private bool SaveFile()
		{
			if (string.IsNullOrEmpty(currentFilePath))
				return SaveFileAs();

			try
			{
				File.WriteAllText(currentFilePath, richTextBox1.Text);
				isModified = false;

				originalContent = richTextBox1.Text;

				UpdateFormTitle();
				UpdateStatusBar();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"保存文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}
		private void NewFile()
		{
			if (!CheckSaveChanges()) return;

			ResetFileState();
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

				// 更新初始字体大小（用户新选择的字体大小）
				_initialFontSize = fontDialog1.Font.Size;

				// 重置缩放比例为100%
				currentZoom = 1.0f;

				richTextBox1.Font = fontDialog1.Font;
				richTextBox1.ForeColor = fontDialog1.Color;

				richTextBox1.ResumeLayout();

				// 更新状态栏
				UpdateZoomStatus();
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
			MessageBox.Show("记事本 1.0\n\n这是一个简单的文本编辑器demo", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			if (CheckSaveChanges())
			{
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					currentFilePath = openFileDialog1.FileName;
					try
					{
						richTextBox1.Text = System.IO.File.ReadAllText(currentFilePath);
						isModified = false;
						UpdateFormTitle();
						UpdateStatusBar(); // 更新状态栏信息
						originalContent = richTextBox1.Text;
						//MessageBox.Show(originalContent, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"打开文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
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