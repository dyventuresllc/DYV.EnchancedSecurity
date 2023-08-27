using DYV.EnchancedSecurity.Agent.Models;
using System;
using System.Data;


namespace DYV.EnchancedSecurity.Agent.Helpers
{
    internal static class Transformers
    {
        internal static JobQueueModel JobQueueItem(DataRow row)
        {
            JobQueueModel output = new JobQueueModel();

            output.JobId = int.Parse(row["JobId"].ToString());
            output.JobName = row["JobName"].ToString();
            output.JobExecute_Time_Hour = int.Parse(row["JobExecute_Time_Hour"].ToString());
            output.JobExecute_Time_Minute  = int.Parse(row["JobExecute_Time_Minute"].ToString());
            output.JobExecute_Interval = int.Parse(row["JobExecute_Interval"].ToString());
            output.JobLastExecute_DateTime = (row["JobLastExecute_DateTime"].ToString() != String.Empty) ? DateTime.Parse(row["JobLastExecute_DateTime"].ToString()) : new DateTime(1900,1,1,00,00,00);

            return output;
        }
    }
}
