using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Compression
{
    public partial class StatisticsForm : Form
    {
        string DirectoryPath;
        List<CompressedFileInfo> Files;

        public StatisticsForm(string Path, List<CompressedFileInfo> Files)
        {
            this.DirectoryPath = Path;
            this.Files = Files;
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            pathLabel.Text = "Path: " + DirectoryPath;
            foreach (var file in Files)
            {
                fileListView.Items.Add(new ListViewItem(new string[] { file.FileName, file.CompressedLength.ToString(), file.OriginalLength.ToString(), file.Ratio.ToString(), file.Algorithm }));
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(DirectoryPath))
                Process.Start("explorer.exe", DirectoryPath);
        }
    }
}
