using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
	public class TextSearchService
	{
		private RichTextBox textBox;
		private int lastFoundIndex = -1;

		public TextSearchService(RichTextBox textBox)
		{
			this.textBox = textBox;
		}

		public bool FindNext(string searchText, bool matchCase, bool searchUp = false)
		{
			if (string.IsNullOrEmpty(searchText))
				return false;

			string content = textBox.Text;
			int startIndex;
			int index;

			if (searchUp)
			{
				// 向上查找逻辑
				startIndex = textBox.SelectionStart - 1;
				if (startIndex < 0)
					startIndex = content.Length - 1;

				StringComparison comparison = matchCase
					? StringComparison.Ordinal
					: StringComparison.OrdinalIgnoreCase;

				index = content.LastIndexOf(searchText, startIndex, comparison);
			}
			else
			{
				// 向下查找逻辑
				startIndex = textBox.SelectionStart + textBox.SelectionLength;
				if (startIndex >= content.Length)
					startIndex = 0;

				StringComparison comparison = matchCase
					? StringComparison.Ordinal
					: StringComparison.OrdinalIgnoreCase;

				index = content.IndexOf(searchText, startIndex, comparison);
			}

			if (index >= 0)
			{
				textBox.SelectionStart = index;
				textBox.SelectionLength = searchText.Length;
				textBox.ScrollToCaret();
				textBox.Focus(); // 确保 RichTextBox 获得焦点
				lastFoundIndex = index;
				return true;
			}
			else
			{
				lastFoundIndex = -1;
				return false;
			}
		}
	}
}