using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Data
{
    public class Pager
    {
        public int CurrentPagte
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int TotalPage
        {
            get;
            set;
        }
    }
}
