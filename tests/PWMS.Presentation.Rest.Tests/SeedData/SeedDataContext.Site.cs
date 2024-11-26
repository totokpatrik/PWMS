using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
public sealed partial class SeedDataContext
{
    public static Site Site(User user) => new Site(SiteName, user);
    public static string SiteName => "TestSite";
}