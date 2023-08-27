using Relativity.API;
using System.Data;

namespace DYV.EnchancedSecurity.Agent.Helpers
{    
    public class DataHandler
    {
        IDBContext EddsDbContext { get; set; }
       
        public DataHandler(IDBContext eddsDbContext) 
        {
            EddsDbContext= eddsDbContext;            
        }

        public DataTable UsersNoGroup() => EddsDbContext.ExecuteSqlStatementAsDataTable(DataAccess.Queries.SELECT.Users_WithNoGroups);
        
    }
}
