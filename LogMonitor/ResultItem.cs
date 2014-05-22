using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitor
{
    class ResultItem
    {
        public ResultItem() { }
        public ResultItem(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
        public string Content
        {
            get
            {
                return contentBuilder.ToString();
            }
        }
        public int LineNumber { get; set; }

        private StringBuilder contentBuilder = new StringBuilder();
        public void AddContent(string content)
        {
            contentBuilder.Append(content);
        }

        public override string ToString()
        {
            return string.Format("File:{0},\r\nLine:{1},\r\nContent:{2}\r\n", FileName, LineNumber, contentBuilder);
        }
    }
}
