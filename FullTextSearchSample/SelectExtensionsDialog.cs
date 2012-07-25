using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FullTextSeachSample
{
    public partial class SelectExtensionsDialog : Form
    {
        #region プロパティ

        public ExtensionFilters Filters { get; private set; }

        #endregion

        #region コンストラクタ

        public SelectExtensionsDialog()
        {
            InitializeComponent();
            Filters = new ExtensionFilters();
        }

        public SelectExtensionsDialog(ExtensionFilters filters)
        {
            InitializeComponent();
            Filters = filters; // コピーオンライトするため直接セットする

            if (Filters.Filters.Count() >= 1)
            {
                extensionsView.Items.Clear();
                var items = new List<ListViewItem>();
                foreach (var f in Filters.Filters)
                {
                    var item = new ListViewItem(new[] {"", f.ExtensionsString, f.Description});
                    item.Checked = f.Enabled;
                    items.Add(item);
                }
                extensionsView.Items.AddRange(items.ToArray());
            }
            // else if count == 0 : ListViewにデフォルトのフィルタ列をセット (フォームデザイナ)
        }

        #endregion

        #region イベントハンドラ

        private void addButton_Click(object sender, EventArgs e)
        {
            var dialog = new InputExtensionDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new[] {
                    String.Empty, dialog.Filter.ExtensionsString, dialog.Filter.Description
                });
                extensionsView.Items.Add(item);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var item = extensionsView.SelectedItems.Cast<ListViewItem>().DefaultIfEmpty(null).First();
            if (item == null)
            {
                MessageBox.Show(this, "項目が選択されていません。", this.Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filter = new ExtensionFilter(false, item.SubItems[1].Text, item.SubItems[2].Text);
            var dialog = new InputExtensionDialog(filter);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                item.SubItems[1].Text = dialog.Filter.ExtensionsString;
                item.SubItems[2].Text = dialog.Filter.Description;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var index = extensionsView.SelectedIndices.Cast<int>().DefaultIfEmpty(-1).First();
            if (index == -1)
            {
                MessageBox.Show(this, "項目が選択されていません。", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            extensionsView.Items.RemoveAt(index);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var filters =
                from item in extensionsView.Items.Cast<ListViewItem>()
                select new ExtensionFilter(item.Checked, item.SubItems[1].Text, item.SubItems[2].Text);

            Filters = new ExtensionFilters(filters); // 新しい設定に置き換える
        }

        #endregion
    }
}
