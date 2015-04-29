using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Pagination
    {
        private int currentPage = 1;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        private int sizePerPage = 10;
        public int SizePerPage
        {
            get { return sizePerPage; }
            set { sizePerPage = value; }
        }

        public int TotalRecords { get; set; }


        public int TotalPage
        {
            get
            {
                return (TotalRecords + SizePerPage) == 0 ? 0 : (TotalRecords / SizePerPage + (TotalRecords % SizePerPage > 0 ? 1 : 0));
            }
        }

        public int Offset
        {
            get
            {
                return (CurrentPage - 1) * SizePerPage;
            }
        }
    }
}