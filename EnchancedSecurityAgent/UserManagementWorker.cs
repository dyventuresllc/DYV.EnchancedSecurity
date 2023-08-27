using Relativity.API;
using kCura.Agent;
using System.Runtime.InteropServices;
using System;
using System.Data;
using DYV.EnchancedSecurity.Agent.Helpers;
using DYV.EnchancedSecurity.Agent.Models;
using System.Text;

namespace DYV.EnchancedSecurity.Agent
{
    [kCura.Agent.CustomAttributes.Name("DYV Enchanced Security Worker")]
    [Guid("63F55A37-F3E2-4B4C-B3EE-7D8AF5845675")]

    public class EnchancedSecurityWorker : AgentBase
    {
       
        private IAPILog logger;
        public override string Name => "DYV Enchanced Security Worker";

        public override void Execute()
        {
            IDBContext eddsDbContext = Helper.GetDBContext(-1);
            QueueHandler queueHandler = new QueueHandler(eddsDbContext, logger);
            IServicesMgr servicesMgr = Helper.GetServicesManager();
            logger = Helper.GetLoggerFactory().GetLogger();
            JobQueueModel queueItem;
            var instanceSettingManager = Helper.GetInstanceSettingBundle();
            string apiKey = instanceSettingManager.GetStringAsync("QuinnEmanuel.Notification", "SMTPPassword").Result;
            string emailFromAddress = instanceSettingManager.GetStringAsync("QuinnEmanuel.Notification", "EmailFromAddress").Result;
            bool successfulRunNoDocs = true;
            try
            {
                queueItem = queueHandler.GetJobParameters();
                
                if(queueItem == null) 
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Exception(ex, "Error during job retrieval");
                return;
            }

            TimeSpan elapsedTS = (DateTime.Now - (DateTime)queueItem.JobLastExecute_DateTime);
            
            if ((elapsedTS.TotalHours > 24) && DateTime.Now.Hour > queueItem.JobExecute_Time_Hour && DateTime.Now.Minute > queueItem.JobExecute_Time_Minute)
            {
                DataHandler dataHandler = new DataHandler(eddsDbContext);
                UserHandler userHandler = new UserHandler(servicesMgr);
            
                var dtUserNoGroups = dataHandler.UsersNoGroup();

                if (dtUserNoGroups.Rows.Count> 0) 
                {
                    DisableUsersNoGroups(dtUserNoGroups, userHandler, apiKey, emailFromAddress, out successfulRunNoDocs);              
                }

                if (successfulRunNoDocs)
                    queueHandler.UpdateLastSuccessfulRunTime();
            }
            else 
            {
                return;
            }           
        }

        public void DisableUsersNoGroups(DataTable dt, UserHandler userHandler, string apiKey, string emailFrom, out bool successfulRunNoDocs)
        {
            StringBuilder disabledUsers = new StringBuilder();
            string note = "Disabled per Policy by QE automation";
            string keyword = "User had no active groups, account disabled.";
            successfulRunNoDocs = true;

            foreach (DataRow dr in dt.Rows) 
            {
                try 
                {
                    userHandler.DisabledUsersAsync((int)dr[0],note, keyword).Wait();
                    disabledUsers = MessageHandler.Gridbody(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(),disabledUsers);
                }
                catch (Exception ex) 
                {
                    successfulRunNoDocs = false;
                    Exception(ex,"Error disabling user");                    
                    return;
                }
            }

            if (successfulRunNoDocs)
            {   //make an instance setting to perfix the environment between environments like US/EU/AU 
                string msgBody = MessageHandler.MesssageNoGroups(disabledUsers);
                EmailHandler.SendEmail(apiKey, emailFrom, "damienyoung@quinnemanuel.com", "Disable Users No Groups", msgBody);
            }
        }

        public void Exception(Exception ex, string errorMessage)
        {            
            errorMessage += ex.InnerException != null ? string.Concat("---", ex.InnerException) : string.Concat("---", ex.Message);
            //string message = $"{ex.Message}{innerMessage}";  
            logger.LogError(ex, errorMessage);
            RaiseError(errorMessage, ex.ToString());
            return;
        }
    }
}
