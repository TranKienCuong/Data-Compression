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
using System.Drawing.Imaging;

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
                    try
                    {
                        if (new Bitmap(path).PixelFormat == PixelFormat.Format24bppRgb)
                        {
                            typeLabel.Text = "File type: Bitmap Image (24-bit)";
                            jpegCheckBox.Visible = true;
                            jpegCheckBox.Checked = true;
                        }
                        else
                            typeLabel.Text = "File type: Bitmap Image (not 24-bit)";
                    }
                    catch (Exception)
                    {
                        typeLabel.Text = "File type: Undefined";
                    };
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
                
                byte[] data = new byte[0];
                if (!losslessJPEG)
                    data = File.ReadAllBytes(sourcePath);
                else
                {
                    Bitmap image = new Bitmap(sourcePath);
                    //data = new DifferentialImageCoding().Encode(image);
                    //result += ((int)ALGORITHM.DifferentialImageCoding).ToString();
                    //result += ((char)image.Width).ToString() + ((char)image.Height).ToString();
                }
                ALGORITHM algorithm = 0;
                byte[] encodeData = new byte[0];
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
                    encodeData = new RunLengthCoding().Encode(data);
                };
                if (lzwRadioButton.Checked)
                {
                    algorithm = ALGORITHM.LZW;
                    encodeData = new LZWCoding().Encode(data);
                };
                if (arithmeticRadioButton.Checked)
                {
                    algorithm = ALGORITHM.ArithmeticCoding;
                    //encodeData = new ArithmeticCoding().Encode(data);
                };
                string header = Path.GetExtension(sourcePath) + "\r\n" + ((int)algorithm).ToString();
                FileStream writer = new FileStream(destPath, FileMode.Create, FileAccess.Write);
                byte[] headerData = Utilities.ConvertStringToBytes(header);
                writer.Write(headerData, 0, headerData.Length);
                writer.Write(encodeData, 0, encodeData.Length);
                writer.Close();

                long compressedLength = new FileInfo(destPath).Length;
                file = new CompressedFileInfo(Path.GetFileName(destPath), algorithm.ToString(), originalLength, compressedLength);
                files.Add(file);
            }
            new StatisticsForm(Path.GetDirectoryName(destPath), files).Show();
        }

        void DoExtraction(FileStream reader)
        {
            string sourcePath = pathTextBox.Text;
            string destPath = extractSaveFileDialog.FileName;
            ALGORITHM algorithm = (ALGORITHM)(reader.ReadByte() - '0');
            bool losslessJPEG = false;
            int width = 0, height = 0;
            if (algorithm == ALGORITHM.DifferentialImageCoding)
            {
                losslessJPEG = true;
                int i1 = reader.ReadByte();
                int i2 = reader.ReadByte();
                int i3 = reader.ReadByte();
                int i4 = reader.ReadByte();
                string s1 = Utilities.ConvertIntegerToBinaryString(i1) + Utilities.ConvertIntegerToBinaryString(i2);
                string s2 = Utilities.ConvertIntegerToBinaryString(i3) + Utilities.ConvertIntegerToBinaryString(i4);
                width = Utilities.ConvertBinaryStringToInteger(s1);
                height = Utilities.ConvertBinaryStringToInteger(s2);
                algorithm = (ALGORITHM)(reader.ReadByte() - '0');
            }
            byte[] data = new byte[reader.Length - reader.Position];
            reader.Read(data, 0, data.Length);
            reader.Close();
            byte[] result = new byte[0];
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
                    //result = new ArithmeticCoding().Decode(data);
                    break;
            }
            if (!losslessJPEG)
            {
                File.WriteAllBytes(destPath, result);
            }
            else
            {
                string imageString = Utilities.ConvertBytesToString(result);
                Bitmap image = new DifferentialImageCoding().Decode(imageString, width, height);
                image.Save(destPath, ImageFormat.Bmp);
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
                compressSaveFileDialog.FileName = Path.GetFileNameWithoutExtension(new FileInfo(pathTextBox.Text).Name);
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
            FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read);
            string ext = "";
            int i = reader.ReadByte();
            while (i != '\n')
            {
                ext += ((char)i).ToString();
                i = reader.ReadByte();
            }
            extractSaveFileDialog.FileName = Path.GetFileNameWithoutExtension(path) + ext;
            if (extractSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoExtraction(reader);
            }
        }

        private void algorithmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            algorithmGroupBox.Enabled = !algorithmCheckBox.Checked;
            Utilities.Check();
        }
    }
}
