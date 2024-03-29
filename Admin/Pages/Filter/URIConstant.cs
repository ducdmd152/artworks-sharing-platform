namespace User.Pages.Filter;

public static class URIConstant
{
    public static readonly string[] WhiteListUris = {
        "/Login",
        "/Logout",
        "/"
    };

    public static readonly string[] ModeratorListUris = {
        "/Moderator/ArtWorksManagement",
        "/Moderator/ReportManagement",
        "/Moderator/ModeratorEditProfile",
    };

    public static readonly string[] AdminListUris = {
        "/Admins/AccountDetailManagement",
        "/Admins/AccountManagement",
        "/Admins/Dashboard",
        "/Admins/Setting",
        "/Admins/TopArtWork",
        "/Admins/TopCreator",
    };

    public static readonly string HomePage = "/";

    public static readonly string Login = "/Login";
}