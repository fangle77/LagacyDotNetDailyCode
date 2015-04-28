using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Cases
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public long TimeInMs { get; set; }
        public int DisplayOrder { get; set; }

        public List<CaseItem> CaseItems { get; set; }
    }

    public class CaseItem
    {
        public int CaseItemId { get; set; }
        public int CaseId { get; set; }
        public string Title { get; set; }
        public int AttachmentId { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreateDate { get; set; }

        public Attachment Attachment { get; set; }
    }

    public class CaseItemDisplayOrderComparer : IComparer<CaseItem>
    {
        public int Compare(CaseItem x, CaseItem y)
        {
            return x.DisplayOrder - y.DisplayOrder;
        }
    }
}
