using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MapDownloader
{
    class DownloadHelper
    {
        private static WebClient wc = null;
        private static void initWebClientInstance()
        {
            if (null == wc)
            {
                wc = new System.Net.WebClient();
            }
        }

        private static void download(string url, string filename)
        {
            // Check filename exist or not
            if (File.Exists(filename))
            {
                return;
            }

            // Create directory
            string dir = Path.GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            initWebClientInstance();
            wc.DownloadFile(url, filename);
        }

        public static void downloadBatch(string urlPattern, string targetFoler, int level)
        {
            int maxX = (int)Math.Pow(2, level - 1);
            int maxY = (int)Math.Pow(2, level - 1);
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    string url = String.Format(urlPattern, 1, x, y, level);
                    string filename = Path.Combine(targetFoler, level.ToString(), x.ToString()) + "\\" + y.ToString() + ".png";
                    download(url, filename);
                }
            }
        }
    }
}
