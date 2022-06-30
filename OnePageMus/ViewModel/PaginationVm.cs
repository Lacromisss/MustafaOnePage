using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePageMus.ViewModel
{
    public class PaginationVm<T>
    {
         public List<T> Item { get; set; }
            public int PageCount { get; set; }
            public int ActivPage { get; set; }
        
    }
}
