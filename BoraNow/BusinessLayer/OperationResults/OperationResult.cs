using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.BoraNow.BusinessLayer.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
