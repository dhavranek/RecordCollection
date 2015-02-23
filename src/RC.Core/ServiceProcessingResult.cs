using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC.Core
{
    public class ServiceProcessingResult
    {
        public bool IsSuccessful { get; set; }
        public ServiceProcessingError Error { get; set; }
    }

    public class ServiceProcessingResult<TReturnedData> : ServiceProcessingResult
    {
        public TReturnedData Data { get; set; }
    }
}
