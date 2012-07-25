using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FullTextSeachSample
{
    public partial class InputExtensionDialog : Form
    {
        #region プロパティ

        public ExtensionFilter Filter { get; private set; }

        #endregion

        #region コンストラクタ

        public InputExtensionDialog()
        {
            InitializeComponent();
        }

        public InputExtensionDialog(ExtensionFilter filter)
        {
            InitializeComponent();
            Filter = filter; // コピーオンライトのためそのままセットする
        }

        #endregion

        #region イベントハンドラ

        private void okButton_Click(object sender, EventArgs e)
        {
            var exts = extensionsBox.Text.Trim();
            var re = new Regex(@"^(?:\*|(?:[_0-9a-z]+)(?:\s*\|\s*(?:[_0-9a-z]+))*)$", 
                RegexOptions.IgnoreCase);

            if (!re.IsMatch(exts))
            {
                MessageBox.Show(this, "拡張子の入力に誤りがあります。拡張子に使える文字は[_0-9a-zA-Z]です。", 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Filter = new ExtensionFilter(false, exts, descriptionBox.Text.Trim());
        }

        #endregion
    }
}
