using System.Security.Claims;

namespace InRealTime.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue("user_id").ToString();
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue("username").ToString();
        }
    }
}
