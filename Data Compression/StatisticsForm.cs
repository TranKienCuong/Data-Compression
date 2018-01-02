using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Compression
{
    public partial class StatisticsForm : Form
    {
        string Path;
        List<CompressedFileInfo> Files;

        public StatisticsForm(string Path, List<CompressedFileInfo> Files)
        {
            this.Path = Path;
            this.Files = Files;
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            pathLabel.Text = "Path: " + Path;
            foreach (var file in Files)
            {
                fileListView.Items.Add(new ListViewItem(new string[] { file.FileName, file.CompressedLength.ToString(), file.OriginalLength.ToString(), file.Ratio.ToString(), file.Algorithm }));
            }
        }
    }
}
