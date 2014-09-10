using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class APIResult
    {
        public DateTime Timestamp
        {
            get;
            set;
        }

        public bool ExecutedSuccesfully
        {
            get
            {
                return CheckSuccessConditions();
            }
        }


        public ExecutionStatus Status
        {
            get;
            set;
        }

        internal APIResult()
        {
            Timestamp = DateTime.Now;
            Status = ExecutionStatus.Pending;
            
        }

        protected virtual bool CheckSuccessConditions()
        {
            return false;
        }
    }


    public enum ExecutionStatus
    {
        Pending,
        WaitingForResponse,
        Failed,
        Succeeded
    }
}
