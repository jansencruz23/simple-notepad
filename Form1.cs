using MaterialSkin;
using MaterialSkin.Controls;
using System.Reflection;

namespace Notepad
{
    public partial class Form1 : MaterialForm
    {
        private string _fileName;
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey900,
                Primary.Grey900, Primary.Grey900, Accent.Amber100, TextShade.WHITE);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var file = new OpenFileDialog();
            file.Filter = "Text Files |*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtInput.Text = File.ReadAllText(file.FileName);
                _fileName = Path.GetFileName(file.FileName);
                Text = _fileName;
            }
        }

        private void txtInput_SelectionChanged(object sender, EventArgs e)
        {
            var currentLine = txtInput.GetLineFromCharIndex(txtInput.SelectionStart) + 1;
            var currentColumn = txtInput.SelectionStart - txtInput.GetFirstCharIndexOfCurrentLine() + 1;

            lblCursor.Text = $"Ln {currentLine}, Col {currentColumn}";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();

            save.Filter = "Text Files |*.txt";
            save.FilterIndex = 2;
            save.RestoreDirectory = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                _fileName = Path.GetFileName(save.FileName);
                StreamWriter file = new StreamWriter(save.FileName.ToString());
                file.WriteLine(txtInput.Text);
                file.Close();

                Text = _fileName;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var font = new FontDialog();

            if (font.ShowDialog() == DialogResult.OK)
            {
                txtInput.Font = font.Font;
            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == DialogResult.OK)
            {
                txtInput.ForeColor = color.Color;
            }
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.SelectionColor = Color.Black;
            txtInput.SelectionBackColor = Color.Yellow;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void wrapToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            txtInput.WordWrap = !txtInput.WordWrap;
        }

        private void wrapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            wrapToolStripMenuItem.Checked = !wrapToolStripMenuItem.Checked;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileName = string.Empty;
            Text = "Untitled";
            txtInput.Clear();
        }
    }
}