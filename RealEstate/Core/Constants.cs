namespace RealEstate.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Master = "Master";
            public const string Admin = "Admin";
        }

        public static class Policies
        {
            public const string RequireMaster = "RequireMaster";
            public const string RequireAdmin = "RequireAdmin";
        }
    }
}
