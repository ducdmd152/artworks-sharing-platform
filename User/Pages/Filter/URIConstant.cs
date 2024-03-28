namespace User.Pages.Filter;

public static class URIConstant
{    
    public static readonly string[] WhiteListUris = {         
        "/Index",
        "/Authenticate/Login",
        "/Authenticate/Logout",
        "/Authenticate/Register",
        "/Artworks/",
        "/CreatorExploration/"
    };

    public static readonly string[] CreatorListUris = {
        "/Creator/ArtworkList",
        "/Creator/AudienceSubscriberStatistic",
        "/Creator/EditPost",
        "/Creator/EditProfile",
        "/Creator/Profile",
        "/Creator/UploadNewArtwork",
    };

    public static readonly string[] AudienceListUris = {
        "/Audience/"
    };

    public static readonly string HomePage = "../Index";

    public static readonly string Login = "/Authenticate/Login";

    public static readonly string ArtworkList = "/Creator/ArtworkList";
}
