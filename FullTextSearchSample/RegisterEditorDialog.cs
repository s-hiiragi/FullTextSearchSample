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
    public partial class RegisterEditorDialog : Form
    {
        #region プロパティ

        public static readonly CommandLineParser Parser = new CommandLineParser(new Dictionary<string, string> {
            {"file", ""}, 
            {"row", ""}, 
            {"row0", ""}, 
            {"column", ""}, 
            {"column0", ""}
        });

        public string Path { get; private set; }
        public string CommandLine { get; private set; }

        #endregion

        #region コンストラクタ

        public RegisterEditorDialog()
        {
            InitializeComponent();
        }

        public RegisterEditorDialog(string editorPath, string commandLine)
        {
            InitializeComponent();

            this.Path = editorPath;
            this.CommandLine = commandLine;

            editorPathBox.Text = this.Path;
            commandLineBox.Text = this.CommandLine;
        }

        #endregion

        #region イベントハンドラ

        private void selectEditorButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                editorPathBox.Text = openFileDialog.FileName;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var commandLine = commandLineBox.Text;
            try
            {
                CommandLineParser.Validate(commandLine);
            }
            catch (SyntaxErrorException ex)
            {
                MessageBox.Show(this, ex.Message, this.Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Path = editorPathBox.Text;
            this.CommandLine = commandLine;
        }

        #endregion
    }
}
