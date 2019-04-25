using System;

namespace AMIQueueMonitor.Dto
{
    public class QueueEntryDto : IEquatable<QueueEntryDto>
    {
        public string CallerId { get; set; }
        public string CallerIdName { get; set; }
        public string Queue { get; set; }
        public string QueueDescription { get; set; }
        public int Position { get; set; }
        public long Wait { get; set; }
        //public int QueueLocalWeight { get; set; }

        public bool Equals(QueueEntryDto otherDto)
        {
            if (otherDto == null || Queue == null)
            {
                return false;
            }
            else
            {
                return Queue.Equals(otherDto.Queue) && Position.Equals(otherDto.Position);
            }
        }
    }
}
