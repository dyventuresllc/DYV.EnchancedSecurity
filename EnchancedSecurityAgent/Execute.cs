using Relativity.Identity.V1.Services;
using Relativity.API;
using System.Data;
using System.Collections.Generic;
using Relativity.Identity.V1.Shared;
using System.ComponentModel.Design;

namespace EnchancedSecurityAgent
{
    public class Execute
    {
        private readonly IUserManager userManager;
        private readonly IGroupManager groupManager;
        private readonly IAPILog logger;

        public Execute(IUserManager _userManager, IGroupManager _groupManager, IAPILog logger)
        {
            this.userManager = _userManager;
            this.groupManager = _groupManager;
            this.logger = logger;
        }

        public void RunDisabledUserNoGroups(IDBContext eddsDbContext, string keyword, string notes)
        {
            DataTable reporting = ResourceHelpers.Reporting();
            var resourcePath = @"\DataAccess\SQL\UsersWithNoGroups.sql";
            var sqlText = ResourceHelpers.sqlStreamToText(resourcePath);

            //SQL Read
            DataTable dtUsersWithNoGroups = eddsDbContext.ExecuteSqlStatementAsDataTable(sqlText);
            logger.LogInformation("SQL Read Action: [Users With No Groups] - Records {@numRecords}", dtUsersWithNoGroups.Rows.Count);

            foreach (DataRow row in dtUsersWithNoGroups.Rows)
            {
                //disable user with API and then add user to reporting table for email
                UserManagerHelper.DisableUsers((int)row[0], keyword, notes, userManager).Wait();

                DataRow dr = reporting.NewRow();
                dr[0] = row[0];  //UserArtifactID
                dr[1] = row[1];  //UserFirstName
                dr[2] = row[2];  //UserLastName
                dr[3] = row[3];  //UserEmailAddress

                reporting.Rows.Add(dr);
            }

            //Send reporting
            //TODO: Add Email Functionality 

            logger.LogInformation("Method execution complete - {0}");
            //TODO:  pass method name that is complete
        }

        public void RunDisabledUsersNotLoggIn(IDBContext eddsDbContext, string keyword, string notes)
        {
            DataTable reporting = ResourceHelpers.Reporting();
            List<string> groups = new List<string>();

            var resourcePath = @"\DataAccess\SQL\UsersNeverLoggedIn.sql";
            var sqlText = ResourceHelpers.sqlStreamToText(resourcePath);

            DataTable dtUsersNeverLoggedIn = eddsDbContext.ExecuteSqlStatementAsDataTable(sqlText);
            logger.LogInformation("SQL Read Action: [Users With No Groups] - Records {@NumRecords}", dtUsersNeverLoggedIn.Rows.Count);

            foreach (DataRow row in dtUsersNeverLoggedIn.Rows)
            {
                //list of groups user is associated
                QueryResultSlim queryResults = UserManagerHelper.RetrieveGroupsForUsers((int)row[0], userManager).Result;

                //Remove groups from user
                foreach (var result in queryResults.Objects)
                {
                    if (result.Values[0].ToString() != "Everyone")
                    {
                        groups.Add(result.Values[0].ToString());
                        UserManagerHelper.RemoveUsersFromGroup(result.ArtifactID, (int)row[0], groupManager).Wait();
                    }
                }

                //remove users
                UserManagerHelper.DisableUsers((int)row[0], keyword, notes, userManager).Wait();
                DataRow dr = reporting.NewRow();
                dr[0] = row[0];
                dr[1] = row[1];
                dr[2] = row[2];
                dr[3] = row[3];
                dr[4] = string.Join("; ", groups);  //groups being removed for user being disabled
                reporting.Rows.Add(dr);
            }
            //TODO: send email to EDS about removed users
            //Emails.Report()

            logger.LogInformation("Method execution complete - {0}");
            //TODO:  pass method name that is complete
        }

        public void x(IDBContext eddsDbContext)
        {
            var resourcePath = @"\DataAccess\SQL\Queue_Insert.sql";
            var sqlText = ResourceHelpers.sqlStreamToText(resourcePath);

            //SQL Read
            int numRecordsAdded = eddsDbContext.ExecuteNonQuerySQLStatement(sqlText);
            logger.LogInformation("SQL Read Action: [Users Over records to queue] - Records{@numRecords}", numRecordsAdded);


            resourcePath = @"\DataAccess\SQL\Users_OverX.sql";
            sqlText = ResourceHelpers.sqlStreamToText(resourcePath);
            DataTable dtUsersOver = eddsDbContext.ExecuteSqlStatementAsDataTable(sqlText);







            DataTable dtQueueUsers = eddsDbContext.ExecuteSqlStatementAsDataTable(sqlText);
            logger.LogInformation("SQL Read Action: [Users Over] - Records{@numRecords}", dtUsersOver.Rows.Count);

            foreach(DataRow dr in dtUsersOver.Rows) 
            {

                //
               
            }

        }
    }
}
