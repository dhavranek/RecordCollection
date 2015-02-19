using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC.Core
{
    public class DataAccessResult
    {
        public bool IsSuccessful { get; set; }
    }

    public class DataAccessResult<TReturnedData> : DataAccessResult
    {
        public TReturnedData Data { get; set; } 
    }

    public class PagedDataAccessResult<TReturnedData> : DataAccessResult<TReturnedData>
    {
        public bool HasNext { get; set; }

        public bool HasPrevious { get; set; }

        public int Count { get; set; }

        public int CurrentPage { get; set; }
    }
}
