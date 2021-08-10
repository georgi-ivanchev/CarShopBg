namespace CarShopBg.Infrastructure
{
    using System.Security.Claims;
    public static class ClaimsPrincipalExtention
    {
        public static string Id(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
