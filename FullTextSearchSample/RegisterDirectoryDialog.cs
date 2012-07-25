using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FullTextSeachSample
{
    public partial class RegisterDirectoryDialog : Form
    {
        #region プロパティ

        public IEnumerable<string> SearchDirectories { get; private set; }

        #endregion

        #region コンストラクタ

        public RegisterDirectoryDialog()
        {
            InitializeComponent();
        }

        public RegisterDirectoryDialog(IEnumerable<string> searchDirectories)
        {
            InitializeComponent();

            this.SearchDirectories = searchDirectories;
            directoryList.Items.AddRange(this.SearchDirectories.ToArray());
        }

        #endregion

        #region イベントハンドラ

        private void addButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;

                if (directoryList.Items.Contains(path))
                {
                    MessageBox.Show(this, "既に追加されています。", this.Text, 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                directoryList.Items.Add(folderBrowserDialog.SelectedPath);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (directoryList.SelectedItems.Count == 0)
                return;

            var indices = 
                from int i in directoryList.SelectedIndices
                select i;
            indices = indices.Reverse();

            foreach (var index in indices)
            {
                directoryList.Items.RemoveAt(index);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SearchDirectories = directoryList.Items.Cast<string>();
        }

        #endregion
    }
}
