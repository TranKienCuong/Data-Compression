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
                case ".bmp":
                    typeLabel.Text = "File type: Bitmap Image";
                    jpegCheckBox.Visible = true;
                    jpegCheckBox.Checked = true;
                    break;
                case ".cdt":
                    typeLabel.Text = "File type: CDT Compression";
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
            long originalLength = new FileInfo(sourcePath).Length;
            bool losslessJPEG = jpegCheckBox.Checked;
            List<CompressedFileInfo> files = new List<CompressedFileInfo>();
            if (algorithmCheckBox.Checked)
            {
                
            }
            else
            {
                CompressedFileInfo file = new CompressedFileInfo();
                string result = Path.GetExtension(sourcePath) + "\r\n";
                string data;
                if (!losslessJPEG)
                    data = File.ReadAllText(sourcePath);
                else
                {
                    Bitmap image = new Bitmap(sourcePath);
                    image = new DifferentialImageCoding().Encode(image);
                    data = Utilities.ConvertImageToString(image);
                    result += (((int)ALGORITHM.DifferentialImageCoding).ToString() + "\r\n");
                    result += ((char)image.Width).ToString() + ((char)image.Height).ToString() + "\r\n";
                }
                ALGORITHM algorithm = 0;
                string encodeData = "";
                if (shannonFanoRadioButton.Checked)
                {
                    algorithm = ALGORITHM.ShannonFano;
                    // to do
                };
                if (huffmanRadioButton.Checked)
                {
                    algorithm = ALGORITHM.HuffmanCoding;
                    // to do
                };
                if (adaptiveHuffmanRadioButton.Checked)
                {
                    algorithm = ALGORITHM.AdaptiveHuffmanCoding;
                    // to do
                };
                if (runLengthRadioButton.Checked)
                {
                    algorithm = ALGORITHM.RunLengthCoding;
                    encodeData = new RunLengthCoding().Encode(data, losslessJPEG);
                };
                if (lzwRadioButton.Checked)
                {
                    algorithm = ALGORITHM.LZW;
                    encodeData = new LZWCoding().Encode(data, losslessJPEG);
                };
                if (arithmeticRadioButton.Checked)
                {
                    algorithm = ALGORITHM.ArithmeticCoding;
                    encodeData = new ArithmeticCoding().Encode(data, losslessJPEG);
                };
                result += (((int)algorithm).ToString() + "\r\n");
                result += encodeData;
                File.WriteAllText(destPath, result);
                long compressedLength = new FileInfo(destPath).Length;
                file = new CompressedFileInfo(Path.GetFileName(destPath), algorithm.ToString(), originalLength, compressedLength);
                files.Add(file);
            }
            new StatisticsForm(Path.GetDirectoryName(destPath), files).Show();
        }

        void DoExtraction(StreamReader reader)
        {
            string sourcePath = pathTextBox.Text;
            string destPath = extractSaveFileDialog.FileName;
            ALGORITHM algorithm = (ALGORITHM)int.Parse(reader.ReadLine());
            bool losslessJPEG = false;
            int width = 0, height = 0;
            if (algorithm == ALGORITHM.DifferentialImageCoding)
            {
                losslessJPEG = true;
                width = reader.Read();
                height = reader.Read();
                algorithm = (ALGORITHM)int.Parse(reader.ReadLine());
            }
            string data = reader.ReadToEnd();
            reader.Close();
            string result = "";
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
                    result = new RunLengthCoding().Decode(data);
                    break;
                case ALGORITHM.LZW:
                    result = new LZWCoding().Decode(data);
                    break;
                case ALGORITHM.ArithmeticCoding:
                    result = new ArithmeticCoding().Decode(data);
                    break;
            }
            if (!losslessJPEG)
            {
                StreamWriter writer = File.CreateText(destPath);
                writer.Write(result);
                writer.Close();
            }
            else
            {
                Bitmap image = new Bitmap(width, height);
                image = Utilities.ConvertStringToImage(result, width, height);
                image = new DifferentialImageCoding().Decode(image);
                image.Save(destPath);
            }
            Process.Start("explorer.exe", "/select, " + destPath);
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
            string path = pathTextBox.Text;
            StreamReader reader = File.OpenText(path);
            string ext = reader.ReadLine();
            extractSaveFileDialog.FileName = Path.GetFileNameWithoutExtension(path) + ext;
            if (extractSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoExtraction(reader);
            }
        }

        private void algorithmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            algorithmGroupBox.Enabled = !algorithmCheckBox.Checked;
        }
    }
}
