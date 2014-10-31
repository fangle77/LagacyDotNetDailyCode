using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Attachment
    {
        public int? AttachmentId { get; set; }
        public string OriginName { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string Type { get; set; }
        public int? Size { get; set; }
        public string Alt { get; set; }
        public int? Version { get; set; }

        public string UrlPath
        {
            get
            {
                return string.Format("/{0}/{1}.{2}?v={3}", Path.Trim('/'), FileName, Type, Version);
            }
        }

        public string FilePath
        {
            get
            {
                string relativePath = string.Format(@"\{0}\{1}.{2}", Path.Trim('/').Replace('/', '\\'), FileName, Type);
                return string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), relativePath);
            }
        }

        public bool IsImage { get; set; }
    }
}
