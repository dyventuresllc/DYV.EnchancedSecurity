using DYV.EnchancedSecurity.Agent.Models;
using Relativity.API;
using System.Data;

namespace DYV.EnchancedSecurity.Agent.Helpers
{
    public class QueueHandler
    {
        IDBContext EddsDbContext { get; set; }

        IAPILog Logger { get; set; }

        public QueueHandler(IDBContext eddsDbContext, IAPILog logger)
        {
            EddsDbContext = eddsDbContext;
            Logger = logger;
        }

        public JobQueueModel GetJobParameters()
        {
            JobQueueModel output = null;
            DataTable dt = EddsDbContext.ExecuteSqlStatementAsDataTable(DataAccess.Queries.SELECT.JobParams);

            if (dt.Rows.Count == 1)
            {
                output = Transformers.JobQueueItem(dt.Rows[0]);
            }
            return output;
        }

        public void UpdateLastSuccessfulRunTime() => EddsDbContext.ExecuteNonQuerySQLStatement(DataAccess.Queries.UPDATE.LastRun);
    }
}
