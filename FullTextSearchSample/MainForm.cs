using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace FullTextSeachSample
{
    using Properties;
    
    public partial class MainForm : Form
    {
        #region プロパティ

        private List<string> searchDirectories;
        private ExtensionFilters extensionFilters;
        private string editorPath = String.Empty;
        private string editorCommandLine = String.Empty;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // 設定が無い場合初期化
            if (Settings.Default.SearchDirectories == null)
                Settings.Default.SearchDirectories = new System.Collections.Specialized.StringCollection();

            if (Settings.Default.ExtensionFilters == null)
            {
                Settings.Default.ExtensionFilters = new System.Collections.Specialized.StringCollection();
                Settings.Default.ExtensionFilters.AddRange(new []{
                    "False,*,全てのファイル", 
                    "True,txt,テキストファイル", 
                    "False,md|markdown,Markdown ファイル", 
                });
            }
            
            if (Settings.Default.EditorPath == null)
                Settings.Default.EditorPath = String.Empty;
            
            if (Settings.Default.EditorCommandLine == null)
                Settings.Default.EditorCommandLine = String.Empty;

            // 設定をロード
            searchDirectories = new List<string>(Settings.Default.SearchDirectories.Cast<string>());
            extensionFilters = ExtensionFilters.Deserialize(Settings.Default.ExtensionFilters.Cast<string>());
            editorPath = Settings.Default.EditorPath;
            editorCommandLine = Settings.Default.EditorCommandLine;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.SearchDirectories.Clear();
            Settings.Default.SearchDirectories.AddRange(searchDirectories.ToArray());
            Settings.Default.ExtensionFilters.Clear();
            Settings.Default.ExtensionFilters.AddRange(extensionFilters.Serialize().ToArray());
            Settings.Default.EditorPath = editorPath;
            Settings.Default.EditorCommandLine = editorCommandLine;
            Settings.Default.Save();
        }

        #region メニュー項目ハンドラ

        /// <summary>
        /// イベント : メニューの「設定 >> 検索するディレクトリを登録」が選択された
        /// 検索ディレクトリ登録ダイアログを表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerDirectoryItem_Click(object sender, EventArgs e)
        {
            var dialog = new RegisterDirectoryDialog(searchDirectories);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                searchDirectories = new List<string>(dialog.SearchDirectories);
            }
        }

        /// <summary>
        /// イベント : メニューの「設定 >> 検索する拡張子を選択」が選択された
        /// 検索する拡張子選択ダイアログを表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectExtensionsItem_Click(object sender, EventArgs e)
        {
            var dialog = new SelectExtensionsDialog(extensionFilters);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                extensionFilters = dialog.Filters;

                System.Diagnostics.Debug.WriteLine(String.Join("\n", extensionFilters.Serialize()));
            }
        }

        /// <summary>
        /// イベント : メニューの「設定 >> テキストエディタを登録」が選択された
        /// テキストエディタ登録ダイアログを表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerEditorItem_Click(object sender, EventArgs e)
        {
            var dialog = new RegisterEditorDialog(editorPath, editorCommandLine);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                editorPath = dialog.Path;
                editorCommandLine = dialog.CommandLine;
            }
        }

        #endregion

        #region イベントハンドラ

        /// <summary>
        /// イベント : 検索ボタンが押された
        /// 検索を開始する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            searchFullText(searchBox.Text);
        }

        /// <summary>
        /// イベント : 検索ワードボックスでEnterが押された
        /// 検索を開始する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                searchFullText(searchBox.Text);
            }
        }

        /// <summary>
        /// イベント : 検索結果一覧の項目がダブルクリックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openSelectedTextFile();
        }

        /// <summary>
        /// イベント : 検索結果一覧の項目を選択した状態でEnterが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                openSelectedTextFile();
        }

        #endregion

        /// <summary>
        /// 全文検索を行う
        /// </summary>
        /// <remarks>
        /// TODO プログレスバーを表示して、リストビューに項目が追加されるたびに更新させたい。
        /// resultMessage.Textはイベント処理が終わるまで描画されない？(=> 重たい処理は別スレッドで行うべき？)
        /// </remarks>
        /// <param name="text"></param>
        private void searchFullText(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return;

            // 前回の結果をクリア
            resultMessage.Text = "";
            resultView.Items.Clear();

            var extensions = extensionFilters.FilteredExtensions.ToArray();
            if (extensions.Length == 0)
            {
                resultMessage.Text = "検索するファイルの拡張子が選択されていません。";
                return;
            }

            resultMessage.Text = "検索ディレクトリ以下のテキストファイルを収集中...";

            // 検索対象拡張子の数によって処理分け
            string[] searchFiles;
            if (extensions.Contains("*"))
            {
                searchFiles =
                    searchDirectories
                    .SelectMany(d => Directory.GetFiles(d, "*", SearchOption.AllDirectories))
                    .ToArray();
            }
            else
            {
                var x =
                    from d in searchDirectories
                    from ext in extensions
                    select Directory.GetFiles(d, "*." + ext, SearchOption.AllDirectories) into files
                    from f in files
                    select f;
                searchFiles = x.ToArray();
            }

            // TODO ファイル数が多いときは警告する

            resultMessage.Text = searchFiles.Length + " 件のテキストファイルを検索中...";

            resultView.BeginUpdate();
            foreach (var filePath in searchFiles)
            {
                var fileName = Path.GetFileName(filePath);

                // 先頭のnバイトから文字コードを判定
                const int READ_SIZE = 512;
                Encoding enc;
                using (var fs = File.OpenRead(filePath))
                {
                    int readSize = (int)Math.Min(READ_SIZE, fs.Length);
                    byte[] buf = new byte[readSize];
                    fs.Read(buf, 0, readSize);

                    enc = StringUtils.GetEncoding(buf);
                    if (enc == null)
                        enc = Encoding.ASCII;
                }

                // 行単位で検索
                int row = 1;
                foreach (var line in File.ReadLines(filePath, enc))
                {
                    var column = line.IndexOf(text);
                    if (column >= 0)
                    {
                        column += 1; // 列は1から始まる

                        var item = new ListViewItem(new[] { line, fileName, row + "," + column, filePath });
                        resultView.Items.Add(item);
                    }
                    ++row;
                }
            }

            resultMessage.Text = resultView.Items.Count + " 件にマッチしました。";
            resultView.EndUpdate();
        }

        /// <summary>
        /// コマンドライン引数を実引数に展開する
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string ExpandEditorCommandLine(string filePath, int row, int column)
        {
            var parser = new CommandLineParser(new Dictionary<string, string> {
                {"file", "\"" + filePath + "\""}, 
                {"row", row.ToString()}, 
                {"row0", (row - 1).ToString()}, 
                {"column", column.ToString()}, 
                {"column0", (column - 1).ToString()}
            });

            return parser.parse(editorCommandLine);
        }

        /// <summary>
        /// 選択されたマッチ行が含まれるテキストファイルを開く
        /// マッチ位置にスクロールする (対応エディタのみ, 「設定 >> テキストエディタを登録」で設定する)
        /// </summary>
        private void openSelectedTextFile()
        {
            if (resultView.SelectedItems.Count == 0)
                return;

            // 選択行から必要な情報を取得し、コマンドライン引数を構築する
            ListViewItem selectedItem = resultView.SelectedItems[0];
            var filePath = selectedItem.SubItems[3].Text;

            var rowColumn = selectedItem.SubItems[2].Text.Split(',');
            var row = Int32.Parse(rowColumn[0]);
            var column = Int32.Parse(rowColumn[1]);

            var commandLine = ExpandEditorCommandLine(filePath, row, column);
            
            // テキストファイルを開く
            if (String.IsNullOrWhiteSpace(editorPath))
            {
                Process.Start(filePath);
            }
            else
            {
                try
                {
                    Process.Start(editorPath, commandLine);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(this, "エディタを起動できません。", this.Text, 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
