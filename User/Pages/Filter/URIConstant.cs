namespace User.Pages.Filter;

public static class URIConstant
{    
    public static readonly string[] WhiteListUris = {         
        "/",
        "/Authenticate/Login",
        "/Authenticate/Logout",
        "/Authenticate/Register",
        "/Artworks/"
    };

    public static readonly string[] CreatorListUris = {
        "/Creator/Profile",
        "/Creator/ArtworkList",
        "/Creator/UploadNewArtwork",
        "/Creator/AudienceSubscriberStatistic"
    };

    public static readonly string[] AudienceListUris = {
        "/Artworks/"
    };

    public static readonly string HomePage = "../Index";

    public static readonly string Login = "/Authenticate/Login";

    public static readonly string ArtworkList = "/Creator/ArtworkList";
}
