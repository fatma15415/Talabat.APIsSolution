﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public class ProductSpecParam
    {
        private const int Maxpagesize = 10;
        private int pageSize = 5;
        public int PageSize { 
            get { return pageSize; }

            set { pageSize = (value > Maxpagesize) ? Maxpagesize : value;  }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private string search { get; set; }

        public string? Search { 
            get { return search; }
            set {   search=value.ToLower();}
                              }
    }
}
