using Relativity.Identity.V1.Services;
using Relativity.Identity.V1.Shared;
using Relativity.Identity.V1.UserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnchancedSecurityAgent
{

    public static class UserManagerHelper
    {
        public static async Task DisableUsers(int userArtifactId, string keyword, string notes, IUserManager userManager)
        {
            UserResponse userResponse = await userManager.ReadAsync(userArtifactId);
            UserRequest request = new UserRequest(userResponse);
            request.RelativityAccess = false;
            request.Keywords = keyword;
            request.Notes = notes;
            await userManager.UpdateAsync(userArtifactId, request);
        }

        public static async Task<QueryResultSlim> RetrieveGroupsForUsers(int userArtifactId, IUserManager userManager)
        {
            QueryRequest request = new QueryRequest
            {
                Fields = new List<FieldRef> { new FieldRef { Name = "Name" } },
                Condition = ""
            };
            return await userManager.QueryGroupsByUserAsync(request, 1, 10000, userArtifactId);
        }

        public static async Task RemoveUsersFromGroup(int GroupArtifactId, int UserArtifactId, IGroupManager groupManager)
        {
            ObjectIdentifier objIdentifier = new ObjectIdentifier()
            {
                ArtifactID = UserArtifactId
            };
            await groupManager.RemoveMembersAsync(GroupArtifactId, objIdentifier);
        }
    }
}
