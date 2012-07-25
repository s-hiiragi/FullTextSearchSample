namespace FullTextSeachSample
{
    partial class RegisterEditorDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.editorPathBox = new System.Windows.Forms.TextBox();
            this.selectEditorButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.commandLineBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "テキストエディタのパス";
            // 
            // editorPathBox
            // 
            this.editorPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorPathBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.editorPathBox.Location = new System.Drawing.Point(12, 24);
            this.editorPathBox.Name = "editorPathBox";
            this.editorPathBox.ReadOnly = true;
            this.editorPathBox.Size = new System.Drawing.Size(339, 19);
            this.editorPathBox.TabIndex = 1;
            // 
            // selectEditorButton
            // 
            this.selectEditorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectEditorButton.Location = new System.Drawing.Point(357, 22);
            this.selectEditorButton.Name = "selectEditorButton";
            this.selectEditorButton.Size = new System.Drawing.Size(60, 23);
            this.selectEditorButton.TabIndex = 2;
            this.selectEditorButton.Text = "参照...";
            this.selectEditorButton.UseVisualStyleBackColor = true;
            this.selectEditorButton.Click += new System.EventHandler(this.selectEditorButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(12, 58);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(405, 75);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "コマンドライン引数 (以下の特殊変数が使えます)\r\n%f  ファイルパス (ダブルクォーテーションで囲まれます)\r\n%r  行番号 (1～)\r\n%R  行番号 (" +
    "0～)\r\n%c  列番号 (1～)\r\n%C  列番号 (0～)";
            // 
            // commandLineBox
            // 
            this.commandLineBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandLineBox.Location = new System.Drawing.Point(12, 139);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(405, 19);
            this.commandLineBox.TabIndex = 4;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "実行ファイル|*.exe";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(251, 178);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK(&O)";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(332, 178);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(85, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "キャンセル(&C)";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // RegisterEditorDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(429, 213);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.commandLineBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.selectEditorButton);
            this.Controls.Add(this.editorPathBox);
            this.Controls.Add(this.label1);
            this.Name = "RegisterEditorDialog";
            this.Text = "テキストエディタを登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editorPathBox;
        private System.Windows.Forms.Button selectEditorButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox commandLineBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}