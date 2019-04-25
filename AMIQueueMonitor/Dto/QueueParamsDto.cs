using System;

namespace AMIQueueMonitor.Dto
{
    public class QueueParamsDto : IEquatable<QueueParamsDto>
    {
        public string Queue { get; set; }
        public string QueueDescription { get; set; }
        public int Calls { get; set; }
        public int Abandoned { get; set; }
        public int Completed { get; set; }
        public DateTime DateReceived { get; set; }
        //public int QueueLocalWeight { get; set; }

        public bool Equals(QueueParamsDto otherDto)
        {
            if (otherDto == null || Queue == null)
            {
                return false;
            }
            else
            {
                return Queue.Equals(otherDto.Queue);
            }
        }
    }
}
