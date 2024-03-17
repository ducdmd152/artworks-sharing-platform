namespace ArtHubBO.Constants;

public class S3Constants
{
    private S3Constants() { }

    public static readonly string AccessKey = "AWS_ACCESS_KEY";
    public static readonly string SecretKey = "AWS_SECRET_KEY";
    public static readonly string BucketName = "AWS_BUCKET_NAME";
    public static readonly string UploadSuccess = "Upload successfull."; 
    public static readonly string DeleteSuccess = "Delete successfull.";
    public static readonly string FolderS3 = "Artwork/";
    public static readonly string BaseUrlS3 = "S3_BASE_URL";
}
