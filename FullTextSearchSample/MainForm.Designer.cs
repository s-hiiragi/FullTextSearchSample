namespace FullTextSeachSample
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configTopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerDirectoryItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectExtensionsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerEditorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.resultMessage = new System.Windows.Forms.Label();
            this.resultView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configTopMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configTopMenuItem
            // 
            this.configTopMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerDirectoryItem,
            this.SelectExtensionsItem,
            this.registerEditorItem});
            this.configTopMenuItem.Name = "configTopMenuItem";
            this.configTopMenuItem.Size = new System.Drawing.Size(62, 22);
            this.configTopMenuItem.Text = "設定(&C)";
            // 
            // registerDirectoryItem
            // 
            this.registerDirectoryItem.Name = "registerDirectoryItem";
            this.registerDirectoryItem.Size = new System.Drawing.Size(263, 22);
            this.registerDirectoryItem.Text = "検索するディレクトリを登録(&D)...";
            this.registerDirectoryItem.Click += new System.EventHandler(this.registerDirectoryItem_Click);
            // 
            // SelectExtensionsItem
            // 
            this.SelectExtensionsItem.Name = "SelectExtensionsItem";
            this.SelectExtensionsItem.Size = new System.Drawing.Size(263, 22);
            this.SelectExtensionsItem.Text = "検索する拡張子を選択(&E)...";
            this.SelectExtensionsItem.Click += new System.EventHandler(this.SelectExtensionsItem_Click);
            // 
            // registerEditorItem
            // 
            this.registerEditorItem.Name = "registerEditorItem";
            this.registerEditorItem.Size = new System.Drawing.Size(263, 22);
            this.registerEditorItem.Text = "テキストエディタを登録(&T)...";
            this.registerEditorItem.Click += new System.EventHandler(this.registerEditorItem_Click);
            // 
            // searchBox
            // 
            this.searchBox.AcceptsReturn = true;
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(12, 36);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(551, 19);
            this.searchBox.TabIndex = 0;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(569, 34);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(51, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "検索";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // resultMessage
            // 
            this.resultMessage.AutoSize = true;
            this.resultMessage.Location = new System.Drawing.Point(12, 67);
            this.resultMessage.Name = "resultMessage";
            this.resultMessage.Size = new System.Drawing.Size(403, 12);
            this.resultMessage.TabIndex = 2;
            this.resultMessage.Text = "上のテキストボックスにキーワードを入力し、検索ボタンまたはEnterで検索してください。";
            // 
            // resultView
            // 
            this.resultView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.resultView.FullRowSelect = true;
            this.resultView.Location = new System.Drawing.Point(12, 82);
            this.resultView.Name = "resultView";
            this.resultView.Size = new System.Drawing.Size(608, 352);
            this.resultView.TabIndex = 3;
            this.resultView.UseCompatibleStateImageBehavior = false;
            this.resultView.View = System.Windows.Forms.View.Details;
            this.resultView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultView_KeyDown);
            this.resultView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.resultView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "マッチした行";
            this.columnHeader1.Width = 312;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ファイル名";
            this.columnHeader2.Width = 175;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "行,列";
            this.columnHeader3.Width = 42;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ファイルパス";
            this.columnHeader4.Width = 75;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.resultView);
            this.Controls.Add(this.resultMessage);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "全文検索サンプル (FullTextSearchSample)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configTopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerDirectoryItem;
        private System.Windows.Forms.ToolStripMenuItem SelectExtensionsItem;
        private System.Windows.Forms.ToolStripMenuItem registerEditorItem;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label resultMessage;
        private System.Windows.Forms.ListView resultView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

