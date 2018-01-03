using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Data_Compression
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void LoadFileInfo(string path)
        {
            jpegCheckBox.Visible = false;
            jpegCheckBox.Checked = false;
            string ext = Path.GetExtension(path).ToLower();
            switch (ext)
            {
                case ".txt":
                    typeLabel.Text = "File type: Text";
                    break;
                case ".jpg":
                case ".png":
                case ".bmp":
                    typeLabel.Text = "File type: Image";
                    jpegCheckBox.Visible = true;
                    jpegCheckBox.Checked = true;
                    break;
                case ".cdt":
                    typeLabel.Text = "File type: CDT Compression";
                    StreamReader reader = File.OpenText(pathTextBox.Text);
                    reader.ReadLine();
                    string extension = reader.ReadLine();
                    extractSaveFileDialog.Filter = "*" + extension + " | *" + extension + " | All files(*.*) | *.*";
                    break;
                default:
                    typeLabel.Text = "File type: Undefined";
                    break;
            }
            FileInfo f = new FileInfo(path);
            lengthLabel.Text = "Total length: " + String.Format("{0:n0}", f.Length) + " bytes";
        }

        void DoCompression()
        {
            string sourcePath = pathTextBox.Text;
            string destPath = compressSaveFileDialog.FileName;
            bool losslessJPEG = jpegCheckBox.Checked;
            List<CompressedFileInfo> files = new List<CompressedFileInfo>();
            if (algorithmCheckBox.Checked)
            {
                // to do
                files.Add(new RunLengthCoding().Encode(sourcePath, destPath, losslessJPEG));
                // to do
            }
            else
            {
                CompressedFileInfo file = new CompressedFileInfo();
                if (shannonFanoRadioButton.Checked)
                {
                    // to do
                };
                if (huffmanRadioButton.Checked)
                {
                    // to do
                };
                if (adaptiveHuffmanRadioButton.Checked)
                {
                    // to do
                };
                if (runLengthRadioButton.Checked)
                {
                    file = new RunLengthCoding().Encode(sourcePath, destPath, losslessJPEG);
                };
                if (lzwRadioButton.Checked)
                {
                    // to do
                };
                if (arithmeticRadioButton.Checked)
                {
                    // to do
                };
                files.Add(file);
            }
            new StatisticsForm(Path.GetDirectoryName(destPath), files).Show();
        }

        void DoExtraction()
        {
            string sourcePath = pathTextBox.Text;
            string destPath = extractSaveFileDialog.FileName;
            StreamReader reader = File.OpenText(sourcePath);
            ALGORITHM algorithm = (ALGORITHM)int.Parse(reader.ReadLine());
            string ext = reader.ReadLine();
            extractSaveFileDialog.Filter = "*" + ext + " | *" + ext + " | All files(*.*) | *.*";
            switch (algorithm)
            {
                case ALGORITHM.ShannonFano:
                    // to do
                    break;
                case ALGORITHM.HuffmanCoding:
                    // to do
                    break;
                case ALGORITHM.AdaptiveHuffmanCoding:
                    // to do
                    break;
                case ALGORITHM.RunLengthCoding:
                    new RunLengthCoding().Decode(sourcePath, destPath);
                    break;
                case ALGORITHM.LZW:
                    // to do
                    break;
                case ALGORITHM.ArithmeticCoding:
                    // to do
                    break;
            }
            Process.Start("explorer.exe", destPath);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            pathTextBox.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(pathTextBox.Text))
            {
                existLabel.Visible = false;
                compressSaveFileDialog.InitialDirectory = Path.GetDirectoryName(pathTextBox.Text);
                extractSaveFileDialog.InitialDirectory = Path.GetDirectoryName(pathTextBox.Text);
                LoadFileInfo(pathTextBox.Text);
            }
            else
            {
                existLabel.Visible = true;
            }
        }

        private void compressButton_Click(object sender, EventArgs e)
        {
            if (compressSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoCompression();
            }
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            if (extractSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoExtraction();
            }
        }

        private void algorithmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            algorithmGroupBox.Enabled = !algorithmCheckBox.Checked;
        }
    }
}
