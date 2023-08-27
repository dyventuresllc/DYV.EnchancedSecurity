    UPDATE qjq
        SET  qjq.[JobLastExecute_DateTime] = GETDATE()
    FROM EDDS.qe.QuinnJobQueue qjq
    WHERE JobId = 1;