using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC.Core
{
    public class ServiceProcessingError
    {
        public string UserMessage { get; set; }
        public string UserHelp { get; set; }
        public bool IsFatal { get; set; }

        public ServiceProcessingError(string userMessage, string userHelp, bool isFatal)
        {
            UserMessage = userMessage;
            UserHelp = userHelp;
            IsFatal = isFatal;
        }
    }
}
