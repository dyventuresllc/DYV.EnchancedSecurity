using System;

namespace DYV.EnchancedSecurity.Agent.Models
{
    public class JobQueueModel
    {
        public int JobId { get; set; }
        public string JobName { get; set; }    
        public int JobExecute_Time_Hour { get; set; }
        public int JobExecute_Time_Minute { get; set; }     
        public int JobExecute_Interval { get; set; }
        public DateTime? JobLastExecute_DateTime { get; set; }
    }
}
