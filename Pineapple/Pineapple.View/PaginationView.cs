using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class PaginationView : Pagination
    {
        public Pagination Pagination
        {
            get
            {
                return this;
            }
        }

        public string RawUrl { get; set; }

        public List<PaginationViewItem> Pages { get; private set; }
        public PaginationViewItem PrevPage { get; private set; }
        public PaginationViewItem NextPage { get; private set; }

        private string BuildPageUrl(int page)
        {
            if (page == CurrentPage) return "javascript:void(0);";

            StringBuilder builder = new StringBuilder(RawUrl);
            builder.Append(RawUrl.LastIndexOf('?') >= 0 ? "&" : "?");
            builder.AppendFormat("size={0}", SizePerPage);
            builder.AppendFormat("&page={0}", page);
            return builder.ToString();
        }

        public void GeneratePages()
        {
            Pages = new List<PaginationViewItem>(TotalPage);
            for (int i = 1; i <= TotalPage; i++)
            {
                var item = new PaginationViewItem();
                item.Page = i;
                item.Active = i == CurrentPage;
                item.Url = BuildPageUrl(i);

                Pages.Add(item);

                if (i == CurrentPage - 1)
                {
                    PrevPage = new PaginationViewItem() { Page = i, Url = item.Url, Active = i > 1 };
                }
                else if (i == CurrentPage + 1)
                {
                    NextPage = new PaginationViewItem() { Page = i, Url = item.Url, Active = i < TotalPage };
                }
            }
            if (PrevPage == null)
            {
                PrevPage = new PaginationViewItem() { Page = 1, Url = BuildPageUrl(CurrentPage), Active = CurrentPage > 1 };
            }
            if (NextPage == null)
            {
                NextPage = new PaginationViewItem() { Page = CurrentPage + 1, Url = BuildPageUrl(CurrentPage), Active = CurrentPage < TotalPage };
            }
        }
    }

    public class PaginationViewItem
    {
        public int Page { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
    }
}
