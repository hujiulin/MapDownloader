using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapDownloader
{
    public partial class MapDownloaderForm : Form
    {
        private string m_downloadFolder = string.Empty;
        public MapDownloaderForm()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri("https://map.baidu.com/");
        }

        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    this.m_downloadFolder = folderBrowserDialog.SelectedPath;
                    MessageBox.Show(string.Format("下载文件夹：{0}", this.m_downloadFolder));
                }
            }
            
        }

        private List<int> getSelectedLevels()
        {
            List<int> levels = new List<int>();

            foreach (Control c in groupBoxLevel.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked)
                    {
                        try
                        {
                            levels.Add(int.Parse(((CheckBox)c).Text));
                        }
                        catch (Exception)
                        {
                            // TODO: add log/exception 
                        }
                    }
                }
            }
            return levels;
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_downloadFolder))
            {
                MessageBox.Show("请点击菜单`文件夹`指定下载文件夹。");
                return;
            }

            List<int> levels = getSelectedLevels();
            if (levels.Count() < 0)
            {
                MessageBox.Show("请至少勾选一个图层深度。");
                return;
            }
            for(int i = 0;i < levels.Count(); i++)
            {
                DownloadHelper.downloadBatch(@"http://online{0}.map.bdimg.com/onlinelabel/?qt=tile&x={1}&y={2}&z={3}&styles=pl&udt=20160719&scaler=1&p=0", this.m_downloadFolder, i);
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void buttonJump_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能开发中...");
        }
    }
}
