using System;

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
