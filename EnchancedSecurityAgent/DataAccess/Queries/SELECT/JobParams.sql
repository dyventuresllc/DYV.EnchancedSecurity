    SELECT
	    JobId,
        JobName,
        JobExecute_Time_Hour,
        JobExecute_Time_Minute,
        JobExecute_Interval,
        JobLastExecute_DateTime
    FROM EDDS.qe.QuinnJobQueue 
    WHERE JobId = 1;