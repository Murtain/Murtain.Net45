using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Net.WebClient
{
    /// <summary>    
    /// 下载数据参数    
    /// </summary>    
    public class DownloadEventArgs : EventArgs
    {
        /// <summary>    
        /// 已接收的字节数    
        /// </summary>    
        public int BytesReceived { get; set; }

        /// <summary>    
        /// 总字节数    
        /// </summary>    
        public int TotalBytes { get; set; }

        /// <summary>    
        /// 当前缓冲区接收的数据    
        /// </summary>    
        public byte[] ReceivedData { get; set; }
    }
}
