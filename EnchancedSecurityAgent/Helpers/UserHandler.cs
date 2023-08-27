using Relativity.API;
using Relativity.Identity.V1.Services;
using Relativity.Identity.V1.UserModels;

using System.Threading.Tasks;

namespace DYV.EnchancedSecurity.Agent.Helpers
{
    public class UserHandler
    {
        internal IServicesMgr ServicesMgr { get; set; }

        public UserHandler(IServicesMgr servicesMgr)
        {            
            ServicesMgr = servicesMgr;
        }

        public async Task DisabledUsersAsync(int userId, string notes, string keyword)
        {
            using (IUserManager userManager = ServicesMgr.CreateProxy<IUserManager>(ExecutionIdentity.System))
            {
                UserResponse response = await userManager.ReadAsync(userId);
                UserRequest request = new UserRequest(response)
                {
                    RelativityAccess = false,
                    Notes = notes,
                    Keywords = keyword
                };
                await userManager.UpdateAsync(userId, request);
            }         
        }
    }
}
