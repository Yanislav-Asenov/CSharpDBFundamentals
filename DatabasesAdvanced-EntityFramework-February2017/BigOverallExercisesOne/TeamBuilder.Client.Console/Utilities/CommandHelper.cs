using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Console.Utilities
{
    public static class CommandHelper
    {
        public static bool IsTeamExistring(string teamName)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExistring(string username)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Users.Any(u => u.Username == username && u.IsDeleted == false);
            }
        }

        public static bool IsInviteExistring(string teamName, User user)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Teams.Any(t => t.Name == teamName && t.Members.Any(m => m.Username == username));
            }
        }

        public static bool IsEventExistring(string eventName)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }

        public static bool IsUserCreatorOfEvent(int eventId, User user)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Events.Any(e => e.Id == eventId && e.CreatorId == user.Id);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Teams.Any(t => t.Name == teamName && t.CreatorId == user.Id);
            }
        }
    }
}
