namespace User.Pages.Filter;

public static class URIConstant
{
    public static readonly string[] WhiteListUris = {
        "/Login",
        "/Logout",
        "/NotFound"
    };

    public static readonly string[] ModeratorListUris = {
        "/Moderator/"
    };

    public static readonly string[] AdminListUris = {
        "/Admins/AccountDetailManagement",
        "/Admins/AccountManagement",
        "/Admins/Dashboard",
        "/Admins/Setting",
        "/Admins/TopArtWork",
        "/Admins/TopCreator",
        "/Admins/"
    };

    public static readonly string HomePage = "/Login";

    public static readonly string Login = "/Login";
    public static readonly string NotFound = "/NotFound";
}
