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
    }
}
